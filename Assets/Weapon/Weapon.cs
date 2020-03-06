using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Weapon : MonoBehaviour {
    private AudioSource audioSource;
    private bool fireLock = false;
    private Vector3 originalPosition;

    [Header("Weapon")]
    public float damage = 20f;
    public float fireRate = 0.2f;
    public float range = 100f;

    [Header("Iron Sight")]
	public Vector3 ironSightOffset;
	public float ironSightSpeed = 10f;

    [Header("Object References")]
	public ParticleSystem muzzleFlash;

    [Header("Audio References")]
	public AudioClip fireSound;

    void Start() {
        originalPosition = transform.localPosition;

        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Fire();
        }
        
        HandleAim();
    }

    void Fire() {
        if (fireLock) return;
        
        fireLock = true;
        Invoke("ResetFireLock", fireRate);

        muzzleFlash.Stop();
        muzzleFlash.Play();

        audioSource.PlayOneShot(fireSound);

        print("Damage: " + damage);
        print("Range: " + range);
    }

    void ResetFireLock() {
        fireLock = false;
    }

    void HandleAim() {
        bool shouldAim = Input.GetMouseButton(1);
        Vector3 targetPosition;

        if (shouldAim) {
            targetPosition = ironSightOffset;
        }
        else {
            targetPosition = originalPosition;
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, ironSightSpeed * Time.deltaTime);
    }
}
