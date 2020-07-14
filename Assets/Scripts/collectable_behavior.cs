using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectable_behavior : MonoBehaviour {


    //top class com lista de tipos e metodos inherit

    [SerializeField]
    public int valor = 1;


    // Use this for initialization
    void Start()
    {

        //definetipo

    }

    // Update is called once per frame
    void Update()
    {
    
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("DMG" + collision.gameObject.name);

        if (collision != null)
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<player_2Dcontroller>().HPPlus(valor);

                Destroy(this.gameObject);
            }
            
            
        }
    }



    private void OnBecameInvisible()
    {
       
        //Debug.Log("BUM");
        Destroy(this.gameObject);
    }
}
