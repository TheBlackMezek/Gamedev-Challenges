using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationGenerator : MonoBehaviour
{

    public MeshFilter filter;

    private void Start() {
        Vector3[] vertices = new Vector3[] {
            new Vector3(-1,0,-1),
            new Vector3(-1,0,1),
            new Vector3(1,0,1),
            new Vector3(1,0,-1)
        };
        int[] triangles = new int[] {
            0, 1, 3,
            1, 2, 3
        };
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        filter.mesh = mesh;
    }

}
