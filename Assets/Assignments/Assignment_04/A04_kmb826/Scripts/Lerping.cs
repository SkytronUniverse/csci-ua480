using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerping : MonoBehaviour {

    public Transform[] controlPoints;
    public float t;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = DumbSpline(Mathf.Clamp01(t));
	}

    private Vector3 DumbSpline(float x)
    {
        Vector3 a = Vector3.Lerp(controlPoints[0].position, controlPoints[1].position, t);
        Vector3 b = Vector3.Lerp(controlPoints[1].position, controlPoints[2].position, t);
        return Vector3.Lerp(a, b, t);
    }
}
