using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErikaScript : MonoBehaviour {

    private Animator anim;
    private float speed = 5f;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

        if (anim == null) return;

        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        move(x, y);
    }

    private void move(float x, float y)
    {
        anim.SetFloat("VelX", x);
        anim.SetFloat("VelY", y);

        transform.position += Vector3.forward * speed * y * Time.deltaTime;
        transform.position += Vector3.right * speed * x * Time.deltaTime;
    }
}
