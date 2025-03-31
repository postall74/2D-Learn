using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    [SerializeField] private Text _counterText;
    [SerializeField] private float _delay = 0.5f;
    [SerializeField] private Image _playPanel;
    [SerializeField] private Image _stopPanel;

    private int _counter = 0;
    private bool _isRunning = false;
    private Coroutine _counterCoroutine;

    private void Start()
    {
        UpdatePanelColors();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            ToggleCounter();
    }

    public void ToggleCounter()
    {
        if (_isRunning)
        {
            StopCoroutine(_counterCoroutine);
            _counterCoroutine = null;
            _isRunning = false;
        }
        else
        {
            _counterCoroutine = StartCoroutine(Count());
            _isRunning = true;
        }


        UpdatePanelColors();
    }

    private IEnumerator Count()
    {
        while (true)
        {
            _counter++;

            if (_counterText != null)
                _counterText.text = $"{_counter}";

            yield return new WaitForSeconds(_delay);
        }
    }

    private void UpdatePanelColors()
    {
        _playPanel.color = _isRunning ? Color.green : Color.white;
        _stopPanel.color = _isRunning ? Color.white : Color.red;
    }
}
