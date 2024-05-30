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


public class mvt_player : MonoBehaviour
{
    public GameObject fond;
    public GameObject shield_object;
    public Rigidbody2D body;
    public CapsuleCollider2D bc;
    public SpriteRenderer my_sprite;
    public float move_distance = 5;


    public bool left_moving = false;
    public bool right_moving = false;
    public bool up_moving = false;
    public bool down_moving = false;



    public float Ajustement_top = 0;
    public float Ajustement_bottom = 0;
    public float Ajustement_right = 0;
    public float Ajustement_left = 0;

    private float delta_time;

    public float boost_speed = 5;
    public float time_en_boost = 5/10;
    public float delais_use_boost;
    private float timer_boost ;
    private bool boosted = false;

    public bool shield;
    public float time_en_heavy = 5;
    public float delais_use_heavy;
    private float timer_heavy;

    public bool actionA;
    public bool actionB;

    public Sprite[] sprite_mvt;

    public float delais_anim;
    public float timer_anim;
    public int anim_actuelle_LR;
    public int anim_actuelle_HB;

    public bool border;

    // Start is called before the first frame update
    void Start()
    {
        timer_boost = time_en_boost;
        timer_heavy = time_en_heavy;
    }


    // Update is called once per frame
    void Update()
    {
        delta_time = Time.deltaTime;

        timer_boost += delta_time;
        timer_heavy += delta_time;

        if (timer_boost > time_en_boost && boosted)
        {
            move_distance -= boost_speed;

            boosted = false;
        }

        if (timer_heavy > time_en_heavy)
        {
            shield_object.SetActive(false);
            shield = false;
        }

        if (shield)
        {
            shield_object.SetActive(true);
        }



        if (left_moving)
        {
            body.velocity = new UnityEngine.Vector2(-move_distance, body.velocity.y);
        }
        if (right_moving) 
        {
            body.velocity = new UnityEngine.Vector2(move_distance, body.velocity.y);
        }
        if (up_moving)
        {
            body.velocity = new UnityEngine.Vector2(body.velocity.x, move_distance);
        }
        if (down_moving)
        {
            body.velocity = new UnityEngine.Vector2(body.velocity.x, -move_distance);
        }




        // stoping velocity
        if (!bc.IsTouchingLayers() && !left_moving && !right_moving)
        {
            body.velocity = new UnityEngine.Vector2(0 , body.velocity.y);
        }
        if (!bc.IsTouchingLayers() && !up_moving && !down_moving)
        {
            body.velocity = new UnityEngine.Vector2(body.velocity.x, 0);
        }
        
        if (!left_moving && !right_moving && !up_moving && !down_moving)
        {
            body.drag = 1;
        }
        else
        {
            body.drag = 0;
        }


        // restarting velocity
        if (!bc.IsTouchingLayers() && left_moving)
        {
            Debug.Log("wtf");
            body.velocity = new UnityEngine.Vector2(-move_distance , body.velocity.y);
        }
        if (!bc.IsTouchingLayers() && right_moving)
        {
            Debug.Log("wtf");
            body.velocity = new UnityEngine.Vector2(move_distance , body.velocity.y);
        }
        if (!bc.IsTouchingLayers() && down_moving)
        {
            Debug.Log("wtf");
            body.velocity = new UnityEngine.Vector2(body.velocity.x , - move_distance);
        }
        if (!bc.IsTouchingLayers() && up_moving)
        {
            Debug.Log("wtf");
            body.velocity = new UnityEngine.Vector2(body.velocity.x , move_distance);
        }



        if (timer_boost > delais_use_boost)
        {
            actionA = true;
        }
        else
        {
            actionA = false;
        }

        if (timer_heavy > delais_use_heavy)
        {
            actionB = true;
        }
        else
        {
            actionB = false;
        }


        if( !left_moving && !right_moving && !up_moving && !down_moving)
        {
            my_sprite.sprite = sprite_mvt[0];
        }


        if(timer_anim > delais_anim)
        {
            if(anim_actuelle_LR == 1)
            {
                my_sprite.sprite = sprite_mvt[5];
                anim_actuelle_LR = 2;
            }
            else if(anim_actuelle_LR == 2)
            {
                my_sprite.sprite = sprite_mvt[4];
                anim_actuelle_LR = 1;
            }
            else if(anim_actuelle_LR == 3)
            {
                my_sprite.sprite = sprite_mvt[7];
                anim_actuelle_LR = 4;
            }
            else if(anim_actuelle_LR == 4)
            {
                my_sprite.sprite = sprite_mvt[6];
                anim_actuelle_LR = 3;
            }



            if (anim_actuelle_HB == 1)
            {
                if (anim_actuelle_LR == 0)
                {
                    my_sprite.sprite = sprite_mvt[3];
                }
                anim_actuelle_HB = 2;
            }
            else if (anim_actuelle_HB == 2)
            {
                if (anim_actuelle_LR == 0)
                {
                    my_sprite.sprite = sprite_mvt[2];
                }
                anim_actuelle_HB = 1;
            }
            else if (anim_actuelle_HB == 3)
            {
                if (anim_actuelle_LR == 0)
                {
                    my_sprite.sprite = sprite_mvt[1];
                }
                anim_actuelle_HB = 4;
            }
            else if (anim_actuelle_HB == 4)
            {
                if (anim_actuelle_LR == 0)
                {
                    my_sprite.sprite = sprite_mvt[0];
                }
                anim_actuelle_HB = 3;
            }


            timer_anim = 0;
        }
        else
        {
            timer_anim += delta_time;
        }




    }




