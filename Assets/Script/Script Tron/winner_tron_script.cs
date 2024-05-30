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

public class winner_tron_script : MonoBehaviour
{
    public BoxCollider2D Bc;

    public int direction_Moto;
    public float vitesse;
    public bool mvt_winner;
    private float timer = 0;
    public int tmp_lancement;

    public bool left_moving = false;
    public bool right_moving = false;

    private float delta_time;
    

    // Start is called before the first frame update
    void Start()
    {
        direction_Moto = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > vitesse + tmp_lancement && mvt_winner)
        {

            tmp_lancement = 0;
            if (direction_Moto == 0)
            {
                // Orientation haut
                transform.rotation = UnityEngine.Quaternion.Euler(0, 0, 0);

                // Mouvement haut
                transform.position = transform.position + UnityEngine.Vector3.up / 5 ;
            }
            if (direction_Moto == 1)
            {
                right_moving = false;
                // Orientation droite
                transform.rotation = UnityEngine.Quaternion.Euler(0, 0, 270);

                // Mouvement droite
                transform.position = transform.position + UnityEngine.Vector3.right / 5;
            }
            if (direction_Moto == 2)
            {
                // Orientation bas
                transform.rotation = UnityEngine.Quaternion.Euler(0, 0, 180);

                // Mouvement bas
                transform.position = transform.position + UnityEngine.Vector3.down / 5;
            }
            if ( direction_Moto == 3)
            {
                left_moving = false;
                // Orientation gauche
                transform.rotation = UnityEngine.Quaternion.Euler(0, 0, 90);

                // Mouvement gauche
                transform.position = transform.position + UnityEngine.Vector3.left /5;
            }

            

            timer = 0;
        }
        else
        {
            timer += UnityEngine.Time.deltaTime;
        }
    }

    public void up(string context)
    //public void up(InputAction.CallbackContext context)
    {
        if(context == "on")
        //if (context.performed)
        {
            
            direction_Moto = 0;
            mvt_winner = true;
        }

    }
    
    public void right(string context)
    //public void right(InputAction.CallbackContext context)
    {
        if(context == "on")
        //if (context.performed)
        {
            direction_Moto = 1;
            mvt_winner = true;
        }
    }
    
    
    public void down(string context)
    //public void down(InputAction.CallbackContext context)
    {
        if(context == "on")
        //if (context.performed)
        {
            
            direction_Moto = 2;
            mvt_winner = true;
        }
    }
    
    public void left(string context)
    //public void left(InputAction.CallbackContext context)
    {
        if(context == "on")
        //if (context.performed)
        {
            direction_Moto = 3;
            mvt_winner = true;
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        transform.position = new UnityEngine.Vector3(0, 0, 0);
        mvt_winner = false;
    }

}
