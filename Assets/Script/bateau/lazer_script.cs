using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lazer_script : MonoBehaviour
{
    public float tmp_tir = 3;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > tmp_tir)
        {
            Destroy(this.gameObject);
        }
        else
        {
            timer += Time.deltaTime;
        }
    }
}
