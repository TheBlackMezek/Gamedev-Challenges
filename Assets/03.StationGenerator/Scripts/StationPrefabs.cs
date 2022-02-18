using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationPrefabs : MonoBehaviour
{

    public float voxelSize;
    public float doorHeight;
    public float doorWidth;
    public Material material;

    public Mesh wall { get; private set; }
    public Mesh wallDoor { get; private set; }
    public Mesh wallWindow { get; private set; }
    public Mesh floor { get; private set; }
    public Mesh ceiling { get; private set; }

    private void Awake() {
        BuildMeshWall();
        BuildMeshFloor();
        BuildMeshCeiling();
        BuildMeshWallDoor();
    }

    private void AddFloor(GameObject root) {
        GameObject obj = new GameObject();
        obj.name = "Floor";
        obj.transform.parent = root.transform;
        MeshFilter filter = obj.AddComponent<MeshFilter>();
        filter.mesh = floor;
        MeshRenderer renderer = obj.AddComponent<MeshRenderer>();
        renderer.materials[0] = material;
    }

    private void AddCeiling(GameObject root) {
        GameObject obj = new GameObject();
        obj.name = "Ceiling";
        obj.transform.parent = root.transform;
        MeshFilter filter = obj.AddComponent<MeshFilter>();
        filter.mesh = ceiling;
        MeshRenderer renderer = obj.AddComponent<MeshRenderer>();
        renderer.materials[0] = material;
        obj.transform.localPosition += new Vector3(0f,voxelSize,0f);
    }

    private void AddWall(GameObject root, int dir) {
        //dir: 0=north, 1=east, 2=south, 3=west
        GameObject obj = new GameObject();
        obj.name = "Wall";
        obj.transform.parent = root.transform;
        MeshFilter filter = obj.AddComponent<MeshFilter>();
        filter.mesh = wall;
        MeshRenderer renderer = obj.AddComponent<MeshRenderer>();
        renderer.materials[0] = material;
        float radius = voxelSize/2f;
        if (dir == 0) {
            obj.transform.localPosition += new Vector3(0f,radius,radius);
        } else if (dir == 1) {
            obj.transform.eulerAngles += new Vector3(0f,90f,0f);
            obj.transform.localPosition += new Vector3(radius,radius,0f);
        } else if (dir == 2) {
            obj.transform.eulerAngles += new Vector3(0f,180f,0f);
            obj.transform.localPosition += new Vector3(0f,radius,-radius);
        } else if (dir == 3) {
            obj.transform.eulerAngles += new Vector3(0f,270f,0f);
            obj.transform.localPosition += new Vector3(-radius,radius,0f);
        }
    }

    private void AddWallDoor(GameObject root, int dir) {
        //dir: 0=north, 1=east, 2=south, 3=west
        GameObject obj = new GameObject();
        obj.name = "WallDoor";
        obj.transform.parent = root.transform;
        MeshFilter filter = obj.AddComponent<MeshFilter>();
        filter.mesh = wallDoor;
        MeshRenderer renderer = obj.AddComponent<MeshRenderer>();
        renderer.materials[0] = material;
        float radius = voxelSize/2f;
        if (dir == 0) {
            obj.transform.localPosition += new Vector3(0f,0f,radius);
        } else if (dir == 1) {
            obj.transform.eulerAngles += new Vector3(0f,90f,0f);
            obj.transform.localPosition += new Vector3(radius,0f,0f);
        } else if (dir == 2) {
            obj.transform.eulerAngles += new Vector3(0f,180f,0f);
            obj.transform.localPosition += new Vector3(0f,0f,-radius);
        } else if (dir == 3) {
            obj.transform.eulerAngles += new Vector3(0f,270f,0f);
            obj.transform.localPosition += new Vector3(-radius,0f,0f);
        }
    }

    public GameObject GetHall() {
        //Default direction is North-South
        GameObject root = new GameObject();
        root.name = "Hall";

        AddFloor(root);
        AddWall(root, 1);
        AddWall(root, 3);
        AddCeiling(root);

        return root;
    }

    public GameObject GetWall(int dir) {
        //dir: 0=north, 1=east, 2=south, 3=west
        GameObject root = new GameObject();
        root.name = "Wall";

        AddFloor(root);
        AddWall(root, dir);
        AddCeiling(root);

        return root;
    }

    public GameObject GetWallDoor(int dir) {
        //dir: 0=north, 1=east, 2=south, 3=west
        GameObject root = new GameObject();
        root.name = "WallDoor";

        AddFloor(root);
        AddWallDoor(root, dir);
        AddCeiling(root);

        return root;
    }

    public GameObject GetCorner(int dir) {
        //dir: 0=NE 1=SE 2=SW 3=NW
        GameObject root = new GameObject();
        root.name = "Corner";

        AddFloor(root);
        AddCeiling(root);
        if (dir == 0) {
            AddWall(root, 0);
            AddWall(root, 1);
        } else if (dir == 1) {
            AddWall(root, 1);
            AddWall(root, 2);
        } else if (dir == 2) {
            AddWall(root, 2);
            AddWall(root, 3);
        } else if (dir == 3) {
            AddWall(root, 3);
            AddWall(root, 0);
        }

        return root;
    }

    public GameObject GetOpen() {
        //A voxel with a floor and ceiling but no walls
        GameObject root = new GameObject();
        root.name = "Open space";

        AddFloor(root);
        AddCeiling(root);

        return root;
    }

    private void BuildMeshWall() {
        float radius = voxelSize/2f;
        Vector3[] vertices = new Vector3[] {
            new Vector3(-radius,-radius,0f),
            new Vector3(-radius,radius,0f),
            new Vector3(radius,radius,0f),
            new Vector3(radius,-radius,0f)
        };
        int[] triangles = new int[] {
            0, 1, 3,
            1, 2, 3
        };
        wall = new Mesh();
        wall.vertices = vertices;
        wall.triangles = triangles;
        wall.RecalculateNormals();
    }

    private void BuildMeshWallDoor() {
        float radius = voxelSize/2f;
        float halfWidth = doorWidth/2f;
        Vector3[] vertices = new Vector3[] {
            //West column
            new Vector3(-radius,0f,0f),
            new Vector3(-radius,voxelSize,0f),
            new Vector3(-halfWidth,voxelSize,0f),
            new Vector3(-halfWidth,0f,0f),
            //East column
            new Vector3(radius,0f,0f),
            new Vector3(radius,voxelSize,0f),
            new Vector3(halfWidth,voxelSize,0f),
            new Vector3(halfWidth,0f,0f),
            //Doorframe top points
            new Vector3(-halfWidth,doorHeight,0f),
            new Vector3(halfWidth,doorHeight,0f)
        };
        int[] triangles = new int[] {
            //West column
            0, 1, 3,
            1, 2, 3,
            //East column
            7, 6, 4,
            6, 5, 4,
            //Doorframe top
            8, 2, 9,
            2, 6, 9
        };
        wallDoor = new Mesh();
        wallDoor.vertices = vertices;
        wallDoor.triangles = triangles;
        wallDoor.RecalculateNormals();
    }

    private void BuildMeshFloor() {
        float radius = voxelSize/2f;
        Vector3[] vertices = new Vector3[] {
            new Vector3(-radius,0f,-radius),
            new Vector3(-radius,0f,radius),
            new Vector3(radius,0f,radius),
            new Vector3(radius,0f,-radius)
        };
        int[] triangles = new int[] {
            0, 1, 3,
            1, 2, 3
        };
        floor = new Mesh();
        floor.vertices = vertices;
        floor.triangles = triangles;
        floor.RecalculateNormals();
    }

    private void BuildMeshCeiling() {
        float radius = voxelSize/2f;
        Vector3[] vertices = new Vector3[] {
            new Vector3(-radius,0f,-radius),
            new Vector3(-radius,0f,radius),
            new Vector3(radius,0f,radius),
            new Vector3(radius,0f,-radius)
        };
        int[] triangles = new int[] {
            3, 1, 0,
            3, 2, 1
        };
        ceiling = new Mesh();
        ceiling.vertices = vertices;
        ceiling.triangles = triangles;
        ceiling.RecalculateNormals();
    }

}
