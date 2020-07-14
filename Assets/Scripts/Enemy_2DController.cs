using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2DController : MonoBehaviour
{
    [SerializeField]
    public int actor_HP = 2;
    

    [SerializeField]
    GameObject floorGO;
    [SerializeField]
    LayerMask groundLayer; //to became inhertit


    bool isVisible = false;
    [SerializeField]
    public float speed = 1.0f;

    [SerializeField]
    public float AutoShotTimer = 0.0f;


    [SerializeField]
    public bool mover = true;
    bool canMove = false;
    [SerializeField]
    public bool shoter = true;

    public bool canShoot = false;

    [SerializeField]
    GameObject projectableOne;


    [SerializeField]
    public GameObject player_GO;
    float playerDelta;

    private void awake()
    {        
        groundLayer = floorGO.layer;

        if (!IsGrounded())
        {
            //GroundedPosition(floorGO);
        }
       
    }

    private void Update()
    {
        isOnScreen();
        PlayerRefreshPos();
        AutoMovimento();       
        ShootBasic();



    }

    public void HPzero()
    {
        if (actor_HP <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void HPMinus(int x)
    {
        actor_HP -= x;
        //Visual HP Fun
        HPzero();

    }

    public void ShootBasic()
    {
        AutoShotTimer += Time.deltaTime;

        //Debug.Log(shoter);

        if (shoter && canShoot)
        {         
                AutoShotTimer += Time.deltaTime;

                if (AutoShotTimer >= 5.0f)
                {
                    GameObject projectible = Instantiate
                   (projectableOne,
                   this.transform.position,
                   Quaternion.identity
                   );
                projectible.GetComponent<projectable_behavior>().projectable_type = 1;

                if (playerDelta > 0)
                    {
                        projectible.GetComponent<projectable_behavior>().proDelta = -1;
                    }
                    if (playerDelta < 0)
                    {
                        projectible.GetComponent<projectable_behavior>().proDelta = 1;
                    }

                    AutoShotTimer = 0.0f;

                }
            
        }
    }
    

    private void OnBecameVisible()
    {
        
        isVisible = true;

        
    }

    private void OnBecameInvisible()
    {
       

        isVisible = false;
    }

    void isOnScreen()
    {
        if (isVisible)
        {
            canMove = true;
            canShoot = true;

        }
        else
        {
            canMove = false;
            canShoot = false;
        }
    }

    public void AutoMovimento()
    {
        if (mover && canMove)
        {
            Vector2 moveDirection = Vector2.zero;          
            if (playerDelta > 0)
            {
                moveDirection = Vector2.left;
                //Debug.Log("left: " + playerDelta);
            }
            if (playerDelta <= 0)
            {
                moveDirection = Vector2.right;
               // Debug.Log("right: " + playerDelta);
            }

            moveDirection *= speed;

            this.transform.Translate(moveDirection * Time.deltaTime);

        }

    }

    public void GroundedPosition(GameObject toRend)
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

    public void PlayerRefreshPos()
    {

        playerDelta = this.transform.position.x - player_GO.transform.position.x;

        //Debug.Log("PlayerDelta" + playerDelta);


    }

    bool IsGrounded()
    {
        Renderer playrend = this.gameObject.GetComponent<Renderer>();
        Vector2 position = this.gameObject.transform.position;
        Vector2 direction = Vector2.down;
        //float distance = 1.0f;


        //float offground = ((playrend.bounds.size.y/2.0f)+0.0001f); // usa o valor do render do objeco, dividido por + 000.1 pra calcular a distancia;
        float offground = ((playrend.bounds.size.y / 2)+0.02f);
        //testvar;
        //Debug.Log(offground);


        RaycastHit2D hit = Physics2D.Raycast(position, direction, offground, groundLayer);
        Debug.DrawRay(this.transform.position, new Vector3(0.0f, -offground, 0.0f), Color.green);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }


}