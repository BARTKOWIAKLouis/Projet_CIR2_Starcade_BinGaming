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



public class Mortar_shell_script : MonoBehaviour
{

    public GameObject Mortar_fire;
    public float tmp_tir = 1;

    private float timer = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if(timer > tmp_tir)
        {
            Instantiate(Mortar_fire, transform.position + new UnityEngine.Vector3(-3/100, 3/100, 0) , transform.rotation);
            Destroy(this.gameObject);
        }
        else
        {
            timer += Time.deltaTime;
        }
        
    }
}
