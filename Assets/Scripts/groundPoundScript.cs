using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundPoundScript : MonoBehaviour
{

    public Vector3 groundPoundMovement;
    public float timer;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

            GetComponent<Rigidbody2D>().AddForce(groundPoundMovement);

        timer += Time.deltaTime;


        if (timer >= 2)
        {

            Destroy(gameObject);


        }


    }
}
