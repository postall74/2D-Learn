using UnityEngine;
using UnityEngine.UI;

public class CounterUI : MonoBehaviour
{
    [SerializeField] private Text _counterText;
    [SerializeField] private Image _playPanel;
    [SerializeField] private Image _stopPanel;
    [SerializeField] private CounterLogic _logic;

    private void OnEnable()
    {
        _logic.OnCounterChanged += UpdateCounterTextUI;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            OnToggleCounter();
    }

    private void OnDestroy()
    {
        _logic.OnCounterChanged -= UpdateCounterTextUI;
    }

    private void OnToggleCounter()
    {
        _logic.ToggleCounter();
        UpdatePanelColors();
    }

    private void UpdateCounterTextUI(int counter)
    {
        _counterText.text = $"{counter}";
    }

    private void UpdatePanelColors()
    {
        _playPanel.color = _logic.IsRunning ? Color.green : Color.white;
        _stopPanel.color = _logic.IsRunning ? Color.white : Color.red;
    }
}
