﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour {
    public int Max;
    public int n;
    private LineRenderer line;
    private Vector3 mousepos;
    private Vector3 startpos;
    private Vector3 endPos;
    public List<Vector2> newVerticies = new List<Vector2>();
    private Vector2[] colvec = new Vector2[0];
    private BoxCollider2D col2;
    private Rigidbody2D rid;
    private int ison;
    private int linenum;
    private float time;
    private int LastPos;
    [SerializeField]
    private ClearConditionScript clearcondition;
    void Start () {
        PlayerPrefs.SetInt(Prefstype.Item2Use, 0);
        clearcondition = this.GetComponent<ClearConditionScript>();
        n = 1;
        linenum = 0;
        time = 0;
    }

    // Update is called once per frame
    void Update () {
        ison = PlayerPrefs.GetInt(Prefstype.Item2Use);
        time += Time.deltaTime;
        
        if(time > 15)
        {
            Destroy(GameObject.Find("Line" + (linenum-1)));
            n = 1;
        }
		if(Input.GetMouseButtonDown(0)&& ison == 1)
        {
            if (Max > n)
            {
                time = 0;
                if (line == null)
                    createLine();
                if (linenum > 0)
                {
                    Destroy(GameObject.Find("Line" + (linenum - 1)));
                    n = 1;
                }
                mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousepos.z = 0;
                col2 = new GameObject("col").AddComponent<BoxCollider2D>();
                col2.size = new Vector2(0.1f, 0.1f);
                col2.transform.position = mousepos;
                col2.transform.parent = line.transform;
                col2.gameObject.layer = LayerMask.NameToLayer("Ground");
         
                newVerticies.Add(new Vector2(mousepos.x, mousepos.y));
                line.SetPosition(0, mousepos);
                line.SetPosition(1, mousepos);
                startpos = mousepos;
            }

        }
        else if(Input.GetMouseButtonUp(0)&& line && ison == 1)
        {
            if (line)
            {
                mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousepos.z = 0;
                endPos = mousepos;
                rid.simulated = true;
                line = null;
                PlayerPrefs.SetInt(Prefstype.Item2Use, 0);
                linenum++;
                time = 0;
            }
        }
        else if(Input.GetMouseButton(0)&& ison == 1)
        {

            if (Max > n)
            {
                line.positionCount = n + 1;
                mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousepos.z = 0;
                col2 = new GameObject("col").AddComponent<BoxCollider2D>();
                col2.size = new Vector2(0.1f, 0.1f);
                col2.transform.position = mousepos;
                col2.transform.parent = line.transform;
                col2.gameObject.layer = LayerMask.NameToLayer("Ground");
                line.SetPosition(n, mousepos);
                newVerticies.Add(new Vector2(mousepos.x, mousepos.y));
                n++;
            }
        }

     
    }
    private void createLine()
    {
        line = new GameObject("Line"+linenum).AddComponent<LineRenderer>();
        line.material = new Material(Shader.Find("Unlit/Texture"));
        line.gameObject.layer = LayerMask.NameToLayer("Ground");
        rid = GameObject.Find("Line"+linenum).AddComponent<Rigidbody2D>();
        line.startWidth = 0.1f;
        line.startColor = Color.white;
        line.useWorldSpace = false;
        rid.simulated = false;
        rid.mass = 200;
    
    }

  
}
