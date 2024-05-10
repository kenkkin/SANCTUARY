using UnityEngine;
using System.Collections.Generic; 
 
public class Water : MonoBehaviour
{
	[Header("Waves")]
	[SerializeField] float size = 1;
	[SerializeField] int grid = 16;

	private MeshFilter meshFilter;

	[Space]
	[Header("Noise")]
	[SerializeField] float power = 3;
	[SerializeField] float scale = 1;
	[SerializeField] float timeScale = 1;
	private float xOffset;
	private float yOffset;
	private MeshFilter mf;

	private void Start()
	{
		meshFilter = GetComponent<MeshFilter>();
		meshFilter.mesh = GenerateMesh();

		mf = GetComponent<MeshFilter>();
		MakeNoise();
	}

	private Mesh GenerateMesh()
	{
		Mesh m = new Mesh();

		var vertices = new List<Vector3>();
		var normals = new List<Vector3>();
		var uvs = new List<Vector2>();

		for(int x = 0; x < grid + 1; x++)
		{		
			for(int y = 0; y < grid + 1; y++)	
			{
				vertices.Add(new Vector3(-size * 0.5f + size * (x / ((float)grid)), 0, -size * 0.5f + size * (y / ((float)grid))));
				normals.Add(Vector3.up);
				uvs.Add(new Vector2(x / (float)grid, y / (float)grid));
			}
		}

		var triangles = new List<int>();
		var vertCount = grid + 1;
		for(int i = 0; i < vertCount * vertCount - vertCount; i++)
		{
			if((i + 1) % vertCount == 0)	
			{
				continue;
			}
		
			triangles.AddRange(new List<int>() {i + 1 + vertCount, i + vertCount, i, i, i + 1, i + vertCount + 1});
		}

		m.SetVertices(vertices);
		m.SetNormals(normals);
		m.SetUVs(0, uvs);
		m.SetTriangles(triangles, 0);

		return m;
	}

	private void Update()
	{
		MakeNoise();
		xOffset += Time.deltaTime * timeScale;
		yOffset += Time.deltaTime * timeScale;
	}

	private void MakeNoise()
	{
		Vector3[] vertices = mf.mesh.vertices;

		for(int i = 0; i < vertices.Length; i++)
		{
			vertices[i].y = CalculateHeight(vertices[i].x, vertices[i].z) * power;
		}

		mf.mesh.vertices = vertices;
	}

	float CalculateHeight(float x, float y)
	{
		float xCord = x * scale + xOffset;
		float yCord = y * scale + yOffset;

		return Mathf.PerlinNoise(xCord, yCord);
	}
}