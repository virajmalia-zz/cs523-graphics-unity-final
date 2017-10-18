using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorScript : MonoBehaviour
{
    static int idleState = Animator.StringToHash("Base Layer.Idle");
    static int JumpObstacle = Animator.StringToHash("Base Layer.JumpObstacle");
    private Animator anim;
    private float maxspeed = 5f;
    private bool jump;
    private CapsuleCollider collider;
    private bool disableCollider;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        jump = false;
        collider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        jump = false;
        disableCollider = false;
        if (anim == null) return;

        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        var speed = new Vector2(x, y);
        anim.SetBool("Jump", false);
        anim.SetFloat("Speed", speed.sqrMagnitude);
        anim.SetFloat("VelX", x);
        anim.SetFloat("VelY", y);
        var currentBaseState = anim.GetCurrentAnimatorStateInfo(0);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("Jump",true);
        }
        else if (!(currentBaseState.nameHash == idleState && y==0))
        {
            move(x, y);
        }


        if (currentBaseState.nameHash == JumpObstacle )
        {
            anim.SetBool("JumpObstacle", false);
        }
    }

    private void move(float x, float y)
    {
        this.transform.Translate(0f,0f,maxspeed * y * Time.deltaTime,Space.Self) ;
        this.transform.Translate(maxspeed * x * Time.deltaTime,0f,0f,Space.Self);
    }


    private void FixedUpdate()
    {
        if (jump)
        {
            collider.radius = 0;
            collider.height = 0;
            anim.SetBool("JumpObstacle", true);
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().isKinematic = false;
        }
        else if(disableCollider)
        {
            collider.radius = 0;
            collider.height = 0;
            anim.SetBool("JumpObstacle", false);
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().isKinematic = false;
        }
        else
        {
            collider.radius = 0.5f;
            collider.height = 2f;
            anim.SetBool("JumpObstacle", false);
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().isKinematic = true;
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            jump = true;
        }
        else
        {
            jump = false;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            disableCollider = true;
        }
        else
        {
            disableCollider = false;
        }
    }

}
