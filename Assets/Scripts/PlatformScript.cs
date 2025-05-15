using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    public GameObject boss;
    public bossScript bossCurrentPhase;

    public Vector3 platformPlacementOff;
    public Vector3 platformPlacementOn;


    // Start is called before the first frame update
    void Start()
    {

        boss = GameObject.FindGameObjectWithTag("Boss");
        bossCurrentPhase = boss.GetComponent<bossScript>();
        GetComponent<Transform>().position = platformPlacementOff;



    }

    // Update is called once per frame
    void Update()
    {

        if(bossCurrentPhase.BossPhaseMachine == bossScript.BossPhase.PHASETWObetween)
        {

            GetComponent<Transform>().position = platformPlacementOn;

        }

    }
}
