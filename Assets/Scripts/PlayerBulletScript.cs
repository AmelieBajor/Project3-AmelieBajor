using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{
    public Vector3 bulletMovement;
    public float timer;

    public GameObject player;
    public WASD playerDir;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerDir = player.GetComponent<WASD>();


        if (playerDir.direction == true)
        {

            GetComponent<Rigidbody2D>().AddForce(bulletMovement);


        }

        else if (playerDir.direction == false)
        {

            GetComponent<Rigidbody2D>().AddForce(-bulletMovement);


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
        if (collision.gameObject.tag == "Boss")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Hazard")
        {
            Destroy(gameObject);
        }

        if(collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);



        }

    }

}
