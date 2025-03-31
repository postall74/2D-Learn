using System;
using System.Collections;
using UnityEngine;

public class CounterLogic : MonoBehaviour
{

    [SerializeField] private float _delay = 0.5f;

    private int _counter = 0;
    private Coroutine _counterCoroutine;
    private WaitForSeconds _wait;

    public event Action<int> OnCounterChanged;
    public bool IsRunning => _counterCoroutine != null;

    private void Awake()
    {
        _wait = new WaitForSeconds(_delay);
    }

    public void ToggleCounter()
    {
        if(_counterCoroutine != null)
        {
            StopCoroutine(_counterCoroutine);
            _counterCoroutine = null;
        }
        else
        {
            _counterCoroutine = StartCoroutine(Count());
        }
    }

    private IEnumerator Count()
    {
        while (enabled)
        {
            _counter++;
            OnCounterChanged?.Invoke(_counter);
            yield return _wait;
        }
    }
}