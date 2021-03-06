﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunPoint : MonoBehaviour
{
    public GameObject player;
    public GameObject shootRotate;
    public ParticleSystem particles;
    public ParticleSystem shootParticle;
    public ParticleSystem smallShootParticle;
    public ParticleSystem explode;
    public float rmbCoolDownSpeed=0.2f;
    public float mmbCoolDownSpeed = 0.1f;
    public Image rmbCoolDownBar;
    public Image mmbCoolDownBar;
    private Rigidbody2D playerRigid;
    private float rmbCoolDown=0;
    private float mmbCoolDown=0;
    public float largeShootForce=10;
    public float smallShootForce = 10;
    private Vector2 mousePos;
    private Vector2 gunDir;
    private float timeSinceShot;
    public AudioSource shootSound;
    public AudioClip LMBSound;
    public AudioClip RMBSound;
    public AudioClip MMBSound;
    public AudioSource ting;
    public AudioSource coh;
    private bool tingedMMB;
    private bool tingedRMB;


    void Start()
    {
        playerRigid = player.GetComponent<Rigidbody2D>();
        particles.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        rmbCoolDown += Time.deltaTime * rmbCoolDownSpeed;
        rmbCoolDownBar.fillAmount = rmbCoolDown;
        mmbCoolDown += Time.deltaTime * mmbCoolDownSpeed;
        mmbCoolDownBar.fillAmount = mmbCoolDown;
      

        if (control.instance.end == false)
        {
            transform.position = player.transform.position;
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            gunDir = new Vector2(transform.position.x, transform.position.y) - mousePos;
            transform.up = -gunDir;
            timeSinceShot += Time.deltaTime;
            if (Input.GetMouseButtonDown(0)&& control.instance.paused==false)
            {
                Shoot(smallShootForce,smallShootParticle);
                control.instance.lmbAmount++;
                shootSound.clip = LMBSound;
                shootSound.Play();
            }

            if(rmbCoolDown >= 0.999)
            {
                rmbCoolDownBar.GetComponent<Image>().color = new Color(0.5781366f, 0.2122642f, 1, 1f);
                if (tingedRMB==false) {

                    tingedRMB = true;
                    ting.Play();
                }

                if (Input.GetMouseButtonDown(1) && control.instance.paused == false)
                {
                    Shoot(largeShootForce, shootParticle);
                    rmbCoolDown = 0;
                    control.instance.mmbamount++;

                    shootSound.clip = RMBSound;
                    shootSound.Play();
                }
                
            }
            else
            {
                tingedRMB = false;
                rmbCoolDownBar.GetComponent<Image>().color = new Color(0.5781366f, 0.2122642f, 1, 0.5f);
                if (Input.GetMouseButtonDown(1) && control.instance.paused == false)
                {
                    coh.Play();
                }
            }
         

            if (mmbCoolDown >= 0.999)
            {
                mmbCoolDownBar.GetComponent<Image>().color = new Color(0.73144f, 1, 0.2588235f, 1f);

                if (tingedMMB == false)
                {
                    tingedMMB = true;
                    ting.Play();
                }

                if (Input.GetMouseButtonDown(2) && control.instance.paused == false)
                {
                    explode.Play();
                    mmbCoolDown = 0;
                    control.instance.rmbamount++;
                    control.instance.Shake(0.3f, 0.2f);
                    shootSound.clip = MMBSound;
                    shootSound.Play();
                }

            }
            else
            {
                tingedMMB = false;
                mmbCoolDownBar.GetComponent<Image>().color = new Color(0.73144f, 1, 0.2588235f, 0.5f);
                if (Input.GetMouseButtonDown(2) && control.instance.paused == false)
                {
                    coh.Play();
                }
            }

            //if (Input.GetMouseButtonDown(0))
            //{
            //    particles.Play();
            //}

            //if (Input.GetMouseButtonUp(0))
            //{
            //    particles.Stop();
            //}
        }
    }

    //void FixedUpdate()
    //{

    //    if (Input.GetMouseButton(0)&& control.instance.end == false)
    //    {
    //        playerRigid.AddForce(gunDir.normalized * speed);
    //    }
    //}
    void Shoot(float force, ParticleSystem partSystem)
    {
        //timeSinceShot = 0;**********************************
        shootRotate.transform.up = -gunDir;
        partSystem.Play();
        playerRigid.AddForce(gunDir.normalized * force);
    }

}
