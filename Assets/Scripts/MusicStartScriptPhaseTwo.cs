using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicStartScriptPhaseTwo : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject boss;
    public bossScript bossCurrentPhase;

    public Vector3 Musicposition;

    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
        bossCurrentPhase = boss.GetComponent<bossScript>();


    }

    // Update is called once per frame
    void Update()
    {

        GetComponent<Transform>().position = Musicposition;



    }
}
