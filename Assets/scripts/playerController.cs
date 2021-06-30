using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    //main player values
    [Header("player stats")]
    public int speed;
    public int jumpPower;

    //private variables for the engine
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        //assigning the animator component
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixuedUpdate()
    {
        
    }
}
