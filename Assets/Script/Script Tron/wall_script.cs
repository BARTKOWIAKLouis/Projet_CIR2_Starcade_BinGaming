using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class wall_script: MonoBehaviour
{

    private float timer;
    public float life_time_cube = 1;

    void Start()
    {
        timer = 0;
    }


    void Update()
    {
        timer += Time.deltaTime;
        if (timer > life_time_cube) 
        {
            Destroy(this.gameObject);
        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(this.gameObject);
    }


    public void set_cube_life_time(float i)
    {
        life_time_cube = i;
    }





}
