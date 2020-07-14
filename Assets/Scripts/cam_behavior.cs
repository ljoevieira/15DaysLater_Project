using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam_behavior : MonoBehaviour {

    [SerializeField]
    GameObject player_ob;
    [SerializeField]
    Vector3 orginalPos = new Vector3(8.89f, 4.05f, -10f);
    [SerializeField]
    Vector3 orginalOffset = new Vector3(0.3f, 3.6f, -1.5f);

    Vector3 offset;
    // Use this for initialization
    void Start()
    {
        playerFinder();

        offset = orginalOffset;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        playerFinder();
        camerFollowX();
        //cameraFollowSimple();

    }

    public void cameraFollowSimple()
    {
        this.transform.position = player_ob.transform.position + offset;
    }

    public void camerFollowX()
    {
        Vector3 tofollow = new Vector3(player_ob.transform.position.x, 0.0f, player_ob.transform.position.z);
        this.transform.position = tofollow + offset;
    }

    public void playerFinder()
    {
        if (!player_ob)
        {
            player_ob = GameObject.FindGameObjectWithTag("Player");

        }
    }

}
