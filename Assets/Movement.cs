using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    public float speed;
    public Rigidbody2D body;
    public string horizontal_axis_name;
    public string vertical_axis_name;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxisRaw(horizontal_axis_name);
        float v = Input.GetAxisRaw(vertical_axis_name);

        //gameObject.transform.position = new Vector2(gameObject.transform.position.x + (h * speed), gameObject.transform.position.y + (v * speed));
        body.velocity = new Vector2(h * speed, v * speed);
        
    }  
}
