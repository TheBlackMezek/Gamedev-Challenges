using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationGenerator : MonoBehaviour
{

    public int coreDiameter;
    public int coreHeight;

    public StationPrefabs prefabs;

    private void Start() {
        GameObject core = BuildRoom(new Vector2Int(-coreDiameter/2,-coreDiameter/2), new Vector2Int(coreDiameter,coreDiameter));
        core.transform.parent = transform;
    }

    private GameObject BuildRoom(Vector2Int start, Vector2Int size) {
        GameObject root = new GameObject();
        root.name = "Room";
        for (int x=0; x<size.x; ++x) {
            for (int z=0; z<size.y; ++z) {
                GameObject obj;
                if (x==0 && z==0) {
                    obj = prefabs.GetCorner(2);
                } else if (x==0 && z==size.y-1) {
                    obj = prefabs.GetCorner(3);
                } else if (x==size.x-1 && z==size.y-1) {
                    obj = prefabs.GetCorner(0);
                } else if (x==size.x-1 && z==0) {
                    obj = prefabs.GetCorner(1);
                } else if (x==0) {
                    obj = prefabs.GetWall(3);
                } else if (x==size.x-1) {
                    obj = prefabs.GetWall(1);
                } else if (z==0) {
                    obj = prefabs.GetWall(2);
                } else if (z==size.y-1) {
                    obj = prefabs.GetWall(0);
                } else {
                    obj = prefabs.GetOpen();
                }
                obj.transform.position = new Vector3(prefabs.voxelSize*(x+start.x),0f,prefabs.voxelSize*(z+start.y));
                obj.transform.parent = root.transform;
            }
        }
        return root;
    }

}
