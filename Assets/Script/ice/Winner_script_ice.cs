using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.InputSystem;
//using static UnityEditor.Timeline.TimelinePlaybackControls;
using Unity.Mathematics;
using TMPro;


public class Winner_script_ice : MonoBehaviour
{
    public Sprite sprite_tir;

    public Rigidbody2D body;
    private UnityEngine.Vector2 tmp_mvt;
    public CapsuleCollider2D bc;


    public bool right_moving = false;
    public bool left_moving = false;
    public bool up_moving = false;
    public bool down_moving = false;
    public float vitesse;
    private float delta_time;

    // Start is called before the first frame update
    void Start()
    {
        tmp_mvt = new UnityEngine.Vector2();
    }


    // Update is called once per frame
    void Update()
    {
        delta_time = Time.deltaTime;
        body.angularDrag = 1;
        if (right_moving)
        {
            transform.Rotate(new UnityEngine.Vector3(0, 0, -100 * delta_time));
            body.angularDrag = 0;

        }
        if (left_moving)
        {
            transform.Rotate(new UnityEngine.Vector3(0, 0, 100 * delta_time));
            body.angularDrag = 0;
        }


        if (up_moving)
        {
            tmp_mvt = transform.up * delta_time; ;
            body.velocity = body.velocity + tmp_mvt * vitesse;
        }
        if (down_moving)
        {
            tmp_mvt = -transform.up * delta_time; ;
            body.velocity = body.velocity + tmp_mvt * vitesse;
        }
    }




    //public void left(InputAction.CallbackContext context)
    public void left(string context)
    {
        if (context == "on")
        {
            left_moving = true;
            //body.angularDrag = 0;
        }

        if (context == "off")
        {
            left_moving = false;
            //body.angularDrag = 1;
        }

    }



    //public void right(InputAction.CallbackContext context)
    public void right(string context)
    {
        if (context == "on")
        {
            right_moving = true;
            //body.angularDrag = 0;
        }

        if (context == "off")
        {
            right_moving = false;
            //body.angularDrag = 1;
        }

    }

    //public void up(InputAction.CallbackContext context)
    public void up(string context)
    {
        if (context == "on")
        {
            up_moving = true;
        }
        if (context=="off")
        {
            up_moving = false;
        }

    }

    //public void down(InputAction.CallbackContext context)
    public void down(string context)
    {
        if (context == "on")
        {
            down_moving = true;
        }
        if (context =="off")
        {
            down_moving = false;
        }
    }

  

    

}
