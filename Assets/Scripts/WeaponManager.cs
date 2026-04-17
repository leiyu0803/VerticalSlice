using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour
{
    private PlayerInput playerInput;

    [Header("Fire Settings")]
    [SerializeField] int RPM;
    float firerate, fireTimer;
    [SerializeField] bool semiAuto;

    [Header("Bullet Settings")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePos;
    [SerializeField] float bulletSpeed;
    [SerializeField] float bulletPerShoot;
    AimStateManager aim;

    [Header("Sound Settings")]
    [SerializeField] AudioClip equipSound;
    [SerializeField] List<AudioClip> shootSound;
    [SerializeField] AudioClip tailSound;
    AudioSource audioSource;
    WeaponAmmo weaponAmmo;
    bool Shooted;

    Animator animator;
    ActionStateManager actionStateManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerInput = GetComponentInParent<PlayerInput>();
        aim = GetComponentInParent<AimStateManager>();
        weaponAmmo = GetComponentInParent<WeaponAmmo>();
        animator = GetComponent<Animator>();
        actionStateManager = GetComponentInParent<ActionStateManager>();
        firerate = 60f / RPM;
        fireTimer = 60f / RPM;
        audioSource.PlayOneShot(equipSound);
    }

    // Update is called once per frame
    void Update()
    {
        if(ShouldFire())
        {
            Fire();

        }
        if (ShouldPlayTailSound()&&Shooted)
        {
            audioSource.PlayOneShot(tailSound);
            Shooted = false;
        }


    }

    bool ShouldFire()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer < firerate)
        {
            return false;
        }
        if(weaponAmmo.currentAmmo == 0)
        {
            return false;
        }
        if(actionStateManager.currentState == actionStateManager.Reload)
        {
            return false;
        }
        if (semiAuto && playerInput.actions["Attack"].WasPressedThisFrame())
        {
            return true;
        }
        if (!semiAuto && playerInput.actions["Attack"].IsPressed())
        {
            return true;
        }
        return false;
    }
    bool ShouldPlayTailSound()
    {
        if (semiAuto)
        {
            return true;
        }
        if (!semiAuto && playerInput.actions["Attack"].WasReleasedThisFrame())
        {
            return true;
        }
        return false;
    }
    void Fire()
    {
        fireTimer = 0;
        firePos.LookAt(aim.aimPos);
        audioSource.PlayOneShot(shootSound[Random.Range(0, shootSound.Count)]);
        weaponAmmo.currentAmmo--;
        for (int i = 0;i< bulletPerShoot;i++)
        {
            GameObject currentBullet = Instantiate(bulletPrefab, firePos.position, firePos.rotation);
            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            rb.AddForce(firePos.forward * bulletSpeed, ForceMode.Impulse);
        }
        Shooted = true;
        animator.SetTrigger("Fire");
        Debug.Log(weaponAmmo.currentAmmo);
    }
}
