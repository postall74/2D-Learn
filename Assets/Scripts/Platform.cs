using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private Settings _settings;

    private void Start()
    {
        var material = new PhysicMaterial
        {
            bounciness = GetBounciness(),
            frictionCombine = PhysicMaterialCombine.Minimum,
            bounceCombine = PhysicMaterialCombine.Maximum
        };

        GetComponent<Collider>().material = material;
    }

    private float GetBounciness()
    {
        if (_settings != null)
            return Mathf.Clamp(
                _settings.PlatformBounciness,
                GameConstants.MinBounciness,
                GameConstants.MaxBounciness
            );

        return GameConstants.DefaultBounciness;
    }
}