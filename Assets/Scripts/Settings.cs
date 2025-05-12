using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Game Settings")]
public class Settings : ScriptableObject
{
    [SerializeField] private List<Material> _materials = new List<Material>();

    [Header("Platform Settings")]
    [Range(GameConstants.MinBounciness, GameConstants.MaxBounciness)]
    [SerializeField] private float _platformBounciness = GameConstants.DefaultBounciness;

    [Header("Sound Settings")]
    [SerializeField] private List<AudioClip> _impactSounds = new List<AudioClip>();
    [Range(0f, 1f)][SerializeField] private float _soundVolume = 0.5f;

    public float PlatformBounciness => _platformBounciness;
    public float SoundVolume => _soundVolume;

    public Material GetRandomMaterial()
    {
        return _materials.Count > 0 ? _materials[Random.Range(0, _materials.Count)] : new Material(Shader.Find(GameConstants.StandardShaderName));
    }

    public Material GetDifferentMaterial(Material original)
    {
        if (_materials.Count < GameConstants.MinMaterialCount) 
            return original;

        Material newMaterial;

        do
        {
            newMaterial = GetRandomMaterial();
        }
        while (newMaterial == original);

        return newMaterial;
    }

    public AudioClip GetRandomImpactSound() =>
       _impactSounds.Count > 0 ? _impactSounds[Random.Range(0, _impactSounds.Count)] : null;
}