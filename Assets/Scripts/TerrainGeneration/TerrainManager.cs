using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour {

    TerrainGenerator terrainGenerator;
    public int terrainScale = 10;
    public int terrainResolution = 1;
    public int terrainNoiseScale = 100;
    public int terrainHeightMultiplier = 10;

    public int terrainSeed;
    public bool randomSeed = true;

    // Use this for initialization
    void Start () {
        GenerateTerrain();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GenerateTerrain()
    {   
        terrainGenerator = new TerrainGenerator(terrainScale, terrainResolution, terrainNoiseScale, terrainHeightMultiplier, terrainSeed);
        terrainGenerator.Generate();
        MeshFilter mf = this.GetComponent<MeshFilter>();
        mf.mesh = terrainGenerator.TerrainMesh;
    }
}
