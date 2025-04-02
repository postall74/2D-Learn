using UnityEngine;
using UnityEngine.UI;

public class CounterUI : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private Image _playPanel;
    [SerializeField] private Image _stopPanel;
    [SerializeField] private CounterLogic _logic;

    private void Awake()
    {
        _logic.OnCounterChanging += UpdateTextUI;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            OnToggle();
    }

    private void OnDestroy()
    {
        _logic.OnCounterChanging -= UpdateTextUI;
    }

    private void OnToggle()
    {
        _logic.Toggle();
        UpdatePanelColors();
    }

    private void UpdateTextUI(int counter)
    {
        _text.text = $"{counter}";
    }

    private void UpdatePanelColors()
    {
        _playPanel.color = _logic.IsRunning ? Color.green : Color.white;
        _stopPanel.color = _logic.IsRunning ? Color.white : Color.red;
    }
}
