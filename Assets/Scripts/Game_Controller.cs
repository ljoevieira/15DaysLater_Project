using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Controller : MonoBehaviour {

    [SerializeField]
    public GameObject playerGO;
    Vector2 player_pos;

    Vector2 start_pos;

    

    player_2Dcontroller player_controller_script;


    //Inicializador
    [SerializeField]
    GameObject[] prefab_floor = new GameObject[5];


    //Controla o mumero e tipo de tiles, ativos;
    public List<GameObject> exfloor = new List<GameObject>();
    public int maxtilenumber = 10;

    //tamanho dos blcos (tiles)
    [SerializeField]
    public float tile_acumulated_size = 10.0f;


    //distancia entre o primeiro bloco e a distancia percorrida;
    float tiledistance;
    float tileAbsolut;
    Renderer rend;



    //
    //
    //Enemy Spawn Controller
    GameObject [] enemy_spawn_pos = new GameObject[7];

    [SerializeField]
    GameObject[] enemy_prefab = new GameObject[2];

    [SerializeField]
    int enemy_Spawn_rate = -4;

    //[SerializeField]
    GameObject[] enemy_spawn = new GameObject[3];


    //
    //
    //Collectables Controller
    private Vector2[] colect_spawn_pos = new Vector2[7];

    [SerializeField]
    int colect_Spawn_rate = -6;

    [SerializeField]
    GameObject[] colect_prefab = new GameObject[1];
    


    // GameObject[] colet_spawn = new GameObject[3];


    private void Awake()
    {
        //Instancia o primeiro bloco de "floor"
        GameObject floor = Instantiate
             (prefab_floor[0],
             (start_pos),
             Quaternion.identity,
             this.transform);

        exfloor.Add(floor);


        int child_countMax = floor.transform.childCount - 1;
        //Debug.Log(child_countMax);
        rend = floor.transform.GetChild(child_countMax).GetComponent<Renderer>();

        tileAbsolut = rend.bounds.size.x;


        //player_controller_script = real_player_character.GetComponent<player_controller>();
    }
    // Use this for initialization
    void Start ()
    {
        //trocar por spawner e referencia no script
        player_pos = playerGO.transform.position; 

    }
	
	// Update is called once per frame
	void Update ()
    {
        floorspawnList();
    }
    void player_RealPos()
    {
        player_pos = playerGO.transform.position;
    }


    void EnemySpawner(GameObject floortoSpawnOn)
    {

        Transform enemy_container = floortoSpawnOn.transform.GetChild(1);

        if (enemy_container.CompareTag("enemy_container"))
        {
            for (int i = 0; i < enemy_container.transform.childCount; i++)
            {
                if (enemy_container.transform.GetChild(i)!= null)
                {
                    int y = Random.Range(enemy_Spawn_rate, 5);
                    if (y >= 0)
                    {
                        int rd = Random.Range(0, enemy_prefab.Length);

                        GameObject enemy = Instantiate
                                       (enemy_prefab[rd],
                                       enemy_container.transform.GetChild(i).transform.position,
                                       Quaternion.identity,
                                       enemy_container.transform.parent);

                        
                        enemy.GetComponent<Enemy_2DController>().player_GO = this.transform.GetChild(0).gameObject; //not perfect >> works for now
                    }
                }
            }
        }
    }

    void ColectSpawner(GameObject floortoSpawnOn)
    {
        Transform collectables_container = floortoSpawnOn.transform.GetChild(0);

        if (collectables_container.CompareTag("collectables_container"))
        {

            for (int i = 0; i < collectables_container.transform.childCount; i++)
            {
                if (collectables_container.transform.GetChild(i) != null)
                {
                    int y = Random.Range(colect_Spawn_rate, 5);
                    if (y >= 0)
                    {
                        // Debug.Log("do" + y);
                        int rd = Random.Range(0, colect_prefab.Length);

                        GameObject collectable = Instantiate
                                   (colect_prefab[rd],
                                   collectables_container.transform.GetChild(i).transform.position,
                                   Quaternion.identity,
                                   collectables_container.transform.parent);
                    }
                    // Debug.Log("ndo" + y);
                }

            }

        }

    }


    void floorspawnList()
    {
        if (exfloor.Count < maxtilenumber)
        {
            int rand = Random.Range(0, prefab_floor.Length);

            for (int i = 0; i < exfloor.Count; i++)
            {
                if (exfloor[i] != null)
                {
                    tile_acumulated_size = exfloor[i].transform.position.x + tileAbsolut;
                    //Debug.Log(tileL);
                }

            }

            GameObject floor = Instantiate
            (prefab_floor[rand],
            (Vector3.right * tile_acumulated_size),
            Quaternion.identity,
            this.transform);

            ColectSpawner(floor);
            EnemySpawner(floor);

            //tileL = tileL + tileAbsolut;
            //Debug.Log(tileL);
            //exfloor.Insert(0, floor);
            exfloor.Add(floor);

        }
        if (exfloor[0] != null)
        {
            player_RealPos();
            float totalLenght = exfloor[0].transform.position.x + (tileAbsolut * 2);
            if (totalLenght < player_pos.x)
            {
                Debug.Log(totalLenght);
                Debug.Log(player_pos.x);
                tiledistance = tiledistance + tileAbsolut;
                GameObject td = exfloor[0];
                exfloor.RemoveAt(0);
                Object.Destroy(td);
            }
           
        }
    }

   
}

    

