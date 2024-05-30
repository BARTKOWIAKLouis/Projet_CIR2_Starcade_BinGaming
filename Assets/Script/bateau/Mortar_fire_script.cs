using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mortar_fire_script : MonoBehaviour
{

    public float tmp_explosion = 1;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > tmp_explosion)
        {
            Destroy(this.gameObject);
        }
        else
        {
            timer += Time.deltaTime;
        }
    }
}
