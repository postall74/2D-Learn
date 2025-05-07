using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Rigidbody), typeof(MeshRenderer), typeof(AudioSource))]
public class CubeBehaviour : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private MeshRenderer _meshRenderer;
    private AudioSource _audioSource;
    private Settings _settings;
    private bool _isTouched = false;
    private ObjectPool<CubeBehaviour> _pool;
    private Material _originalMaterial;
    private WaitForSeconds _waitTime;

    public void Initialize(ObjectPool<CubeBehaviour> pool, Settings settings)
    {
        _pool = pool;
        _settings = settings;

        _meshRenderer = GetComponent<MeshRenderer>();
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();

        _waitTime = new WaitForSeconds(UnityEngine.Random.Range(InputConstants.MinLifeTimeCube, InputConstants.MaxLifeTimeCube));
        _originalMaterial = _settings.GetRandomMaterial();
        _meshRenderer.material = _originalMaterial;
        _audioSource.volume = settings.SoundVolume;

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
        if (collision.collider.CompareTag(InputConstants.PlatformTag) && _isTouched == false)
            HandlePlatformCollision();
    }

    private void HandlePlatformCollision()
    {
        _isTouched = true;
        _meshRenderer.material = _settings.GetDifferntMaterail(_originalMaterial);
        AudioClip clip = _settings.GetRandomImpactSound();
        
        if (clip != null)
            _audioSource.PlayOneShot(clip);

        StartCoroutine(ReleaseAfterTime());
    }

    private IEnumerator ReleaseAfterTime()
    {
        yield return _waitTime;
        _rigidbody.isKinematic = true;
        _pool.Release(this);
    }
}
