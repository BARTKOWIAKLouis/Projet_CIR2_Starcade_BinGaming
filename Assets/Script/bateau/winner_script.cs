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
using UnityEngine.InputSystem.Controls;
using System.Timers;
using TMPro;

public class winner_script : MonoBehaviour
{


    public Rigidbody2D body;
    public CapsuleCollider2D bc;
    public float move_distance = 5;


    public bool left_moving = false;
    public bool right_moving = false;

    public SpriteRenderer my_sprite;
    public Sprite[] sprite_mvt;

    public float delais_anim;
    public float timer_anim;
    public int anim_actuelle_LR;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if (left_moving)
        {
            body.velocity = new UnityEngine.Vector2(-move_distance, body.velocity.y);
        }
        if (right_moving)
        {
            body.velocity = new UnityEngine.Vector2(move_distance, body.velocity.y);
        }


        // stoping velocity
        if (!bc.IsTouchingLayers() && !left_moving && !right_moving)
        {
            body.velocity = new UnityEngine.Vector2(0, body.velocity.y);
        }

        if (!left_moving && !right_moving)
        {
            my_sprite.sprite = sprite_mvt[0];
        }



        if (timer_anim > delais_anim)
        {
            if (anim_actuelle_LR == 1)
            {
                my_sprite.sprite = sprite_mvt[5];
                anim_actuelle_LR = 2;
            }
            else if (anim_actuelle_LR == 2)
            {
                my_sprite.sprite = sprite_mvt[4];
                anim_actuelle_LR = 1;
            }
            else if (anim_actuelle_LR == 3)
            {
                my_sprite.sprite = sprite_mvt[7];
                anim_actuelle_LR = 4;
            }
            else if (anim_actuelle_LR == 4)
            {
                my_sprite.sprite = sprite_mvt[6];
                anim_actuelle_LR = 3;
            }


            timer_anim = 0;
        }
        else
        {
            timer_anim += Time.deltaTime;
        }



    }




    //public void left(InputAction.CallbackContext context)
    public void left(string context)
    {
        if (context == "on")
        {

            //my_sprite.sprite = sprite_mvt[4];

            timer_anim = 0;
            anim_actuelle_LR = 1;


            left_moving = true;
            right_moving = false;
            //body.velocity = new UnityEngine.Vector2(-move_distance, body.velocity.y);
        }

        if (context == "off")
        {

            if (anim_actuelle_LR == 1 || anim_actuelle_LR == 2)
            {
                anim_actuelle_LR = 0;
            }

            left_moving = false;
            //body.velocity = new UnityEngine.Vector2(0, body.velocity.y);
        }

    }



    //public void right(InputAction.CallbackContext context)
    public void right(string context)
    {
        if (context == "on")
        {
            //my_sprite.sprite = sprite_mvt[6];

            timer_anim = 0;
            anim_actuelle_LR = 3;

            right_moving = true;
            left_moving = false;
            //body.velocity = new UnityEngine.Vector2(move_distance, body.velocity.y);
        }

        if (context == "off")
        {
            if (anim_actuelle_LR == 3 || anim_actuelle_LR == 4)
            {
                anim_actuelle_LR = 0;
            }


            right_moving = false;
            //body.velocity = new UnityEngine.Vector2(0, body.velocity.y);
        }

    }



    public void set_sprite_deplacement(Sprite[] tmp_sprite)
    {
        sprite_mvt = tmp_sprite;
    }



}
