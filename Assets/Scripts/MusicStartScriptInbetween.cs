using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicStartScriptInbetween : MonoBehaviour
{
    public GameObject boss;
    public bossScript bossCurrentPhase;

    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
        bossCurrentPhase = boss.GetComponent<bossScript>();
    }

    // Update is called once per frame
    void Update()
    {

        if (bossCurrentPhase.BossPhaseMachine == bossScript.BossPhase.PHASETWOinProgress)
        {

            Destroy(gameObject);

        }



    }
}
