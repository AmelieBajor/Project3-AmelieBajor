using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveScript : MonoBehaviour
{

    public float cameraShakeX;

    public bool doingShake;
    public float shakeTimer;
    public Vector3 cameraPosition;

    public bossScript phase;
    public GameObject boss;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Transform>().position = cameraPosition;
        doingShake = false;
        phase = boss.GetComponent<bossScript>();
    }

    // Update is called once per frame
    void Update()
    {

        cameraPosition.x = cameraShakeX;
        cameraPosition.y = GetComponent<Transform>().position.y;
        GetComponent<Transform>().position = cameraPosition;


        if (doingShake == true)
        {
            shakeTimer += Time.deltaTime;
            
            if (shakeTimer >= 0.1 && shakeTimer <= 0.15)
            {
                cameraShake();

            }
            if (shakeTimer >= 0.2 && shakeTimer <= 0.25)
            {
                cameraShake();

            }
            if (shakeTimer >= 0.3 && shakeTimer <= 0.35)
            {
                cameraShake();

            }
            if (shakeTimer >= 0.4 && shakeTimer <= 0.45)
            {
                cameraShake();

            }
            if (shakeTimer >= 0.5 && shakeTimer <= 0.55)
            {
                cameraShakeX = 0;
                shakeTimer = 0;
                doingShake = false;

            }



        }

    }


    void cameraShake()
    {

        cameraShakeX = Random.Range(-1, 1);

    }
}
