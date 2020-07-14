using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_2Dcontroller : MonoBehaviour {

    [SerializeField]
    GameObject projectableOne;
   
    [SerializeField]
    float testvar = 0.1f;

    /// THIS var

    [SerializeField]
    public LayerMask groundLayer; // ground var


    [SerializeField]
    public float maxJumpDis = 20.0f;

    [SerializeField]
    public float jumpCharges = 1; // Jump counter

    Rigidbody2D buneco; //lol rg

    Vector2 floor = new Vector2(0.0f, -1.0f); //position floor var

    public Vector2 Dvertical_velocity; //debug var
    Vector3 moveDirection = Vector3.zero; //imported move var

    [SerializeField]
    float maxGravity = 2.0f;

    /// Imported var
    
    [SerializeField]
    public float fallMTP = 2.5f;
    [SerializeField]
    public float lowJumpMTP = 2.0f;

    
    [SerializeField]
    float speed = 4.0f;
    [SerializeField]
    public float jumpSpeed = 8.0f;
    
    [SerializeField]
    public float internalGravity = 20.0f;

    [SerializeField]
    public float maxSpeed = 5.0f;
    

    //HP
    public int player_maxHP = 15;
    public int player_HP = 10;
    

    [SerializeField]
    GameObject floorGO;
    

    public float gTimer = 0.0f;
    public float jTimer = 0.0f;

    public int p_moveDelta = 1;


    public bool canJump = true;
    // Use this for initialization
    void Start()
    {
        rgPlayerCheck(this.gameObject);
        
    }

 
    // Update is called once per frame
    void FixedUpdate()
    {
        
        Tmovimento();
        PJump3();
        ShootBasic();


        //ResetPosition(floorGO);
        //PJump();
        //PJumpCharges();
        // PTransJump();
        // Debug.Log(jumpCharges);



        //ChargesCounter();

        // FakePhis(floorGO);

        //maxJump = isMaxJump();



    }




    public void HPPlus(int x)
    {
       
        //Visual HP Fun
        if (player_HP < player_maxHP)
        {
            player_HP += x;
            //Death Fun

            //Debug.Log("YOUDEAD!");
        }

    }

    public void HPMinus(int x)
    {
        player_HP -= x;
        //Visual HP Fun
        if (player_HP < 0)
        {
            //Death Fun

            //Debug.Log("YOUDEAD!");
        }

    }

    public void ResetPosition(GameObject toRend)
    {
        if (Input.GetKey(KeyCode.R))
        {
        Renderer FRend = toRend.GetComponent<Renderer>();
        float floorYbound = FRend.bounds.size.y;
        //Debug.Log(floorYbound/2);
       
            //Debug.Log(floorYbound/2);
            this.gameObject.transform.position = new Vector3(1.0f, floorYbound, 0);
        }

    }

    public void ShootBasic()
    {
        Renderer playrend = this.gameObject.GetComponent<Renderer>();
        float offground = ((playrend.bounds.size.x) + 0.1f)*p_moveDelta;
        Vector3 spPoint = new Vector3((this.transform.position.x+offground),this.transform.position.y,0.0f);

        if (Input.GetKey(KeyCode.Q))
        {
            GameObject projectible = Instantiate
           (projectableOne,
           spPoint,
           Quaternion.identity
           );
            projectible.GetComponent<projectable_behavior>().proDelta = p_moveDelta;
            projectible.GetComponent<projectable_behavior>().projectable_type = 2;

        }

    }


    public void ChargesCounter()
    {
        jTimer += Time.deltaTime;

        if (gTimer < 1.0f)
        {
            gTimer += Time.deltaTime;
        }

        if (gTimer >= 1.0f)
        {
            if (jumpCharges < 100)
            {
                jumpCharges += 1;
            }
            gTimer = 0.0f;
        }
    }

    public void rgPlayerCheck(GameObject GO)
    {

        if (GO.gameObject.GetComponent<Rigidbody2D>())
        {
            buneco = GO.gameObject.GetComponent<Rigidbody2D>();
        }
        else
        {
            Rigidbody2D rgPlayer1 = GO.gameObject.AddComponent<Rigidbody2D>();
        }
        //Debug.Log(this.gameObject.GetComponent<Rigidbody2D>().name);
    }
    
   


    public void Tmovimento()
    {
        
        //Debug.Log("movYES");
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
        moveDirection *= speed;


        //moveDirection.y -= internalGravity * Time.deltaTime;

        if (moveDirection.x > 0)
        {
            p_moveDelta = 1;
        }
        if (moveDirection.x < 0)
        {
            p_moveDelta = -1;
        }
        buneco.transform.position += moveDirection * Time.deltaTime;

        /* rolagem
        buneco.velocity += (moveDirection * Time.deltaTime);*/
    }


    public void PJump3()
    {
        if (Input.GetButton("Jump") && canJump)
        {

            if (canJump)
            {
               
                    buneco.transform.Translate((Vector2.up * (jumpSpeed)) * Time.deltaTime);
                   // Debug.Log("UP");
                    isMaxJump();

               
                
            }
             if (isMaxJump())
            {
                if (!IsGrounded())
                {
                    buneco.transform.Translate((-Vector2.up* (fallMTP))* Time.deltaTime);
                    //Debug.Log("STOP");
                    
                }
               
            }
           
            
	{

            }
        }
        else
        {
            if (IsGrounded())
            {
                isMaxJump();
            }
            else
            {
                buneco.transform.Translate((-Vector2.up * (fallMTP)) * Time.deltaTime);

            }
        }

    }

    bool isMaxJump()
    {
        Renderer playrend = this.gameObject.GetComponent<Renderer>();
        Vector2 position = this.gameObject.transform.position;
        Vector2 direction = Vector2.down;
        float distance = maxJumpDis;

        //RaycastHit2D hit = Physics2D.Raycast(position, direction, -2.0f, groundLayer);
        Debug.DrawRay(this.transform.position, new Vector3(0.0f, -distance, 0.0f), Color.red);
       if(Physics2D.Raycast(position, direction, distance, groundLayer))
        {
            if (IsGrounded())
            {
                canJump = true;
            }
            
            return false;
        }

        canJump = false;

        return true;
    }

        

   

    bool IsGrounded()
{
        Renderer playrend = this.gameObject.GetComponent<Renderer>();
        Vector2 position = this.gameObject.transform.position;
        Vector2 direction = Vector2.down;
        float distance = 1.0f;


        //float offground = ((playrend.bounds.size.y/2.0f)+0.0001f); // usa o valor do render do objeco, dividido por + 000.1 pra calcular a distancia;
        float offground = ((playrend.bounds.size.y/2)+testvar);
        //testvar;
        //Debug.Log(offground);


    RaycastHit2D hit = Physics2D.Raycast(position, direction, offground, groundLayer);
    Debug.DrawRay(this.transform.position,new Vector3(0.0f,-offground, 0.0f),Color.green);
    if (hit.collider != null)
    {
        return true;
    }

    return false;
}

    





    //OTHER JUMPS (Rigibody)
    /*
   public void PJump()
   {
       if (buneco)
       {
           Dvertical_velocity = buneco.velocity;


           if (Input.GetButton("Jump"))
           {

               if (Input.GetAxis("Horizontal") != 0.0f)
               {
                   moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
                   moveDirection *= speed;

                   //moveDirection.y -= internalGravity * Time.deltaTime;


                   buneco.transform.position += moveDirection * Time.deltaTime;
               }

               if (buneco.velocity.y <= 0)
               {

                   buneco.velocity += (((-1 * Vector2.up) * internalGravity) * (fallMTP - internalGravity) * Time.deltaTime);  //(direção * gravidade) *(força - gravidade))

                   //Debug.Log("JUMPY "+buneco.velocity.y);
                   //jumpCharges -= 1.0f * Time.deltaTime;


               }
           }
           else if (buneco.velocity.y > 0 && !Input.GetButtonDown("Jump"))
           {
               Debug.Log("down" + buneco.velocity.y);
               buneco.velocity += (Vector2.up * internalGravity * (lowJumpMTP - internalGravity) * Time.deltaTime); //(direção * gravidade) *(forçadaQUEDA - gravidade))


           }
           else if (IsGrounded())
           {

               if (IsGrounded() && buneco.velocity.y != 0.0f)
               {
                   buneco.velocity = Vector2.zero;
                   //Debug.Log("ZERO");
               }
               //Debug.Log(IsGrounded());


           }
       }
   }

   public void PTransJump()
   {
       if (buneco)
       {
           Vector3 jumpDirection = Vector3.up;

           if (Input.GetButton("Jump"))
           {
               if (jumpCharges >= 1)
               {
                   if (jTimer >= 2.0f)
                   {
                       if (buneco.isKinematic)
                       {
                           //Vector3 jumpDirection = new Vector3(0.0f, Input.GetAxis("Vertical"), 0.0f);


                           jumpDirection *= jumpSpeed;
                           float tposY = transform.position.y;

                           buneco.transform.Translate((Vector2.up * jumpSpeed ) * Time.deltaTime);
                           //buneco.transform.position += jumpDirection * Time.deltaTime;
                           //buneco.velocity += (((-Vector2.up) * internalGravity) * (fallMTP - internalGravity) * Time.deltaTime);  //(direção * gravidade) *(força - gravidade))



                       }
                       jTimer -= Time.deltaTime*2;
                       jumpCharges -= 1.0f;
                   }

               }
           }
           //DEBUG
           Dvertical_velocity = jumpDirection;
       }
   }
   */


    //Referencia Raycast Floor + render space
    /*
    public void floordection(LayerMask mask)
    {
        RaycastHit hit;

        Renderer playrend = this.gameObject.GetComponent<Renderer>();

        float offground = (playrend.bounds.size.y);
        //Debug.Log(offground);

        Debug.DrawRay(this.transform.position, Vector3.down, Color.red, offground);

        //old offground = 2,5f;
        if (Physics.Raycast(this.transform.position, Vector3.down, out hit, offground, mask))
        {
            //jumpmove
            groundcanmov = false;
            //normalmove
            //groundcanmov = true;


            //Debug.Log(hit.transform.gameObject.GetInstanceID());
            objectID = hit.transform.gameObject.GetInstanceID();
        }
        else
        {
            //jumpmove
            groundcanmov = true;

            //normalmove
            //groundcanmov = false;

        }

    }
    */

}
