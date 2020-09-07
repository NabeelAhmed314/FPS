using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float gunDamage = 10f;
    public float gunRange = 100f;
    public float fireRate = 15f;
    public ParticleSystem muzzleFlash;
    public Camera FPSCam;
    public int maxAmmo = 10;
    public float reloadTime = 1f;
    public Animator animator;
    public bool isAuto;


    private int currentAmmo;
    private bool isReloding = false;
    private float nextTimeToFire = 0f;

    private void Start()
    {
        currentAmmo = maxAmmo;
    }

    private void OnEnable()
    {
        isReloding = false;
        animator.SetBool("reloading", false);
    }
    // Update is called once per frame
    void Update()
    {
        if (isReloding)
        {
            return;
        }
        if(currentAmmo <= 0)
        {
            StartCoroutine("Reload");
            return;
        }

        if (isAuto)
        {
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
    }

    IEnumerator Reload()
    {
        isReloding = true;
        animator.SetBool("reloading", true);
        Debug.Log("Reloading....");
        yield return new WaitForSeconds(reloadTime - 0.25f);
        animator.SetBool("reloading", false);
        yield return new WaitForSeconds(0.25f);
        currentAmmo = maxAmmo;
        isReloding = false;
    }

    void Shoot()
    {
        currentAmmo--;
        Debug.Log("Current Ammo: " + currentAmmo);
        muzzleFlash.Play();
        RaycastHit hit;
        if(Physics.Raycast(FPSCam.transform.position,FPSCam.transform.forward, out hit , gunRange))
        {
            Target target = hit.transform.GetComponent<Target>();
            if(target != null)
            {
                target.takeDamage(gunDamage);
            }
        }

    }
}
