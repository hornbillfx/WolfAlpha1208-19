﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PowerUp : MonoBehaviour
{
    public bool canAttack = false;
    public Animator animator;
    public Button attackButton;
    public float throwableSpeed = 500.0f;

    public GameObject shuriken;
    public PhotonView pv;
    public GameObject SentBy;

    private void Start()
    {
      //  animator = GetComponent<Animator>();
    //    attackButton = GetComponent<PlayerMovement>().manage.attackBtn;
     //   attackButton.onClick.AddListener(() => Attack());
    }

    private void Update()
    {

        //  attackButton.GetComponent<Button>().enabled = canAttack;
        //  attackButton.GetComponent<Image>().enabled = canAttack;  
        if (power == Power.ShurikenAttack)
        {
            Vector3 move = transform.InverseTransformDirection(Vector3.right);
            transform.Translate(move);
        }
    }


    public void Attack()
    {
        animator.SetTrigger("Attack");
        canAttack = false;

        float move = GetComponent<PlayerMovement>().runSpeed;
        move *= .1f;
        if(pv.IsMine)
        {
            GameObject throwable = PhotonNetwork.Instantiate(shuriken.name, this.transform.position, Quaternion.identity);
            throwable.GetComponent<ThrowWeapon>().mine = this.gameObject;
            attackButton.GetComponent<Button>().enabled = false;
            attackButton.GetComponent<Image>().enabled = false;
        }
        //  GameObject throwable = Instantiate(shuriken, this.GetComponent<Transform>(), false);
        Debug.Log(move);
        

    }
    public enum Power { SpeedRun, Shuriken, ShurikenAttack };
    public Power power;
    public List<GameObject> collectedCharacter = new List<GameObject>();
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //   Debug.Log("Triggered");
        if(power==Power.ShurikenAttack)
        {
            if (collision.tag == "Player")
            {
                if(collision.gameObject!=SentBy)
                {
                    Debug.LogError(collision.name);
                    //   collision.gameObject.GetComponent<PlayerMovement>().pv.RPC("PlayerPunished", RpcTarget.AllBuffered, null);
                    collision.gameObject.GetComponent<PlayerMovement>().PlayerPunished();
                    //   StartCoroutine(PlayerPunished(collision.gameObject.GetComponent<PlayerMovement>()));
                }
            }
        }
        if (collision.tag == "Player")
        {

            if (!collectedCharacter.Contains(collision.gameObject))
            {
                if (power == Power.SpeedRun)
                {
                    if (collision.gameObject.GetComponent<PlayerMovement>().pv.IsMine)
                        Manager.manage.attackBtn.gameObject.SetActive(true);
                    Manager.manage.ShurikenBtn.gameObject.SetActive(false);


                }
                else if (power == Power.Shuriken)
                {
                    if(collision.gameObject.GetComponent<PlayerMovement>().pv.IsMine)
                         Manager.manage.ShurikenBtn.gameObject.SetActive(true);
                    Manager.manage.attackBtn.gameObject.SetActive(false);

                }

                //  StartCoroutine(collision.GetComponent<PlayerMovement>().SpeedUp(collision.gameObject));
                //  collectedCharacter.Add(collision.gameObject);
            }

        }
        if (collision.tag == "Shuriken")
        {
          //  canAttack = true;
           // GameObject collide = collision.gameObject;
          //  collide.SetActive(false);
        }
    }
}
