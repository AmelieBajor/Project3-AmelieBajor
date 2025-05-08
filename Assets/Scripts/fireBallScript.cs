using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBallScript : MonoBehaviour
{
    public GameObject boss;
    public bossScript bossDir;
    public Vector3 fireBallMovement;
    public float timer;


    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
        bossDir = boss.GetComponent<bossScript>();

        if (bossDir.direction == true)
        {

            GetComponent<Rigidbody2D>().AddForce(fireBallMovement);


        }

        else if (bossDir.direction == false)
        {

            GetComponent<Rigidbody2D>().AddForce(-fireBallMovement);


        }


    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;



        if (timer >= 5)
        {
            Destroy(gameObject);


        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }

    }

}
