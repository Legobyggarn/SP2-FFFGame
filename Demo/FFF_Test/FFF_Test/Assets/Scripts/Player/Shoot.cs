﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shoot : MonoBehaviour
{
    Animator anim;
    int fireHash = Animator.StringToHash("FireStart");
    int fireHash1 = Animator.StringToHash("FireIsPressed");
    // Public variables
    public GameObject spawnPoint; // Spawn position of bullet
    public GameObject bulletPrefab; // Object to spawn as bullet/projectile

    public float reloadTime;
    public float chargeTime;
    public float bulletSpeed;
    public float bulletLifespan;
    //public float bulletDamage;

    // Private variables
    private float reloadTimer;
    private float chargeTimer;
    private bool isLoaded;

    private List<GameObject> BulletList;
    // Use this for initialization
    void Start()
    {
        BulletList = new List<GameObject>();
        reloadTimer = reloadTime;
        isLoaded = true;
        chargeTimer = 0.0f;
        anim = GetComponent<Animator>();
    }
    public float animSpeed;
    public void SpawnThatBullet()
    {

        GameObject ThaBullet = Instantiate(bulletPrefab, spawnPoint.transform.position, transform.rotation) as GameObject;
        BulletList.Add(ThaBullet);

    }
    public void FIRETHATSHIT()
    {
        // Instantiate projectile...
        // Fire projectile (add force)...
        while (BulletList.Count != 0)
        {
            GameObject ThaBullet = BulletList[0];
            BulletList.RemoveAt(0);
            //  Debug.Log("FUCKING SHOOTIN NOW");
            Destroy(ThaBullet, bulletLifespan);
            // Add force as impulse instead of continous force!
            ThaBullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
        }
    }
    // Update is called once per frame
    void Update()
    {

        ShootAnimation();

    }
    private void ShootAnimation()
    {

        // Shoot
        if (Input.GetAxis("Fire1") >= 0.2f)
        {
            anim.SetBool("FireIsPressed", true);
            if (isLoaded)
            { // Can shoot (reload finished)
                if (chargeTimer >= chargeTime)
                { // Can shoot (charge up finished)
                  // Play fire animation...

                    anim.SetTrigger(fireHash);
                    reloadTimer = 0.0f;
                    isLoaded = false;
                    chargeTimer = 0.0f;
                }
                else { // Can't shoot (charging up)
                       // Play chaching animation...

                    chargeTimer += Time.deltaTime;

                    // Debug
                    //Debug.Log("Charging!");
                }
            }
        }
        else {
            anim.SetBool("FireIsPressed", false);
            anim.ResetTrigger(fireHash);
        }


        if (!isLoaded)
        {
            reloadTimer += Time.deltaTime;

            if (reloadTimer >= reloadTime)
            {
                isLoaded = true;
            }
        }
    }
    // OLD FUNCTION THAT ISNT USED ANYMORE
    private void SUPERLAZERSHOOT()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("shoot_5"))
        {




            // Instantiate projectile...
            // Fire projectile (add force)...
            GameObject go = Instantiate(bulletPrefab, spawnPoint.transform.position, transform.rotation) as GameObject;
            Destroy(go, bulletLifespan);
            // Add force as impulse instead of continous force!
            go.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);







        }

        /*	if (anim.IsPlaying("shoot_4") || anim.IsPlaying("shoot_5")) {
                    anim.Play ("shoot_4");
                } else {
                    anim.SetTrigger (fireHash);
                    anim.SetBool ("FireIsPressed", true);
                }*/
    }
    // OLD shoot implementation
    private void OldShoot()
    {




        // Shoot
        if (Input.GetKey(KeyCode.Space))
        {
            if (isLoaded)
            { // Can shoot (reload finished)
                if (chargeTimer >= chargeTime)
                { // Can shoot (charge up finished)
                  // Play fire animation...

                    anim.SetTrigger(fireHash);
                    anim.SetBool("FireIsPressed", true);
                    if (anim.GetCurrentAnimatorStateInfo(0).IsName("shoot_5"))
                    {
                        //anim.Play ("shoot_5");
                    }


                    // Instantiate projectile...
                    // Fire projectile (add force)...
                    GameObject go = Instantiate(bulletPrefab, spawnPoint.transform.position, transform.rotation) as GameObject;
                    Destroy(go, bulletLifespan);
                    // Add force as impulse instead of continous force!
                    go.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);

                    // Reset timers (reload and charge)
                    reloadTimer = 0.0f;
                    isLoaded = false;
                    chargeTimer = 0.0f;


                    // Debug
                    //Debug.Log("FIRE!");
                }
                else { // Can't shoot (charging up)
                       // Play chaching animation...

                    chargeTimer += Time.deltaTime;

                    // Debug
                    //Debug.Log("Charging!");
                }
            }
        }
        else {
            anim.SetBool("FireIsPressed", false);
            anim.ResetTrigger(fireHash);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            // Start playing idle animation... (?)

            // Reset 'chargeTimer'
            chargeTimer = 0.0f;
        }

        if (!isLoaded)
        {
            reloadTimer += Time.deltaTime;

            if (reloadTimer >= reloadTime)
            {
                isLoaded = true;
            }
        }
    }
}
