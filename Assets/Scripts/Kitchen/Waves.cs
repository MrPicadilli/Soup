
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{
    private Mesh mesh;
    private Vector3[] vert;
    private Vector3[] norm;
    Vector3[] newVert;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vert = mesh.vertices;
        norm = mesh.normals;
        newVert = mesh.vertices;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < vert.Length; i++)
        {
            newVert[i] = vert[i] + norm[i] * WaveManager.instance.GetWaveLength(vert[i].x, vert[i].z);
        }

        mesh.vertices = newVert;
        mesh.RecalculateNormals();
    }
}
