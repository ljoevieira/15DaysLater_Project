using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectable_behavior : MonoBehaviour
{
    [SerializeField]
    public int speed = 50;
    [SerializeField]
    public int dmg = 1;

    public int projectable_type = 0;

    /*[SerializeField]
    Sprite[] sprite_color = new Sprite[2];*/
    


    public int proDelta;
    // Use this for initialization
    void Start()
    {
        /*if (projectable_type == 1)
        {
            sprite_color[0] = this.gameObject.GetComponent<Sprite>();
            sprite_color[0] = sprite_color[1];
        }
        if (projectable_type == 2)
        {
            sprite_color[0] = this.gameObject.GetComponent<Sprite>();
            sprite_color[0] = sprite_color[2];
        }*/
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        this.transform.Translate((new Vector2(proDelta, 0) * speed) * Time.deltaTime);

    }

   //transform
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("DMG" + collision.gameObject.name);

        if (collision != null)
        {
            if (projectable_type == 1)
            {
                if (collision.CompareTag("Player"))
                {
                    collision.GetComponent<player_2Dcontroller>().HPMinus(dmg);
                    Destroy(this.gameObject);
                }
            }
            if (projectable_type == 2)
            {
                if (collision.CompareTag("Enemy"))
                {
                    collision.GetComponent<Enemy_2DController>().HPMinus(dmg);
                    Destroy(this.gameObject);
                }
            }
           
            

           ;
        }
    }
    

    
    private void OnBecameInvisible()
    {
        //Debug.Log("BUM");
        Destroy(this.gameObject);
    }
}

