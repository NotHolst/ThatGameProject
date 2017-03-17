using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



[CustomEditor(typeof(TerrainGenerator))]
public class TerrainGeneratorEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TerrainGenerator manager = (TerrainGenerator)target;
        if (GUILayout.Button("Generate Terrain"))
        {
            if (manager.randomSeed) manager.seed = Random.Range(0, 99999);
            manager.Generate();
        }
    }
}
