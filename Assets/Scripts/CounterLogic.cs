using System;
using System.Collections;
using UnityEngine;

public class CounterLogic : MonoBehaviour
{

    [SerializeField] private float _delay = 0.5f;

    private int _counter = 0;
    private Coroutine _coroutine;
    private WaitForSeconds _wait;

    public event Action<int> OnCounterChanging;
    public bool IsRunning => _coroutine != null;

    private void Awake()
    {
        _wait = new WaitForSeconds(_delay);
    }

    public void Toggle()
    {
        if(_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
        else
        {
            _coroutine = StartCoroutine(Count());
        }
    }

    private IEnumerator Count()
    {
        while (enabled)
        {
            _counter++;
            OnCounterChanging?.Invoke(_counter);
            yield return _wait;
        }
    }
}