﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PangPang : MonoBehaviour {
    public float timeUntilDecrease;
    public float overHeatMax;
    public float overHeatTick;
    public float overHeatMult;
    public float coolingTick;
    public float FireRate;
    public float bulletSpeed;
    public float bulletLifespan;
    public float currentHeatLevel;
    private float currentHeatMult;
    private float timeSinceLastFire;
    private float fireTimer;
    private bool overHeated;
    public GameObject spawnPoint; // Spawn position of bullet
    public GameObject bulletPrefab; // Object to spawn as bullet/projectile
    private List<GameObject> BulletList;
    public ChangeTattooAlpha TattoScript;
    Animator anim;
    // Use this for initialization
    void Start () {
        BulletList = new List<GameObject>();
        anim = GetComponent<Animator>();
        currentHeatMult = 0;
        TattoScript = GameObject.Find("/PlayerStandard1/mainchar/arms").GetComponent<ChangeTattooAlpha>();
    }
	
	// Update is called once per frame
	void Update () {
        //  ShootAnimation();
        
        ShootTesting();
    }
    void ShootTesting()
    {
        fireTimer += Time.deltaTime;
        // Wants to shoot
        if(!overHeated && Input.GetAxis("Fire1") >= 0.2f )
        {
            // Not overheated
            if( fireTimer > FireRate)
            {
                // Spawn and fire bullet
                spawnBullet();
                fireBullet();

                fireTimer = 0;
                timeSinceLastFire = 0;
                // Add heat to the weapon
                currentHeatMult += overHeatMult;
                float cHeat = overHeatTick + currentHeatMult;
                currentHeatLevel += cHeat;

               
                if (currentHeatLevel >= overHeatMax)
                {
                    UpdateTattooAlpha();
                    overHeated = true;
                }
            }
         


        }
        else{
            timeSinceLastFire += Time.deltaTime;
            // Enought Time has passed for the weapon to start cooling down
            if(timeSinceLastFire > timeUntilDecrease)
            {
                // Reset heat multiplier and decrease the heat on the hands because they are on FIRAY
                currentHeatMult = 0;
                currentHeatLevel -= coolingTick * Time.deltaTime;
                if(currentHeatLevel < 0)
                {
                    currentHeatLevel = 0;
                }
            }
            if(overHeated && currentHeatLevel <= 0)
            {
                overHeated = false;
                TattoScript.setFadeInTattoo();
            }
        }
        
    }
    private void ShootAnimation()
    {

        // Shoot
        if (Input.GetAxis("Fire1") >= 0.2f)
        {
            anim.SetBool("FireStart", true);
        }
        else {
            anim.SetBool("FireStart", false);
            anim.ResetTrigger("FireStart");
        }


        
    }
    public void spawnBullet()
    {
        GameObject ThaBullet = Instantiate(bulletPrefab, spawnPoint.transform.position, transform.rotation) as GameObject;
        ThaBullet.transform.parent = this.gameObject.transform;
        BulletList.Add(ThaBullet);
    }
    public void fireBullet()
    {
        // Instantiate projectile...
        // Fire projectile (add force)...
        while (BulletList.Count != 0)
        {
            GameObject ThaBullet = BulletList[0];
            BulletList.RemoveAt(0);
            //  Debug.Log("FUCKING SHOOTIN NOW");
            Destroy(ThaBullet, bulletLifespan);
            ThaBullet.transform.parent = null;
            // Add force as impulse instead of continous force!
            ThaBullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
        }
    }

    private void UpdateTattooAlpha()
    {
        float heatPerc = currentHeatLevel / overHeatMax;
        if(heatPerc > 1f)
        {
            heatPerc = 1f;
        }
        heatPerc = 1 - heatPerc;
        TattoScript.setAlpha(heatPerc);
        // tattoscript.setalpha(heatPerc);
        // or when current==max fade out, and when it is recharged fade in
    }
}
