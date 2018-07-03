using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeAFace : MonoBehaviour {

    public GameObject[] hairs;
    public GameObject[] eyes;
    public GameObject[] noses;
    public GameObject[] mouths;

    GameObject hair, LEye, REye, nose, mouth;

    public Vector2 minMaxHair = Vector2.one;

    void Start () {
        hair = Instantiate(hairs[Random.Range(0, hairs.Length)]);
        LEye = Instantiate(eyes[Random.Range(0, eyes.Length)]);
        REye = Instantiate(LEye);
        nose = Instantiate(mouths[Random.Range(0, noses.Length)]);
        mouth = Instantiate(noses[Random.Range(0, mouths.Length)]);

        hair.transform.localPosition = new Vector3(0, 1, 0);
        float hairScale = Random.Range(minMaxHair.x, minMaxHair.y);
        hair.transform.localScale = new Vector3(hairScale, hairScale, hairScale);
        LEye.transform.localPosition = new Vector3(-.5f, 0, 0);
        REye.transform.localPosition = new Vector3(.5f, 0, 0);
        REye.transform.localScale = new Vector3(-1f*LEye.transform.localScale.x, LEye.transform.localScale.y, 1);
        nose.transform.localPosition = new Vector3(0, -.5f, 0);
        mouth.transform.localPosition = new Vector3(0, -1, 0);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
