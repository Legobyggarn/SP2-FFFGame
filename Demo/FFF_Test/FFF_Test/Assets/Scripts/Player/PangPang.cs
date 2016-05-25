﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PangPang : MonoBehaviour {
    public bool TestingMode;
    public bool Oculus;
    public bool stackBullets;
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
    public GameObject spawnPoint2;
    public GameObject spawnPoint3;
    public GameObject bulletPrefab; // Object to spawn as bullet/projectile
    private List<GameObject> BulletList;
    public ChangeTattooAlpha TattoScript;
    private string ScriptPath;
   
    Animator anim;
    // Use this for initialization
    void Start () {
        if(!Oculus)
        {
            ScriptPath = "/PlayerStandard1/mainchar/arms";
        }
        else
        {
            ScriptPath = "/PlayerOculus_AlmostFinal/CenterEyeAnchor/mainchar/arms";
        }
        BulletList = new List<GameObject>();
        anim = GetComponent<Animator>();
        currentHeatMult = 0;
        TattoScript = GameObject.Find(ScriptPath).GetComponent<ChangeTattooAlpha>();
    }
	
	// Update is called once per frame
	void Update () {
        if(!TestingMode)
        {
            ShootAnimation();
        }
            if (TestingMode)
        { 
        ShootTesting();
        }
    }
    public void shotFired()
    {
        timeSinceLastFire = 0;
    }
    public void calculateHeat()
    {
        currentHeatMult += overHeatMult;
        float cHeat = overHeatTick + currentHeatMult;
        currentHeatLevel += cHeat;
    }
    public void stuff()
    {
        spawnBullet();
        fireBullet();
        shotFired();
        calculateHeat();
        UpdateTargetTattooAlpha();
        overHeatControl();
    }
    public void overHeatControl()
    {
        if (currentHeatLevel >= overHeatMax)
        {
            anim.SetBool("OverHeated", true);
            overHeated = true;
        }
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
                if(!stackBullets)
                { 
                fireBullet();
                }
                fireTimer = 0;
                timeSinceLastFire = 0;
                // Add heat to the weapon
                currentHeatMult += overHeatMult;
                float cHeat = overHeatTick + currentHeatMult;
                currentHeatLevel += cHeat;

                UpdateTargetTattooAlpha();
                if (currentHeatLevel >= overHeatMax)
                {
                   
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
                if (!overHeated)
                {

                    UpdateTargetTattooAlpha();
                }
                
                if (currentHeatLevel < 0)
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
        if (!overHeated && Input.GetAxis("Fire1") >= 0.2f)
        {
            anim.SetBool("FireStart", true);
            anim.SetBool("FireIsPressed", true);
        }
        else {
                   
            anim.SetBool("FireIsPressed", false);
            anim.SetBool("FireStart", false);
            
        }

        coolingSystem();

    }
    private void coolingSystem()
    {
        timeSinceLastFire += Time.deltaTime;
        // Enought Time has passed for the weapon to start cooling down
        if (timeSinceLastFire > timeUntilDecrease)
        {
            // Reset heat multiplier and decrease the heat on the hands because they are on FIRAY
            currentHeatMult = 0;
            currentHeatLevel -= coolingTick * Time.deltaTime;
            if (!overHeated)
            {

                UpdateTargetTattooAlpha();
            }

            if (currentHeatLevel < 0)
            {
                currentHeatLevel = 0;
            }
        }
        if (overHeated && currentHeatLevel <= 0)
        {
            overHeated = false;
            anim.SetBool("OverHeated", false);
            TattoScript.setFadeInTattoo();
        }
    }
    public void spawnBullet()
    {
        GameObject ThaBullet = Instantiate(bulletPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;
        ThaBullet.transform.parent = this.gameObject.transform;
        BulletList.Add(ThaBullet);
    }
    public void spawnBullet2()
    {
        GameObject ThaBullet = Instantiate(bulletPrefab, spawnPoint2.transform.position, transform.rotation) as GameObject;
        ThaBullet.transform.parent = this.gameObject.transform;
        BulletList.Add(ThaBullet);
    }
    public void spawnBullet3()
    {
        GameObject ThaBullet = Instantiate(bulletPrefab, spawnPoint3.transform.position, transform.rotation) as GameObject;
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
           
            Destroy(ThaBullet, bulletLifespan);
            ThaBullet.transform.parent = null;
            // Add force as impulse instead of continous force!
            Vector3 bulletDirectipon = spawnPoint.transform.forward;
            ThaBullet.GetComponent<Rigidbody>().AddForce(bulletDirectipon * bulletSpeed);
        }
    }

    private void UpdateTargetTattooAlpha()
    {
        float heatPerc = currentHeatLevel / overHeatMax;
        if (heatPerc > 1f)
        {
            heatPerc = 1f;
        }
        heatPerc = 1f - heatPerc;
        //TattoScript.setTargetAlpha(heatPerc);
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