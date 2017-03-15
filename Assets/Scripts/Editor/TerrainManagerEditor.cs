using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



[CustomEditor(typeof(TerrainManager))]
public class TerrainManagerEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TerrainManager manager = (TerrainManager)target;
        if (GUILayout.Button("Generate Terrain"))
        {
            if (manager.randomSeed) manager.terrainSeed = Random.Range(0, 99999);
            manager.GenerateTerrain();
        }
    }
}
