using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
//using static UnityEditor.Timeline.TimelinePlaybackControls;
public class menu_move_player : MonoBehaviour
{
    public float speed = 10f;
    public float minX = -5f;
    public float maxX = 5f;
    public float minY = -5f;
    public float maxY = 5f;
    public Rigidbody2D body;
    public SpriteRenderer sprite_renderer;

    public Sprite sprite_move;
    public Sprite sprite1;
    public Sprite sprite2;

    public float delais_anim;
    public float timer_anim;
    public int anim_actuelle_LR;

    public bool go_up;
    public bool stop_up;
    public bool go_down;
    public bool stop_down;
    public bool go_left;
    public bool stop_left;
    public bool go_right;
    public bool stop_right;


    // Start is called before the first frame update
    void Start()
    {

    }



    public void up(string context)
    //public void Up(InputAction.CallbackContext context)
    {
        
        if (context == "on")
        {
            go_up = true;
            body.velocity = new UnityEngine.Vector2(body.velocity.x, speed);
            
        }

        if (context == "off")
        {
            stop_up = true;
            body.velocity = new UnityEngine.Vector2(body.velocity.x, 0);
        }
    }
    public void down(string context)
    //public void Down(InputAction.CallbackContext context)
    {
        if (context == "on")
        {
            go_down = true;
            body.velocity = new UnityEngine.Vector2(body.velocity.x, -speed);
        }

        if (context == "off")
        {
            stop_down = true;
            body.velocity = new UnityEngine.Vector2(body.velocity.x, 0);
        }
    }

    public void right(string context)
    //public void Right(InputAction.CallbackContext context)
    {
        if (context == "on")
        {
            go_right = true;
            timer_anim = 0;
            anim_actuelle_LR = 1;

            sprite_renderer.sprite = sprite1;
            body.velocity = new UnityEngine.Vector2(speed, body.velocity.y);
            transform.rotation = Quaternion.identity;
        }

        if (context == "off")
        {
            stop_right = true;
            anim_actuelle_LR = 0;
            body.velocity = new UnityEngine.Vector2(0, body.velocity.y);
        }
    }

    public void left(string context)
    //public void Left(InputAction.CallbackContext context)
    {
        if (context == "on")
        {
            go_left = true;
            timer_anim = 0;
            anim_actuelle_LR = 1;

            transform.rotation = new Quaternion(0, 90, 0, 0);
            sprite_renderer.sprite = sprite1;
            body.velocity = new UnityEngine.Vector2(-speed, body.velocity.y);


        }

        if (context == "off")
        {
            stop_left = true;
            anim_actuelle_LR = 0;
            body.velocity = new UnityEngine.Vector2(0, body.velocity.y);
        }
    }
    // Update is called once per frame
    void Update()

    {
        if (go_up)
        {
            go_up = false;
            //if(body.velocity.y != speed)
            //{
            body.velocity = new UnityEngine.Vector2(body.velocity.x,  speed);
            //}
            
        }
        if (stop_up)
        {
            stop_up = false;
            //if (body.velocity.y == speed) { 
            body.velocity = new UnityEngine.Vector2(body.velocity.x, 0);
            //}

      
        }

        if (go_down)
        {
            go_down = false;
            //if (body.velocity.y != -speed)
            //{
            body.velocity = new UnityEngine.Vector2(body.velocity.x,  - speed);
            //}
        }
        if (stop_down)
        {
            stop_down = false;
            body.velocity = new UnityEngine.Vector2(body.velocity.x, 0);
            
        }

        if (go_right)
        {
            go_right = false;
            transform.rotation = Quaternion.identity;
            body.velocity = new UnityEngine.Vector2(speed, body.velocity.y);
            
        }
        if (stop_right)
        {
            stop_right = false;
            body.velocity = new UnityEngine.Vector2(0, body.velocity.y);
            
        }

        if (go_left)
        {
            go_left = false;
            transform.rotation = new Quaternion(0, 90, 0, 0);
            body.velocity = new UnityEngine.Vector2(- speed, body.velocity.y);
        }
        if (stop_left)
        {
            stop_left = false;
            body.velocity = new UnityEngine.Vector2(0, body.velocity.y);
            
        }

        // Restrict player movement within map boundaries
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);
        transform.position = clampedPosition;

       
        transform.rotation = Quaternion.identity;
   

        if (timer_anim > delais_anim)
        {
            if (anim_actuelle_LR == 1)
            {

                sprite_renderer.sprite = sprite1;
                anim_actuelle_LR = 2;
            }
            else if (anim_actuelle_LR == 2)
            {

                sprite_renderer.sprite = sprite2;
                anim_actuelle_LR = 1;
            }


            timer_anim = 0;
        }
        else
        {
            timer_anim += Time.deltaTime;
        }

    }
}
