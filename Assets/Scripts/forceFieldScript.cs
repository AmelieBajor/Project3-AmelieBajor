using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forceFieldScript : MonoBehaviour
{

    public GameObject boss;
    public PhaseTwoScript bossHeadbutt;

    public Vector3 active;
    public Vector3 unactive;


    // Start is called before the first frame update
    void Start()
    {
        bossHeadbutt = boss.GetComponent<PhaseTwoScript>();

    }

    // Update is called once per frame
    void Update()
    {
        if (bossHeadbutt.headbutt == true)
        {

            GetComponent<Transform>().position = unactive;

        }
        if (bossHeadbutt.headbutt == false)
        {

            GetComponent<Transform>().position = active;

        }
    }
}
