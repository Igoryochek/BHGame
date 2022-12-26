using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Material _bodyMaterial;
    [SerializeField] private Color _speedUpColor;
    [SerializeField] private float _damagingTime;
    [SerializeField] private int _health=20;
    [SerializeField] private int _damage=5;

    private bool _canTakeDamage = true;
    private Color _startColor;

    public int Damage => _damage;

    private void Start()
    {
        _startColor = _bodyMaterial.color;
    }

    public void TakeDamage(int count)
    {
        if (_canTakeDamage)
        {
            _health -= count;
            if (_health<=0)
            {
                Debug.Log("you lose!");
            }
            else
            {
                StartCoroutine(TakingDamage());
            }
        }
    }

    private IEnumerator TakingDamage()
    {
        _canTakeDamage = false;
        StartCoroutine(ChangingColorToRed());
        yield return new WaitForSeconds(_damagingTime);
        StartCoroutine(ChangingColorToNormal());
        _canTakeDamage = true;
    }

    private IEnumerator ChangingColorToRed()
    {
        while (true)
        {
            _bodyMaterial.color = Color.Lerp(_bodyMaterial.color,_speedUpColor,2);
            yield return null;
        }
    }
    
    private IEnumerator ChangingColorToNormal()
    {
        while (true)
        {
            _bodyMaterial.color = Color.Lerp(_bodyMaterial.color,_startColor,2);
            yield return null;
        }
    }
}
