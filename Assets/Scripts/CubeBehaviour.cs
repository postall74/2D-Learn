using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(MeshRenderer), typeof(AudioSource))]
public class CubeBehaviour : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private MeshRenderer _meshRenderer;
    private AudioSource _audioSource;
    private Settings _settings;
    private bool _isTouched = false;
    private Material _originalMaterial;
    private WaitForSeconds _waitTime;

    public event System.Action<CubeBehaviour> OnReleaseRequested;

    public void Initialize(Settings settings)
    {
        _settings = settings;

        _meshRenderer = GetComponent<MeshRenderer>();
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
        _waitTime = new WaitForSeconds(UnityEngine.Random.Range(GameConstants.MinLifeTime, GameConstants.MaxLifeTime));
        _originalMaterial = _settings.GetRandomMaterial();
        _meshRenderer.material = _originalMaterial;

        ResetState();
    }

    public void ResetState()
    {
        _isTouched = false;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.isKinematic = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Platform platform) 
            && _isTouched == false)
            HandlePlatformCollision();
    }

    private void HandlePlatformCollision()
    {
        _isTouched = true;
        ChangeMaterial();
        PlayImpactSound();
        StartCoroutine(ReleaseRoutine());
    }

    private void ChangeMaterial()
    {
        _meshRenderer.material = _settings.GetDifferentMaterial(_originalMaterial);
    }

    private void PlayImpactSound()
    {
        AudioClip clip = _settings.GetRandomImpactSound();

        if (clip != null)
            _audioSource.PlayOneShot(clip);
    }

    private IEnumerator ReleaseRoutine()
    {
        yield return _waitTime;
        OnReleaseRequested?.Invoke(this);
    }
}
