using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIdebug : MonoBehaviour {

    [SerializeField]
    player_2Dcontroller PScript;
    [SerializeField]
    TextMeshProUGUI DbugText1;
    [SerializeField]
    TextMeshProUGUI DbugText2;
    [SerializeField]
    TextMeshProUGUI JUMPCH;




    [SerializeField]
    TextMeshProUGUI TextTimer;


    //HP BAR
    [SerializeField]
    GameObject par_HPbar;               //parent
    Transform mid_HPbar;               // midle - to be changed whdt
    Transform bg_HPbar;                // background
    Transform indicator_HPbar; //txt on BAR
    TextMeshProUGUI txt_HPbar;
    int maxWidht_HPBAR;


    public float player_atualHP;
    public float player_maxHP;



    [SerializeField]
    Transform HPbar;
    [SerializeField]
    int var1 = 1;

    
    public float globalTimer;
    public float DspeedX;
    public float DspeedY;
	// Use this for initialization
	void Start ()
    {



        if (PScript)
        {
            DspeedX = PScript.Dvertical_velocity.x;
            DspeedY = PScript.Dvertical_velocity.y;
            globalTimer = PScript.gTimer;
            player_atualHP = PScript.player_HP;
            player_maxHP = PScript.player_maxHP;
        }

        HPBarStart();

	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        globalTimer = PScript.gTimer;
        DspeedX = PScript.Dvertical_velocity.x;
        DspeedY = PScript.Dvertical_velocity.y;
        player_atualHP = PScript.player_HP;

        HPwidth();

        DbugText1.SetText("Velocity X: " + DspeedX);
        DbugText2.SetText("Velocity Y: " + DspeedY);
        TextTimer.SetText("Global Timer: " + globalTimer);
       // JUMPCH.SetText(player_atualHP.ToString());
       // HPbar.transform.localScale = new Vector3(player_atualHP/4, 1, 1);


    }


    public void HPBarStart()
    {
        bg_HPbar = par_HPbar.transform.GetChild(0);
        maxWidht_HPBAR = (int)bg_HPbar.transform.localScale.x;
        mid_HPbar = par_HPbar.transform.GetChild(1);
        indicator_HPbar = par_HPbar.transform.GetChild(2);
        txt_HPbar = indicator_HPbar.GetComponent<TextMeshProUGUI>();


    }
    
    public void HPwidth()
    {

        float widht_bar = ((((player_atualHP*1.0f)/player_maxHP)/1.0f));
        //Debug.Log(widht_bar);
        
        mid_HPbar.transform.localScale = new Vector3(widht_bar, mid_HPbar.transform.localScale.y, 0);
        txt_HPbar.SetText(player_atualHP.ToString());
        

    }




}
