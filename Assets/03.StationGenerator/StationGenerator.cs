using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationGenerator : MonoBehaviour
{

    public int sizeX;
    public int sizeZ;

    public StationPrefabs prefabs;

    private void Start() {
        for (int x=0; x<sizeX; ++x) {
            for (int z=0; z<sizeZ; ++z) {
                GameObject obj = prefabs.GetOpen();
                obj.transform.position = new Vector3(prefabs.voxelSize*x,0f,prefabs.voxelSize*z);
            }
        }
    }

}
