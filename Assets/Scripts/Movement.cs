using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    public enum MoveKeys
    {
        WASD = 0,
        arrows = 1
    }
    public float speed;
    public Rigidbody2D body;

    [Header("Control Settings")]
    public MoveKeys inputMethod;
    /*
    public string horizontal_axis_name;
    public string vertical_axis_name;
    */
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float h = 0, v = 0;
        switch (inputMethod)
        {
            case MoveKeys.WASD:
                h = Input.GetAxisRaw("Horizontal1");
                v = Input.GetAxisRaw("Vertical1");
                break;
            case MoveKeys.arrows:
                h = Input.GetAxisRaw("Horizontal2");
                v = Input.GetAxisRaw("Vertical2");
                break;
            default:
                break;
        }

        //gameObject.transform.position = new Vector2(gameObject.transform.position.x + (h * speed), gameObject.transform.position.y + (v * speed));
        body.velocity = new Vector2(h * speed, v * speed);
        
    }  
}
