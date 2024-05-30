using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
//using static UnityEditor.Timeline.TimelinePlaybackControls;

public class movepalyer : MonoBehaviour
{
    public float speed  = 10f;
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
    
    // Start is called before the first frame update
    void Start()
    {
        
    }




    //public void Up(InputAction.CallbackContext context)
    public void up(string context)
    {

        if (context=="on")
        {
            body.velocity = new UnityEngine.Vector2(body.velocity.x, speed);
        }

        if (context=="off")
        {
            body.velocity = new UnityEngine.Vector2(body.velocity.x, 0);
        }
    }
    //public void Down(InputAction.CallbackContext context)
    public void down(string context)
    {
        if (context=="on")
        {
            body.velocity = new UnityEngine.Vector2(body.velocity.x, -speed);
        }

        if (context=="off")
        {
            body.velocity = new UnityEngine.Vector2(body.velocity.x, 0);
        }
    }

    //public void Right(InputAction.CallbackContext context)
    public void right(string context)
    {
        if (context=="on")
        {
            timer_anim = 0;
            anim_actuelle_LR = 1;
            
            sprite_renderer.sprite = sprite1;
            body.velocity = new UnityEngine.Vector2(speed, body.velocity.y);
            transform.rotation = Quaternion.identity;
        }

        if (context=="off")
        {
            anim_actuelle_LR = 0;
            body.velocity = new UnityEngine.Vector2(0, body.velocity.y);
        }
    }

    //public void Left(InputAction.CallbackContext context)
    public void left(string context)
    {
        if (context=="on")
        {
            timer_anim = 0;
            anim_actuelle_LR = 1;

            transform.rotation = new Quaternion(0, 90, 0, 0);
            sprite_renderer.sprite = sprite1;
            body.velocity = new UnityEngine.Vector2(-speed, body.velocity.y);
            

        }

        if (context=="off")
        {
            
            anim_actuelle_LR = 0;
            body.velocity = new UnityEngine.Vector2(0, body.velocity.y);
        }
    }
    // Update is called once per frame
    void Update()

    {
        // Restrict player movement within map boundaries
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);
        transform.position = clampedPosition;
        
        //if (false)
        //{
            transform.rotation = Quaternion.identity;
        //  }

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
