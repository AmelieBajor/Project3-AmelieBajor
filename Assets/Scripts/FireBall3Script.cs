using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall3Script : MonoBehaviour
{

    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 2)
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



        if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);



        }

    }
}
