using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationGenerator : MonoBehaviour
{

    public int hallLength;

    public StationPrefabs prefabs;

    private void Start() {
        for (int i=0; i<hallLength; ++i) {
            GameObject obj = prefabs.GetHall();
            obj.transform.position = new Vector3(0f,0f,prefabs.voxelSize*i);
        }
    }

}
