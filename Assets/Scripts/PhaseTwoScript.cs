using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseTwoScript : MonoBehaviour
{

    public Vector3 movementLeft;
    public Vector3 movementRight;
    public Vector3 movementUp;

    public GameObject fireballLeft;
    public GameObject fireballRight;

    public bool headbutt;
    public Vector3 originalPosition;
    public Vector3 unactivePosition;

    public float movementTimer;
    public float movementChance;

    public float headbuttTimer;

    public float moveCount;

    public AudioClip groundPoundSound;
    AudioSource bossSource;

    public GameObject boss;
    public bossScript phase;

    public bool inAir;

    public CameraMoveScript cameraShakeEffect;
    public GameObject cameraObject;

    public Animator myAnimator;

    public enum bossAttackState
    {
        ROAMING,
        HEADBUTT


    }

    public bossAttackState BossAttackMachine;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Transform>().position = originalPosition;
        phase = boss.GetComponent<bossScript>();

        bossSource = GetComponent<AudioSource>();

        cameraShakeEffect = cameraObject.GetComponent<CameraMoveScript>();

    }

    // Update is called once per frame
    void Update()
    {



        if (phase.BossPhaseMachine == bossScript.BossPhase.PHASETWOinProgress)
        {



            switch (BossAttackMachine)
            {

                case bossAttackState.ROAMING:
                    myAnimator.SetBool("isAttacking", false);
                    break;
                case bossAttackState.HEADBUTT:
                    myAnimator.SetBool("isAttacking", true);
                    break;

            }


            if (headbutt == true)
            {

                BossAttackMachine = bossAttackState.HEADBUTT;

            }

            else if (headbutt == false)
            {

                BossAttackMachine = bossAttackState.ROAMING;

            }

            phase.restingTimer = 5;
            movementTimer += Time.deltaTime;
            if (movementTimer >= 2)
            {

                movementChance = Random.Range(1, 4);

                if (movementChance == 1)
                {
                    GetComponent<Rigidbody2D>().AddForce(movementLeft);

                    inAir = true;

                }

                if (movementChance == 2)
                {
                    GetComponent<Rigidbody2D>().AddForce(movementRight);
                    inAir = true;

                }


                if (movementChance == 3)
                {
                    GetComponent<Rigidbody2D>().AddForce(movementUp);
                    inAir = true;

                }



                movementChance = 0;
                moveCount++;
                movementTimer = 0;
            }

            if (moveCount >= 5)
            {
                headbutt = true;
                headbuttTimer += Time.deltaTime;
                if (headbuttTimer >= 3)
                {
                    GetComponent<Transform>().position = originalPosition;
                    headbutt = false;         
                   headbuttTimer = 0;

                    moveCount = 0;

                }




            }

        }

        if (phase.bossHealth <= 0)
        {

            Destroy(gameObject);


        }



    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Bullet")
        {
            if (phase.BossPhaseMachine == bossScript.BossPhase.PHASETWOinProgress)
            {

                phase.bossHealth -= 1;


            }

        }

        if (collision.gameObject.tag == "ForceField")
        {
            if (inAir == true)
            {

                
                bossSource.clip = groundPoundSound;
                bossSource.Play();
                
                inAir = false;
                cameraShakeEffect.doingShake = true;


            }

        }


    }

}
