using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rays : MonoBehaviour {
    public GameObject Brimestone;
    List<GameObject> brimstones;
    [Header("Control Settings")]
    public KeyCode attackKey;
    
    [Header("Laser Settings")]
    public Material material;
    public Color lineColor;
    public float lineSize;
    List<GameObject> lines = new List<GameObject>();
    LineRenderer myRenderer;
	// Use this for initialization
	void Start () {
		brimstones = new List<GameObject>();
	}

    public void PaintLine(Vector3 start, Vector3 end)
    {
        GameObject line = new GameObject();
        lines.Add(line); // 统一管理动态生成的物体，以便销毁
        line.transform.parent = transform;
        myRenderer = line.AddComponent<LineRenderer>();

        myRenderer.material = material;
        myRenderer.startColor = lineColor;
        myRenderer.endColor = lineColor;

        myRenderer.startWidth = lineSize;
        myRenderer.endWidth = lineSize;

        myRenderer.positionCount = 2;
        myRenderer.SetPosition(0, start);
        myRenderer.SetPosition(1, end);
        // renderer根据一个顶点构成的序列来绘制所需的线条，这里setPosition的第二个参数就代表要设置的顶点坐标，第一个参数代表这个顶点在序列中的索引位置，从0开始

    }

    // Update is called once per frame
    void Update () {
        for (int i = 0; i < brimstones.Count; ++i) {
            Destroy(brimstones[i]);
        }
        brimstones.Clear();
        for (int i = 0; i < lines.Count; ++i)
        {
            Destroy(lines[i]);
        }
        lines.Clear();

        Ray2D ray;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (!Input.GetKey(attackKey)) return;

        //Vector2 rayVector = new Vector2(Input.mousePosition.x - transform.position.x, Input.mousePosition.y - transform.position.y);
        //Vector2 rayVector = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        Vector2 rayVector = new Vector2(0, 1);
        rayVector = transform.TransformVector(rayVector);

        

        ray = new Ray2D(transform.position, rayVector);
        RaycastHit2D info = Physics2D.Raycast(ray.origin, ray.direction);
        int reflectCounter = 0;
        if (info.collider != null)
        {
            while (reflectCounter <= 5 && info.collider != null)
            {
                if (info.collider.attachedRigidbody != null)
                {
                    Attack(info.collider.attachedRigidbody.gameObject);
                    
                }
                //Debug.DrawLine(ray.origin, info.point, Color.red);
                this.DrawBrimstone(ray.origin, info.point);
                rayVector = Vector2.Reflect(rayVector, info.normal);
                ray = new Ray2D(info.point, rayVector);
                info = Physics2D.Raycast(ray.origin, ray.direction);
                reflectCounter++;
            }
            Debug.DrawRay(ray.origin, ray.direction, Color.red);
            //this.DrawBrimstoneRay(ray.origin, ray.direction);
        }
        else
        {
            Debug.DrawRay(transform.position, rayVector, Color.red);
            this.DrawBrimstoneRay(ray.origin, ray.direction);
        }
	}
    void DrawBrimstoneRay(Vector2 start, Vector2 direction) {
        float LENGTH = 80F;
        Vector2 end = start + direction * LENGTH;
        DrawBrimstone(start, end);
    }

    void DrawBrimstone(Vector2 start, Vector2 end)
    {
        Vector3 st = new Vector3(start.x, start.y, 0);
        Vector3 ed = new Vector3(end.x, end.y, 0);
        PaintLine(st, ed);
    }

    void DrawBrimstoneDeprecated(Vector2 start, Vector2 end) {
        //Debug.Log("brimstone called.");
        /*
        if (Input.GetKeyDown(KeyCode.Space)) {
            GameObject rayunit = Instantiate(Brimestone) as GameObject;
        }
         
        GameObject rayunit = Instantiate(Brimestone) as GameObject;        
        brimstones.Add(rayunit);
        rayunit.transform.position = end;
        Vector2 direction = end - start;
        rayunit.transform.forward = direction;
         */
        
        float LENGTH = 0.5F;
        Vector2 direction = end - start;
        //Vector2 renderPos = new Vector2(start.x + this.transform.position.x,start.y + this.transform.position.y);
        Vector2 renderPos = start;
        var dist = direction.magnitude;
        var stepmax = dist / LENGTH;
        //Debug.Log(stepmax);
        Vector2 step = direction / stepmax;
        for (int i = 0; i < stepmax; i++) {
            if (brimstones.Count > 200) break;
            GameObject rayunit = Instantiate(Brimestone) as GameObject;
            brimstones.Add(rayunit);
            rayunit.transform.position = renderPos;
           //rayunit.transform.forward = direction;
            //rayunit.transform.LookAt(end);
            rayunit.transform.right = direction;
            renderPos = renderPos + step;            
        }
         
         
    }

    private void OnDestroy()
    {
        for (int i = 0; i < brimstones.Count; ++i)
        {
            Destroy(brimstones[i]);
        }
        brimstones.Clear();
    }

    private void Attack(GameObject obj)
    {
        Health health = obj.GetComponent<Health>();
        health.attacked();
    }

}
