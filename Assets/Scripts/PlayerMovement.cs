using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _speedUpSpeed;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _speedUpDistance;
    [SerializeField] private Player _player;

    private bool _speedUp = false;
    private float _startSpeed;
    private Vector3 _startPosition;
    private Vector3 _currentposition;
    private Coroutine _speedingUp;

    private void Start()
    {
        _startSpeed = _speed;
    }

    private void Update()
    {

        _startPosition = transform.position;
        if (_speedUp==false)
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(Vector3.down * Time.deltaTime * _rotationSpeed);
                MoveForward();
            }

            else if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(Vector3.up * Time.deltaTime * _rotationSpeed);
                MoveForward();
            }

            else if (Input.GetKey(KeyCode.W))
            {
                MoveForward();
            }

            else
            {
                if (_animator.GetBool("Run") == true)
                {
                    _animator.SetBool("Run", false);
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (_speedingUp == null)
            {
                _speedingUp = StartCoroutine(SpeedingUp());
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            player.TakeDamage(_player.Damage);
        }
    }

    private IEnumerator SpeedingUp()
    {
        _speedUp = true;
        if (_animator.GetBool("SpeedUp") == false)
        {
            _animator.SetBool("SpeedUp", true);

        }
        Vector3 direction = (transform.position - _startPosition).normalized;
        Vector3 destination = new Vector3(transform.position.x + (_speedUpDistance * direction.x), transform.position.y + (_speedUpDistance * direction.y), transform.position.z+(_speedUpDistance*direction.z));
        while (transform.position!=destination)
        {
            transform.position = Vector3.MoveTowards(transform.position,destination,_speedUpSpeed);
            yield return null;
        }
        Debug.Log("fygjc");
        _animator.SetBool("SpeedUp", false);
        _speedUp = false;
        _speedingUp = null;
        
    }

    private void MoveForward()
    {

        if (_animator.GetBool("Run") == false)
        {
            _animator.SetBool("Run", true);

        }
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        _currentposition = transform.position;
    }
}
