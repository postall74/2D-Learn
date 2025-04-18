using UnityEngine;

public class Cube : MonoBehaviour, IClickable
{
    [SerializeField, Range(0f, 1f)] private float _splitChance = 1f;
    [SerializeField, Range(0f, 1f)] private float _splitDecay = 0.5f;

    public event System.Action<float> SplitRequested;

    public void Initialize(float splitChance)
    {
        _splitChance = splitChance;
    }

    public void OnClick()
    {
        if (Random.value < _splitChance)
        {
            float newChance = _splitChance * _splitDecay;
            SplitRequested?.Invoke(newChance);
        }

        Destroy(gameObject);
    }
}