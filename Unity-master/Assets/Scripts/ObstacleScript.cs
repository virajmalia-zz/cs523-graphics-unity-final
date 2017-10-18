using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour {
    public Animator anim;
    //private Animation animation;
    static int JumpObstacle = Animator.StringToHash("Base Layer.JumpObstacle");
    //public GameObject player;
    private bool jump;
    private bool jumpExit;
    private Collider collider;
    private float yDisposition = 0f;
    private int count = 0;
    // Use this for initialization
    void Start () {
        jump = false;
        jumpExit = false;
        //animation = player.GetComponent<Animation>();
    }
	
	// Update is called once per frame
	void Update () {

        if (jump)
        {
            yDisposition = 10f * Time.deltaTime;
            anim.SetBool("JumpObstacle", true);
            collider.transform.Translate(0f, yDisposition, 0f, Space.Self);
            
        }
        else
        {
            anim.SetBool("JumpObstacle", false);
        }

            if ((anim.GetCurrentAnimatorStateInfo(0).fullPathHash == JumpObstacle))
            {
                anim.SetBool("JumpObstacle", false);
                collider.transform.Translate(0f, -1*yDisposition , 5*Time.deltaTime, Space.Self);
                collider.transform.position= new Vector3(collider.transform.position.x,0f,collider.transform.position.z);
                jumpExit = false;
            }


	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            collider =  other;
            jump = true;
        }
        else
        {
            jump = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            collider = other;
            jump = false;
            jumpExit = true;
        }
    }
}