    //public void left(InputAction.CallbackContext context)
    public void left(string context)
    {
        if (context=="on") {

            //my_sprite.sprite = sprite_mvt[4];

            timer_anim = 0;
            anim_actuelle_LR = 1;


            left_moving = true;
            right_moving = false;
            //body.velocity = new UnityEngine.Vector2(-move_distance, body.velocity.y);
        }
        
        if (context=="off"){
           
            if( anim_actuelle_LR == 1 || anim_actuelle_LR == 2)
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
        if (context=="on")
        {
            //my_sprite.sprite = sprite_mvt[6];

            timer_anim = 0;
            anim_actuelle_LR = 3;

            right_moving = true;
            left_moving = false;
            //body.velocity = new UnityEngine.Vector2(move_distance, body.velocity.y);
        }
        
        if (context=="off")
        {
            if (anim_actuelle_LR == 3 || anim_actuelle_LR == 4)
            {
                anim_actuelle_LR = 0;
            }


            right_moving = false;
            //body.velocity = new UnityEngine.Vector2(0, body.velocity.y);
        }
        
    }

    //public void up(InputAction.CallbackContext context)
    public void up(string context)
    {
        if (context=="on")
        {
            if (anim_actuelle_LR == 0)
            {
                //my_sprite.sprite = sprite_mvt[2];
                timer_anim = 0;
            }

            anim_actuelle_HB = 1;

            up_moving = true;
            down_moving = false;
            //body.velocity = new UnityEngine.Vector2(body.velocity.x, move_distance);
        }

        if (context=="off")
        {
            if (anim_actuelle_HB == 1 || anim_actuelle_HB == 2)
            {
                anim_actuelle_HB = 0;
            }

            up_moving = false;
            //body.velocity = new UnityEngine.Vector2(body.velocity.x, 0);
        }

    }

    //public void down(InputAction.CallbackContext context)
    public void down(string context)
    {
        if (context=="on")
        {
            if (anim_actuelle_LR == 0)
            {
                //my_sprite.sprite = sprite_mvt[0];
                timer_anim = 0;
            }

            anim_actuelle_HB = 3;
            

            down_moving = true;
            up_moving = false;
           // body.velocity = new UnityEngine.Vector2(body.velocity.x, -move_distance);
        }

        if (context=="off")
        {
            if (anim_actuelle_HB == 3 || anim_actuelle_HB == 4)
            {
                anim_actuelle_HB = 0;
            }

            down_moving = false;
            //body.velocity = new UnityEngine.Vector2(body.velocity.x, 0);
        }

    }


    public void fonction_Act1(string context)
    //public void boost(InputAction.CallbackContext context)
    {
        if (context == "on" && timer_boost > delais_use_boost)
        {
            move_distance += boost_speed;

            timer_boost = 0;
            boosted = true;
        }
    }

    public void fonction_Act2(string context)
   // public void heavy(InputAction.CallbackContext context)
    {
        if (context == "on" && timer_heavy > delais_use_heavy)
        {
            //shield_object.SetActive(true);

            shield = true;

            timer_heavy = 0;
        }
    }



    private void OnTriggerEnter2D()
    {
        if (!shield)
        {
            Destroy(this.gameObject);
        }
    }


    public void set_sprite_deplacement(Sprite[] tmp_sprite)
    {
        sprite_mvt = tmp_sprite;
    }





}
