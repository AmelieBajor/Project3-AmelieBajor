using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossScript : MonoBehaviour
{

    public Vector3 bossMoveLeft;
    public Vector3 bossMoveRight;
    public Vector3 bossMoveJump;
    public Vector3 bossMoveWait;

    public int movementChance;
    public int attackChance;


    public float bossMoveTimer;
    public float bossAttackTimer;
    public bool direction;

    public GameObject player;
    public GameObject fireball;
    public WASD playerPlacement;
    public Vector3 bossPlacement;

    public Vector3 bossFacedRight;
    public Vector3 bossFacedLeft;
    public Vector3 FireBallPlacementOne;

    public enum BossPhase
    {
        PHASEONE,
        PHASETWO,
        PHASETHREE
    }

    // Start is called before the first frame update
    void Start()
    {
        playerPlacement = player.GetComponent<WASD>();

    }

    // Update is called once per frame
    void Update()
    {
        bossMoveTimer += Time.deltaTime;
        bossAttackTimer += Time.deltaTime;

        bossPlacement = GetComponent<Transform>().position;

        //if player is right to boss
        if (playerPlacement.playerPosition.x >= bossPlacement.x)
        {
            direction = true;
            GetComponent<Transform>().localScale = bossFacedRight;
        }

        //player is left to boss
        else if (playerPlacement.playerPosition.x < bossPlacement.x)
        {

            direction = false;
            GetComponent<Transform>().localScale = bossFacedLeft;


        }


        if (bossAttackTimer >= 3)
        {

                if(direction == true)
                {
                    Instantiate(fireball, GetComponent<Transform>().position + FireBallPlacementOne, Quaternion.identity);


                }

                else if (direction == false)
                {
                    Instantiate(fireball, GetComponent<Transform>().position - FireBallPlacementOne, Quaternion.identity);
                }
   
            


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
            else if (movementChance == 5)
            {

                GetComponent<Rigidbody2D>().AddForce(bossMoveJump);

            }


            bossMoveTimer = 0;



        }


 



    }
}
