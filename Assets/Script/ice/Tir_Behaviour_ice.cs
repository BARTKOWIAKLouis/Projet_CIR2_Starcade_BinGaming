using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tir_Behaviour : MonoBehaviour
{
    public Rigidbody2D body;
    public float speed_tir = 2;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start");
        body.velocity = transform.up * speed_tir;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("destroy");
        Destroy(gameObject);
    }

}
