using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class Quad : MonoBehaviour
    {

    void Start()
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        Vector3[] vertices = new Vector3[4];
        Vector3[] normals = new Vector3[4];

        vertices[0] = new Vector3(-1, 1, 0);
        vertices[1] = new Vector3(1, 1, 0);
        vertices[2] = new Vector3(1, -1, 0);
        vertices[3] = new Vector3(-1, -1, 0);
        

        int[] triangles = new int[] { 0, 1, 3, 3, 1, 2, 1, 2, 3 };

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        normals[0] = -Vector3.forward;
        normals[1] = Vector3.forward;
        normals[2] = -Vector3.up;
        normals[3] = Vector3.up;

        mesh.normals = normals;

        Vector2[] uv = new Vector2[8];
        uv[0] = new Vector2(0, 1);
        uv[1] = new Vector2(1, 0);
        uv[2] = new Vector2(0, 0);
        uv[3] = new Vector2(1, 1);

        mesh.uv = uv;
    }


}
