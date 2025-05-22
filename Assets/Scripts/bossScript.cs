using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bossScript : MonoBehaviour
{
    SpriteRenderer mySprite;

    public Vector3 bossMoveLeft;
    public Vector3 bossMoveRight;
    public Vector3 bossMoveJump;
    public Vector3 bossMoveWait;

    public Animator myAnimator;

    public CameraMoveScript cameraShakeEffect;
    public GameObject cameraObject;

    public GameObject inbetweenMusic;
    public GameObject phaseTwoMusic;

    public int movementChance;
    public int attackChance;

    public Slider healthBar;

    public float bossMoveTimer;
    public float bossAttackTimer;
    public bool direction;
    public bool resting;
    public int restingCount;
    public bool playerCanAttack;
    public float restingTimer;

    public int bossHealth;

    public AudioClip GroundPoundSound;
    AudioSource bossSource;

    public bool phaseTwoMusicActive;

    public GameObject player;
    public GameObject fireball;
    public GameObject groundPound;

    public WASD playerPlacement;

    public Vector3 bossPlacement;

    public Vector3 FireBallPlacement;
    public Vector3 groundPoundPlacement;
    public bool doingGroundPound;

    public bool phaseOneEnd;

    public enum BossPhase
    {
        PHASEONE,
        PHASETWObetween,
        PHASETWO,
        PHASETWOinProgress,
    }

    public enum BossAnimations
    {
     IDLE,
     JUMPING,
     ATTACKING,
     RESTING
    }


    public BossPhase BossPhaseMachine;
    public BossAnimations AnimationMachine;

    // Start is called before the first frame update
    void Start()
    {
        BossPhaseMachine = BossPhase.PHASEONE;
        bossHealth = 100;
        playerPlacement = player.GetComponent<WASD>();
        mySprite = GetComponent<SpriteRenderer>();
        cameraShakeEffect = cameraObject.GetComponent<CameraMoveScript>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (AnimationMachine)
        {

            case BossAnimations.IDLE:
                myAnimator.SetBool("isResting", false);
                break;
            case BossAnimations.RESTING:
                myAnimator.SetBool("isResting", true);
                break;

        }




        healthBar.value = bossHealth;
        if (BossPhaseMachine == BossPhase.PHASEONE)
        {


            bossMoveTimer += Time.deltaTime;
            bossAttackTimer += Time.deltaTime;

            bossPlacement = GetComponent<Transform>().position;

            //if player is right to boss
            if (playerPlacement.playerPosition.x >= bossPlacement.x)
            {
                direction = true;
                mySprite.flipX = true;
            }

            //player is left to boss
            else if (playerPlacement.playerPosition.x < bossPlacement.x)
            {

                direction = false;
                mySprite.flipX = false;


            }


            if (bossAttackTimer >= 3)
            {
                attackChance = Random.Range(1, 4);

                if (attackChance == 1)
                {
                    if (mySprite.flipX == true)
                    {
                        Instantiate(fireball, GetComponent<Transform>().position + FireBallPlacement, Quaternion.identity);


                    }

                    else if (mySprite.flipX == false)
                    {
                        Instantiate(fireball, GetComponent<Transform>().position - FireBallPlacement, Quaternion.identity);
                    }



                }

                if (attackChance == 3)
                {
                    GetComponent<Rigidbody2D>().AddForce(bossMoveJump);
                    doingGroundPound = true;


                }


                restingCount++;
                bossAttackTimer = 0;

            }

            if (bossMoveTimer >= 2)
            {

                movementChance = Random.Range(1, 6);
                if (movementChance == 1)
                {

                    GetComponent<Rigidbody2D>().AddForce(bossMoveLeft);


                }

                else if (movementChance == 2)
                {

                    GetComponent<Rigidbody2D>().AddForce(bossMoveLeft);


                }

                else if (movementChance == 3)
                {

                    GetComponent<Rigidbody2D>().AddForce(bossMoveRight);

                }

                else if (movementChance == 4)
                {

                    GetComponent<Rigidbody2D>().AddForce(bossMoveRight);

                }


                bossMoveTimer = 0;



            }
            if (restingCount >= 3)
            {
                AnimationMachine = BossAnimations.RESTING;
                playerCanAttack = true;

            }
            else
            {
                AnimationMachine = BossAnimations.IDLE;
                playerCanAttack = false;

            }

            if (restingCount >= 3)
            {

                restingTimer += Time.deltaTime;

                if (restingTimer >= 3)
                {

                    restingCount = 0;
                    restingTimer = 0;
                }


            }


        }


        if (bossHealth <= 50)
        {

            if (phaseOneEnd == false)
            {
                BossPhaseMachine = BossPhase.PHASETWObetween;
                Instantiate(inbetweenMusic);
                phaseOneEnd = true;

            }


        }

        if (BossPhaseMachine == BossPhase.PHASETWObetween)
        {

            GetComponent<Transform>().position -= bossMoveJump;

        }

        if (BossPhaseMachine == BossPhase.PHASETWOinProgress)
        {
            if (phaseTwoMusicActive == false)
            {

                Instantiate(phaseTwoMusic);
                phaseTwoMusicActive = true;
            }


        }


    }

        void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Bullet")
        {
            if (BossPhaseMachine == BossPhase.PHASEONE)
            {
                if (playerCanAttack == true)
                {

                    bossHealth -= 5;

                }



            }

        }

        if (collision.gameObject.tag == "Ground")
        {
            if (doingGroundPound == true)
            {

               Instantiate(groundPound, GetComponent<Transform>().position + groundPoundPlacement, Quaternion.identity);
                /*
                bossSource.clip = GroundPoundSound;
                bossSource.Play();
                */
                doingGroundPound = false;
                cameraShakeEffect.doingShake = true;
                

            }

        }


    }
}
