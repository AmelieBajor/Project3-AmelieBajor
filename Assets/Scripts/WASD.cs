using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASD : MonoBehaviour
{
    //speed is our public mod for the direction input
    public float speed = 1f;
    public Vector3 jumpForce;
    public Animator myAnimator;
    Rigidbody2D myRB;
    Vector3 dir = new Vector3(0, 0, 0);

    public bool canJump;
    public GameObject Fireball;
    public Vector3 playerPosition;

    // Start is called before the first frame update

    public enum PlayerState
    {
        RUNNING,
        IDLE,
        FALLING
    }
    public PlayerState playerStateMachine;

    void Start()
    {
        playerStateMachine = PlayerState.FALLING;
        myAnimator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {

        playerPosition = GetComponent<Transform>().position;

        dir = Direction();
        if (dir != Vector3.zero)
        {
            playerStateMachine = PlayerState.RUNNING;
        }
        else
        {
            playerStateMachine = PlayerState.IDLE;
        }


        switch (playerStateMachine)
        {
            case PlayerState.IDLE:
                myAnimator.SetBool("isIdle", true);
                break;

            case PlayerState.RUNNING:
                myAnimator.SetBool("isIdle", false);
                //Debug.Log("desired dir based off player input: " + dir);
                transform.Translate(dir * speed * Time.deltaTime);
                break;

            case PlayerState.FALLING:
                break;
        }


    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            canJump = true;
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
        else if (Input.GetKey(KeyCode.S))
        { 
            
            v += Vector3.down; 
        
        }

        //now do our left/right
        if (Input.GetKey(KeyCode.D))
        { 
            
            v += Vector3.right;
        
        }
        else if (Input.GetKey(KeyCode.A))
        { 
            
            v += Vector3.left;
        
        }
        
        //return our desired direction after all WASD checks  
        return v;
    }
}