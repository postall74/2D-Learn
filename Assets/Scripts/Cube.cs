using UnityEngine;

[RequireComponent(typeof(CubeSpawner))]
public class Cube : MonoBehaviour, IClickable
{
    [SerializeField, Range(0f, 1f)] private float _splitChance;
    [SerializeField, Range(0f, 1f)] private float _splitDecay;

    private CubeSpawner _spawner;

    public event System.Action<float> OnSplitRequested;

    public void Initialize(float splitChance)
    {
        _splitChance = splitChance;
    }

    private void Awake()
    {
        _spawner = GetComponent<CubeSpawner>();
    }

    public void OnClick()
    {
        if (Random.value < _splitChance)
        {
            float newChance = _splitChance * _splitDecay;
            OnSplitRequested?.Invoke(newChance);
        }

        Destroy(gameObject);
    }
}