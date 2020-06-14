using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public GameObject obj;
    public GameObject displayer;
    public int health;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            killed();
            return;
        }

    }

    public void attacked()
    {
        if (health > 0) health--;
    }

    public void killed()
    {
        if (obj != null)
            Destroy(obj);
        obj = null;
    }

}
