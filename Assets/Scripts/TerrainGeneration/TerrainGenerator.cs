using System;
using System.Collections;
using UnityEngine;

public class TerrainGenerator {
    
    public int Scale { get; private set; }
    public int Resolution { get; private set; } 
    public float NoiseScale { get; private set; }
    public float HeightMultiplier { get; private set; }

    public Mesh TerrainMesh { get; private set; }


    private float[,] heightmap;
    private int seed;


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

    }

    private void GenerateHeightmap()
    {
        heightmap = new float[Scale+1,Scale+1];

        for (int i = 0, z = 0; z <= Scale; z++)
        {
            for (int x = 0; x <= Scale; x++, i++)
            {
                float height = Mathf.PerlinNoise(seed + x / (NoiseScale * 10) + 0.01f, seed + z / (NoiseScale * 10) + 0.01f);
                height += Mathf.PerlinNoise(seed + x / NoiseScale + 0.01f, seed + z / NoiseScale + 0.01f) *0.1f;
                height *= (Mathf.PerlinNoise(seed + x / (NoiseScale * 10) + 0.01f, seed + z / (NoiseScale * 10) + 0.01f) <= 0.2f) ? 0.9f : 1;

                heightmap[x, z] = height * HeightMultiplier;
            }
        }

    }

    private void GenerateMesh()
    {
        Vector3[] vertices;
        Vector2[] uvs;
        int[] triangles;

        vertices = new Vector3[(Scale + 1) * (Scale + 1)];
        uvs = new Vector2[(Scale + 1) * (Scale + 1)];
        for (int i = 0, z = 0; z <= Scale; z++)
        {
            for (int x = 0; x <= Scale; x++, i++)
            {

                vertices[i] = new Vector3(x - (Scale / 2), heightmap[x,z], z - (Scale / 2));

                uvs[i] = new Vector2(x * (1.0f / Scale), z * (1.0f / Scale));

            }
        }

        triangles = new int[Scale * Scale * 6];
        for (int ti = 0, vi = 0, z = 0; z < Scale; z++, vi++)
        {
            for (int x = 0; x < Scale; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + Scale + 1;
                triangles[ti + 5] = vi + Scale + 2;
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

        


    }

}
