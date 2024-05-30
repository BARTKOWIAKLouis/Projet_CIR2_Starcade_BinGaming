using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class firing_manager : MonoBehaviour
{
    private float timer_laser;
    public float max_delais_laser;
    public float min_delais_laser;
    private float delais_laser;
    private float delais_firing_canon;
    private int canon_about_to_fire;

    private float timer_mortier;
    public float max_delais_mortier;
    public float min_delais_mortier;
    private float delais_mortier;


    public GameObject laser;
    public GameObject mortier;
    public GameObject canon_firing;


    public UnityEngine.Vector3[] pos_lazer;
    public UnityEngine.Vector3[] pos_canon;
    public float ajustement_place_cannon = 55/10;
    private bool charging = false;

    private float timer_start;
    public float delay_start;
    public bool start;

    // Start is called before the first frame update
    void Start()
    {
        random_delais_laser();
    }

    // Update is called once per frame
    void Update()
    {
        /*  if (!start)
          {
              if(timer_start)
          }*/
        start = true;

        if (start)
        {
            if (timer_laser > delais_laser)
            {
                float mem = canon_about_to_fire;
                while (mem == canon_about_to_fire)
                {
                    canon_about_to_fire = Random.Range(0, 5);
                }

                GameObject clone = Instantiate(canon_firing, pos_canon[canon_about_to_fire], transform.rotation);
                clone.GetComponent<Spark_script>().position_lazer(pos_lazer[canon_about_to_fire]);

                timer_laser = 0;
            }
            else
            {
                timer_laser += Time.deltaTime;
            }



            if (timer_mortier > delais_mortier)
            {
                timer_mortier = 0;
                Fire_mortier();
                random_delais_mortier();
            }
            else
            {
                timer_mortier += Time.deltaTime;
            }
        }

    }

    public void random_delais_laser()
    {
        delais_laser = Random.Range(min_delais_laser, max_delais_laser);
    }



    public void Fire_mortier()
    {
        Instantiate(mortier, new Vector3(Random.Range(-5,1), Random.Range(-5, 1),0) , transform.rotation);
    }

    public void random_delais_mortier()
    {
        delais_mortier = Random.Range(min_delais_mortier, max_delais_mortier);
    }

}
