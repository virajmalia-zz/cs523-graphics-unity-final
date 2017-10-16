using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruteScript : MonoBehaviour {
    private Animator anim;
    private float maxspeed = 5f;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (anim == null) return;

        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        var speed = new Vector2(x,y);
        anim.SetFloat("Speed", speed.sqrMagnitude);
        anim.SetFloat("Direction", x, 0, Time.deltaTime);
        anim.SetFloat("Backward",y);
        move(x, y);
    }

    private void move(float x, float y)
    {     
        this.transform.position += Vector3.forward * maxspeed * y * Time.deltaTime;
        this.transform.position += Vector3.right * maxspeed * x * Time.deltaTime;
    }
}
