using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spark_script : MonoBehaviour
{

    private float timer;
    private UnityEngine.Vector3 pos;
    public GameObject lazer;
    public float delais_feu = 1;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > delais_feu)
        {
            Instantiate(lazer, pos, Quaternion.Euler(new Vector3(0,0,335)));

            Destroy(this.gameObject);
        }
        timer += Time.deltaTime;  
    }


   public void position_lazer(UnityEngine.Vector3 tmp_vect)
    { 
        pos = tmp_vect;
    }

}
