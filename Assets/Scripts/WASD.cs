using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WASD : MonoBehaviour
{
    SpriteRenderer mySprite;

    //speed is our public mod for the direction input
    public float speed = 1f;
    public Vector3 jumpForce;
    public Animator myAnimator;
    Rigidbody2D myRB;
    Vector3 dir = new Vector3(0, 0, 0);

    public bossScript bossFallAttack;
    public GameObject boss;

    public int playerhealth;
    public int mana;

    public Slider healthBar;
    public Slider manaBar;
    public float manaRecharge;

    public GameObject fireball2;
    public Vector3 fireball2Placement;
    public GameObject bullet;
    public Vector3 bulletOffset;

    public bool canJump;
    public bool direction;
    public Vector3 playerPosition;

    public GameObject GameManager;

    public bool isInvincible;
    public float invincibilityTimer;

    public bool activatedPhaseTwo;
    // Start is called before the first frame update

    public enum PlayerState
    {
        IDLE,
        DAMAGED,
        RUNNING
    }
    public PlayerState playerStateMachine;

    void Start()
    {
        playerhealth = 3;
        mana = 12;

        playerStateMachine = PlayerState.IDLE;
        myAnimator = GetComponent<Animator>();
        mySprite = GetComponent<SpriteRenderer>();
        bossFallAttack = boss.GetComponent<bossScript>();

        isInvincible = false;

    }
    // Update is called once per frame
    void Update()
    {
        switch (playerStateMachine)
        {

            case PlayerState.IDLE:
                myAnimator.SetBool("isDamaged", false);
                break;
            case PlayerState.DAMAGED:
                myAnimator.SetBool("isDamaged", true);
                break;

        }

        healthBar.value = playerhealth;
        manaBar.value = mana;

        if (playerhealth <= 0)
        {

            Destroy(gameObject);


        }

        if (mana <= 12)
        {

            manaRecharge += Time.deltaTime /2;

            if (manaRecharge >= 1)
            {

                mana += 1;
                manaRecharge = 0;

            }


        }

        if (invincibilityTimer > 0)
        {
            playerStateMachine = PlayerState.DAMAGED;
            isInvincible = true;
            invincibilityTimer -= Time.deltaTime;

        }
        if (invincibilityTimer <= 0)
        {
            playerStateMachine = PlayerState.IDLE;
            isInvincible = false;
            invincibilityTimer = 0;

        }

        playerPosition = GetComponent<Transform>().position;

        dir = Direction();
        if (dir != Vector3.zero)
        {
            playerStateMachine = PlayerState.RUNNING;
        }


        switch (playerStateMachine)
        {
            case PlayerState.IDLE:
                myAnimator.SetBool("isIdle", true);
                break;

            case PlayerState.RUNNING:
                //Debug.Log("desired dir based off player input: " + dir);
                transform.Translate(dir * speed * Time.deltaTime);
                break;

        }



        if (bossFallAttack.attackChance == 2)
        {


            Instantiate(fireball2, GetComponent<Transform>().position + fireball2Placement, Quaternion.identity);
            bossFallAttack.attackChance = 0;

        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (mana > 0)
            {
                mana -= 1;
                if (direction == true)
                {

                    Instantiate(bullet, GetComponent<Transform>().position + bulletOffset, Quaternion.identity);


                }
                if (direction == false)
                {

                    Instantiate(bullet, GetComponent<Transform>().position - bulletOffset, Quaternion.identity);


                }

            }

        }


    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            canJump = true;
        }
        if (collision.gameObject.tag == "Platform")
        {
            canJump = true;
        }
        if (collision.gameObject.tag == "PhaseTwoPlatform")
        {
            canJump = true;

        }
        if (collision.gameObject.tag == "Lava")
        {
            canJump = true;
        }



        if (collision.gameObject.tag == "Boss")
        {
            if (isInvincible == false)
            {

                invincibilityTimer = 1;

            }
        }




    }

    void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.tag == "Hazard")
        {
            if (isInvincible == false)
            {
                playerhealth -= 1;
                invincibilityTimer = 5;

            }
        }

        if (collision.gameObject.tag == "BossPhaseTwo")
        {
            if (isInvincible == false)
            {
                playerhealth -= 1;
                invincibilityTimer = 5;

            }
        }

        if (collision.gameObject.tag == "Lava")
        {
            playerhealth -= 1;
            invincibilityTimer = 5;
            GetComponent<Rigidbody2D>().AddForce(jumpForce);
        }


        if (bossFallAttack.BossPhaseMachine == bossScript.BossPhase.PHASETWObetween)
        {

            if (collision.gameObject.tag == "PhaseTwoPlatform")
            {
                bossFallAttack.BossPhaseMachine = bossScript.BossPhase.PHASETWO;

                if (activatedPhaseTwo == false)
                {
                    activatedPhaseTwo = true;
                    bossFallAttack.BossPhaseMachine = bossScript.BossPhase.PHASETWOinProgress;

                }


            }


        }


    }

    void OnCollisionExit2D(Collision2D collision)
    {

        canJump = false;

    }


    //--------------------------------- THIS METHOD DOES INPUT CHECKS FOR WASD -----------------------------------------
    //----------------------------------------------------------------------------------------------------------------
    Vector3 Direction()
    {
        //temp vector to hold our direction
        Vector3 v = Vector3.zero;
        //check our Up/Down axis
        //else if so there's only one valid direction at a time
        if (Input.GetKeyDown(KeyCode.W))
        { 
        
            if (canJump == true)
            {


                GetComponent<Rigidbody2D>().AddForce(jumpForce);


            }

        
        }

        //now do our left/right
        if (Input.GetKey(KeyCode.D))
        { 
            
            v += Vector3.right;
            mySprite.flipX = true;
            direction = true;

        }
        else if (Input.GetKey(KeyCode.A))
        { 
            
            v += Vector3.left;
            mySprite.flipX = false;
            direction = false;

        }



        
        //return our desired direction after all WASD checks  
        return v;
    }
}