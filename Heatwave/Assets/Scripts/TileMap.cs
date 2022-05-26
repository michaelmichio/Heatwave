using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class TileMap : MonoBehaviour {
	
	public int inputSize_x = 5;
	public int inputSize_z = 5;
	public float tileSize = 1.0f;
	
	// Use this for initialization
	void Start () {
		BuildMesh();
	}
	
	public void BuildMesh() {
		
		int size_x = inputSize_x * 2 + 1;
		int size_z = inputSize_z * 2 + 1;
		
		int numTiles = size_x * size_z;
		int numTris = numTiles * 2;
		
		int vsize_x = size_x + 1;
		int vsize_z = size_z + 1;
		int numVerts = vsize_x * vsize_z;
		
		// Generate the mesh data
		Vector3[] vertices = new Vector3[ numVerts ];
		Vector3[] normals = new Vector3[ numVerts ];
		Vector2[] uv = new Vector2[ numVerts ];
		
		int[] triangles = new int[ numTris * 3 ];

		int x, z;
		int tileX = 0;
		int vertX = 0;
		int tileZ = 0;
		int vertZ = 0;
		for(z=0; z < vsize_z; z++) {
			if(vertZ==2) {
				tileZ++;
				vertZ = 1;
			}
			else {
				vertZ++;
			}

			for(x=0; x < vsize_x; x++) {

				vertices[ z * vsize_x + x ] = new Vector3( tileX, 0, tileZ );
				normals[ z * vsize_x + x ] = Vector3.up;
				uv[ z * vsize_x + x ] = new Vector2( (float)x / vsize_x, (float)z / vsize_z );

				if(vertX==2){
					tileX++;
					vertX = 1;
				}
				else {
					vertX++;
				}

				if(x==0 || z==0 || x==vsize_x-1 || z==vsize_z-1) {
					vertices[ z * vsize_x + x ] = new Vector3( tileX, -1, tileZ );
				}
				else{
					vertices[ z * vsize_x + x ] = new Vector3( tileX, 0, tileZ );
				}

				// vertices[ z * vsize_x + x ] = new Vector3( tileX, 0, tileZ );

				normals[ z * vsize_x + x ] = Vector3.up;
				uv[ z * vsize_x + x ] = new Vector2( (float)x / vsize_x, (float)z / vsize_z );
			}

			tileX = 0;
			vertX = 0;

		}
		// for(z=0; z < vsize_z; z++) {
		// 	for(x=0; x < vsize_x; x++) {
		// 		vertices[ z * vsize_x + x ] = new Vector3( x*tileSize, 0, z*tileSize );
		// 		normals[ z * vsize_x + x ] = Vector3.up;
		// 		uv[ z * vsize_x + x ] = new Vector2( (float)x / vsize_x, (float)z / vsize_z );
		// 	}
		// }
		Debug.Log ("Done Verts!");
		
		
		for(z=0; z < size_x; z++) {
			for(x=0; x < size_x; x++) {
				int squareIndex = z * size_x + x;
				int triOffset = squareIndex * 6;
				
				triangles[triOffset + 0] = z * vsize_x + x + 		   0;
				triangles[triOffset + 1] = z * vsize_x + x + vsize_x + 0;
				triangles[triOffset + 2] = z * vsize_x + x + vsize_x + 1;
				
				triangles[triOffset + 3] = z * vsize_x + x + 		   0;
				triangles[triOffset + 4] = z * vsize_x + x + vsize_x + 1;
				triangles[triOffset + 5] = z * vsize_x + x + 		   1;
			}
		}
		Debug.Log ("Done Triangles!");
		
		// Create a new Mesh and populate with the data
		Mesh mesh = new Mesh();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		// mesh.normals = normals;
		mesh.uv = uv;
		mesh.RecalculateNormals();
		
		// Assign our mesh to our filter/renderer/collider
		MeshFilter mesh_filter = GetComponent<MeshFilter>();
		MeshRenderer mesh_renderer = GetComponent<MeshRenderer>();
		MeshCollider mesh_collider = GetComponent<MeshCollider>();
		
		mesh_filter.mesh = mesh;
		mesh_collider.sharedMesh = mesh;
		Debug.Log ("Done Mesh!");
		
	}
	
}
