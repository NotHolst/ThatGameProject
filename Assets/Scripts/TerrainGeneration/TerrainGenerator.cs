using System;
using System.Collections;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour{

    public int Scale;
    public int Resolution;
    public float NoiseScale;
    public float HeightMultiplier;

    public Mesh TerrainMesh { get; private set; }


    private float[,] heightmap;
    public int seed;
    public bool randomSeed = true;


    public GameObject[] TreePrefabs;
    public GameObject[] StonePrefabs;
    public GameObject[] GoldPrefabs;

    public TerrainGenerator(int scale, int resolution, float noiseScale, float heightMultiplier, int seed)
    {
        this.Scale = scale;
        if (resolution > 0) this.Resolution = resolution;
        else this.Resolution = 1;
        this.NoiseScale = noiseScale;
        this.HeightMultiplier = heightMultiplier;
        this.seed = seed;
    }

	public void Generate()
    {

        GenerateHeightmap();
        GenerateMesh();
        GenerateObjects();

        this.GetComponent<MeshFilter>().mesh = this.TerrainMesh;

    }

    private void GenerateHeightmap()
    {
        heightmap = new float[Resolution+1, Resolution + 1];

        for (int i = 0, z = 0; z <= Resolution; z++)
        {
            for (int x = 0; x <= Resolution; x++, i++)
            {
                float height = Mathf.PerlinNoise(seed + x / (NoiseScale * 10) + 0.01f, seed + z / (NoiseScale * 10) + 0.01f)+0.2f;
                height += Mathf.PerlinNoise(seed + x / NoiseScale + 0.01f, seed + z / NoiseScale + 0.01f) *0.05f;

                if (height > 0.6f)
                    height = 0.6f;

                if (height < 0.47f)
                    height *= 1.47f-(height*1.25f);

                if (x == 0 || x == Resolution || z == 0 || z == Resolution)
                    height = 0.5f;

                heightmap[x, z] = height * HeightMultiplier;
            }
        }

    }

    private void GenerateMesh()
    {
        Vector3[] vertices;
        Vector2[] uvs;
        int[] triangles;

        vertices = new Vector3[(Resolution + 1) * (Resolution + 1)];
        uvs = new Vector2[(Resolution + 1) * (Resolution + 1)];
        for (int i = 0, z = 0; z <= Resolution; z++)
        {
            for (int x = 0; x <= Resolution; x++, i++)
            {

                vertices[i] = new Vector3((x - (Resolution / 2))*Scale, (heightmap[x,z] - (HeightMultiplier/2))* Scale, (z - (Resolution / 2))*Scale);

                uvs[i] = new Vector2(x * (1.0f / Resolution), z * (1.0f / Resolution));

            }
        }

        triangles = new int[Resolution * Resolution * 6];
        for (int ti = 0, vi = 0, z = 0; z < Resolution; z++, vi++)
        {
            for (int x = 0; x < Resolution; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + Resolution + 1;
                triangles[ti + 5] = vi + Resolution + 2;
            }
        }

        Mesh m = new Mesh();
        m.vertices = vertices;
        m.uv = uvs;
        m.triangles = triangles;
        m.RecalculateNormals();

        TerrainMesh = m;
    }

    private void GenerateObjects()
    {

        for (int x = 0; x < heightmap.GetLength(0); x++)
        {
            for (int z = 0; z < heightmap.GetLength(1); z++)
            {
                
            }
        }

    }

}
