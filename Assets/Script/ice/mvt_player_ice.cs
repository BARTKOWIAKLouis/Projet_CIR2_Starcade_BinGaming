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
using UnityEngine.UIElements;


public class mvt_player_ice : MonoBehaviour
{
    public Sprite sprite_tir;

    public Rigidbody2D body;
    private UnityEngine.Vector2 tmp_mvt;
    public CapsuleCollider2D bc;
    public float speed = 1;
    public GameObject Tir;
    public float decalage;
    public float recul = 5 / 10;
    public float delay_tir;
    public float delay_frein;
    public float distance_pour_tir;
    private float delta_time = 0;
    private float timer = 0;
    private float timer2 = 0;

    public bool right_moving = false;
    public bool left_moving = false;
    public bool up_moving = false;
    public bool down_moving = false;

    public bool actionA;
    public bool actionB;

    private bool do_actionA;
    private bool do_actionB;

    // Start is called before the first frame update
    void Start()
    {
        tmp_mvt = new UnityEngine.Vector2();
        new WaitForSecondsRealtime(1);
    }


    // Update is called once per frame
    void Update()
    {
        delta_time = Time.deltaTime;

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
        if (!left_moving && ! right_moving)
        {
            transform.Rotate(new UnityEngine.Vector3(0, 0, 0));
            body.angularDrag = 1;
        }


        if (up_moving)
        {
            tmp_mvt = transform.up * delta_time; ;
            body.velocity = body.velocity + tmp_mvt;
        }
        if (down_moving)
        {
            tmp_mvt = -transform.up * delta_time; ;
            body.velocity = body.velocity + tmp_mvt;
        }



        if (delay_tir < timer)
        {
            actionA = true;
        }
        else
        {
            actionA = false;
        }

        if (delay_frein < timer2)
        {
            actionB = true;
        }
        else
        {
            actionB = false;
        }


        if (do_actionA)
        {
            do_actionA = false;

            GameObject clone1 = Instantiate(Tir, transform.position + transform.up * distance_pour_tir + transform.right * decalage, transform.rotation);
            GameObject clone2 = Instantiate(Tir, transform.position + transform.up * distance_pour_tir - transform.right * decalage, transform.rotation);
        }
        if (do_actionB)
        {
            do_actionB = false;

            body.velocity = body.velocity / 2;
        }



        timer += delta_time;
        timer2 += delta_time;

    }




    //public void left(InputAction.CallbackContext context)
    public void left(string context)
    {
        if (context=="on")
        {
            left_moving = true;
            //body.angularDrag = 0;
        }

        if (context=="off")
        {
            left_moving = false;
            //body.angularDrag = 1;
        }

    }



    //public void right(InputAction.CallbackContext context)
    public void right(string context)
    {
        if (context=="on")
        {
            right_moving = true;
            //body.angularDrag = 0;
        }

        if (context=="off")
        {
            right_moving = false;
            //body.angularDrag = 1;
        }

    }

    //public void up(InputAction.CallbackContext context)
    public void up(string context)
    {
        if (context=="on")
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
        if (context=="on")
        {
            down_moving = true;
        }
        if (context=="off")
        {
            down_moving = false;
        }
    }


    //public void fonction_Act1(InputAction.CallbackContext context)
    public void fonction_Act1(string context)
    {
        //if (context.started && delay_tir < timer)
        if (context == "on" && delay_tir < timer)
        {
            do_actionA = true;

            GameObject clone1 = Instantiate(Tir, transform.position + transform.up  * distance_pour_tir + transform.right * decalage, transform.rotation);
      

            GameObject clone2 = Instantiate(Tir, transform.position + transform.up * distance_pour_tir - transform.right * decalage, transform.rotation);
      

            tmp_mvt = -transform.up * recul; ;
            body.velocity = body.velocity + tmp_mvt;

            timer = 0;
        }
    }


    //public void fonction_Act2(InputAction.CallbackContext context)
    public void fonction_Act2(string context)
    {
        //if (context.started && delay_frein < timer2)
        if (context == "on" && delay_frein < timer2)
        {
            do_actionB = true;

            body.velocity = body.velocity / 2;
            timer2 = 0;
        }
    }


    public void color_lazer(GameObject tmp)
    {
        Tir = tmp;
    }




    private void OnTriggerExit2D()
    {
        Destroy(this.gameObject);
    }

}
