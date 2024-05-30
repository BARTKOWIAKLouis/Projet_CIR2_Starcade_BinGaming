using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.InputSystem;

public class Movement_player_tron : MonoBehaviour
{
    public GameObject[] wall_list;
    public GameObject wall;
    public Rigidbody2D Rigidbody_moto;
    public BoxCollider2D Bc;
    public SpriteRenderer My_sprite_tron;

    public int direction_Moto;
    public float vitesse;
    private float timer = 0;
    public int tmp_lancement = 3;
    public int player_id = 0;

    private Vector3 tmpvect;
    private PlayerInput playerinput;

    private float delta_time;

    public float time_en_bulldozer;
    public float delais_use_bulldozer;
    private float timer_invisible;
    private bool bulldozered = false;

    public float time_en_stop = 1;
    public float delais_use_stop;
    private float timer_stop;
    private bool stopped = false;

    public float initial_cube_life_time;
    public float additional_cube_life_time;
    public float delays_between_life_time_increase;
    private float timer_life_time_increase = 0;

    public bool actionA;
    public bool actionB;

    // Start is called before the first frame update
    void Start()
    {
       

        if (transform.position.x > 5)
        {
            direction_Moto = 3;
        }
        else if(transform.position.x < 5)
        {
            direction_Moto = 1;
        }
        else
        {
            if (transform.position.y > 0)
            {
                direction_Moto = 2;
            }
            else
            {
                direction_Moto = 0;
            }
            
        }


        timer_invisible = time_en_bulldozer;
        timer_stop = time_en_stop;
    }

    // Update is called once per frame
    void Update()
    {
        
        delta_time = Time.deltaTime;

        timer += delta_time;
        timer_stop += delta_time;
        timer_life_time_increase += delta_time;
        timer_invisible += delta_time;

        if (timer_invisible > time_en_bulldozer && bulldozered)
        {
            Bc.isTrigger = false;
            My_sprite_tron.enabled = true ;

            actionA = true;

            timer_invisible = 0;
            bulldozered = false;
        }

        if (timer_stop > time_en_stop && stopped)
        {
            actionB = true;
            stopped = false;
        }

        
        if (timer > vitesse + tmp_lancement && !stopped)
        {
            
            tmp_lancement = 0;

            Debug.Log(wall.transform.localScale.x);
            Debug.Log(vitesse);

            tmpvect = transform.position;
            if (direction_Moto == 0)
            {
                // Orientation haut
                transform.rotation = Quaternion.Euler(0, 0, 0);

                // Mouvement haut
                transform.position = transform.position + Vector3.up * 2 * wall.transform.localScale.x;
            }
            if (direction_Moto == 1)
            {
                // Orientation droite
                transform.rotation = Quaternion.Euler(0, 0, 270);

                // Mouvement droite
                transform.position = transform.position + Vector3.right * 2 *wall.transform.localScale.x;
            }
            if (direction_Moto == 2)
            {
                // Orientation bas
                transform.rotation = Quaternion.Euler(0, 0, 180);

                // Mouvement bas
                transform.position = transform.position + Vector3.down * 2 * wall.transform.localScale.x;
            }
            if (direction_Moto == 3)
            {
                // Orientation gauche
                transform.rotation = Quaternion.Euler(0, 0, 90);

                // Mouvement gauche
                transform.position = transform.position + Vector3.left * 2 * wall.transform.localScale.x;
            }
            
            if(timer_life_time_increase > delays_between_life_time_increase)
            {
                initial_cube_life_time += additional_cube_life_time;
            }

            if (!bulldozered)
            {
                GameObject clone = Instantiate(wall, tmpvect, transform.rotation);

                clone.GetComponent<wall_script>().set_cube_life_time(initial_cube_life_time);
            }

            timer = 0;
        }   
    }

    public void up(string context)
    //public void up(InputAction.CallbackContext context)
    {
        if(context == "on")
        //if (context.performed)
        {
            direction_Moto = 0;
        }
       
    }
    public void right(string context)
    //public void right(InputAction.CallbackContext context)
    {
        if(context == "on")
        //if (context.performed )
        {
            direction_Moto = 1;
        }
    }
    public void down(string context)
    //public void down(InputAction.CallbackContext context)
    {
        if(context == "on")
        //if (context.performed )
        {
            direction_Moto = 2;
        }
    }
    public void left(string context)
    //public void left(InputAction.CallbackContext context)
    {
        if(context == "on")
        //if (context.performed)
        {
            direction_Moto = 3;
        }
    }



    public void tron_color_cube(int i)
    {
        wall = wall_list[i];
    }



    public void fonction_Act1(string context)
    //public void fonction_Act1(InputAction.CallbackContext context)
    {
        if (context == "on" && timer_invisible > delais_use_bulldozer)
        //if (context.started && timer_invisible > delais_use_bulldozer)
        {
            actionA = false;

            Bc.isTrigger = true;
            My_sprite_tron.enabled = false;

            timer_invisible = 0;
            bulldozered = true;
        }
    }

    public void fonction_Act2(string context)
    //public void fonction_Act2(InputAction.CallbackContext context)
    {
        if (context == "on" && timer_stop > delais_use_stop)
        //if (context.started && timer_stop > delais_use_stop)
        {
            actionB = false;
            timer_stop = 0;
            stopped = true;
        }
    }






    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
