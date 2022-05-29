using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshController : MonoBehaviour
{

    [Range(1.5f, 5f)]
    public float radius = 2f;

    [Range(1.5f, 5f)]
    public float deformationStrength = 2f;
    
    public GameObject terrainTexture;
    public GameObject terrainPath;

    private Mesh meshTexture, meshPath;
    private Vector3[] verticiesTexture, modifiedVertsTexture, verticiesPath, modifiedVertsPath;
    
    // Start is called before the first frame update
    void Start()
    {
        meshTexture = terrainTexture.GetComponent<MeshFilter>().mesh;
        verticiesTexture = meshTexture.vertices;
        modifiedVertsTexture = meshTexture.vertices;

        meshPath = terrainPath.GetComponent<MeshFilter>().mesh;
        verticiesPath = meshPath.vertices;
        modifiedVertsPath = meshPath.vertices;
    }

    void RecalculateMesh() {
        meshTexture.vertices = modifiedVertsTexture;

        MeshFilter mesh_filter_T = terrainTexture.GetComponent<MeshFilter>();
		MeshRenderer mesh_renderer_T = terrainTexture.GetComponent<MeshRenderer>();
		MeshCollider mesh_collider_T = terrainTexture.GetComponent<MeshCollider>();

        mesh_filter_T.mesh = meshTexture;
        mesh_collider_T.sharedMesh = meshTexture;
        
        // terrainTexture.GetComponent<MeshFilter>().mesh = meshTexture;
        // terrainTexture.GetComponent<MeshFilter>().sharedMesh = meshTexture;
        meshTexture.RecalculateNormals();

        meshPath.vertices = modifiedVertsPath;

        MeshFilter mesh_filter_P = terrainPath.GetComponent<MeshFilter>();
		MeshRenderer mesh_renderer_P = terrainPath.GetComponent<MeshRenderer>();
		MeshCollider mesh_collider_P = terrainPath.GetComponent<MeshCollider>();

        mesh_filter_P.mesh = meshPath;
        mesh_collider_P.sharedMesh = meshPath;
        
        // terrainPath.GetComponent<MeshFilter>().mesh = meshPath;
        // terrainPath.GetComponent<MeshFilter>().sharedMesh = meshPath;
        meshPath.RecalculateNormals();
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKey(KeyCode.W)) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, Mathf.Infinity)) {

                for (int v = 0; v < modifiedVertsTexture.Length; v++) {
                    Vector3 distance = modifiedVertsTexture[v] - hit.point;

                    float smoothingFactor = 2f;
                    float force = deformationStrength / (1f + hit.point.sqrMagnitude);
                    
                    if(distance.sqrMagnitude < radius) {
                        modifiedVertsTexture[v] = modifiedVertsTexture[v] + (Vector3.up * force) / smoothingFactor;
                        RecalculateMesh();
                    }
                }

                for (int v = 0; v < modifiedVertsPath.Length; v++) {
                    Vector3 distance = modifiedVertsPath[v] - hit.point;

                    float smoothingFactor = 2f;
                    float force = deformationStrength / (1f + hit.point.sqrMagnitude);
                    
                    if(distance.sqrMagnitude < radius) {
                        modifiedVertsPath[v] = modifiedVertsPath[v] + (Vector3.up * force) / smoothingFactor;
                        RecalculateMesh();
                    }
                }

            }
        }

        if(Input.GetKey(KeyCode.S)) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, Mathf.Infinity)) {

                for (int v = 0; v < modifiedVertsTexture.Length; v++) {
                    Vector3 distance = modifiedVertsTexture[v] - hit.point;

                    float smoothingFactor = 2f;
                    float force = deformationStrength / (1f + hit.point.sqrMagnitude);
                    
                    if(distance.sqrMagnitude < radius) {
                        modifiedVertsTexture[v] = modifiedVertsTexture[v] + (Vector3.down * force) / smoothingFactor;
                        RecalculateMesh();
                    }
                    
                }

                for (int v = 0; v < modifiedVertsPath.Length; v++) {
                    Vector3 distance = modifiedVertsPath[v] - hit.point;

                    float smoothingFactor = 2f;
                    float force = deformationStrength / (1f + hit.point.sqrMagnitude);
                    
                    if(distance.sqrMagnitude < radius) {
                        modifiedVertsPath[v] = modifiedVertsPath[v] + (Vector3.down * force) / smoothingFactor;
                        RecalculateMesh();
                    }
                }

            }
        }

        if(Input.GetKeyDown(KeyCode.A)) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, Mathf.Infinity)) {
                int hitTri = hit.triangleIndex;
                Debug.Log(hitTri);
                Debug.Log(modifiedVertsPath.Length);
                modifiedVertsPath[(hitTri-(8*1)) + 0].y += 1;
                modifiedVertsPath[(hitTri-(8*1)) + 1].y += 1;
                modifiedVertsPath[(hitTri-(8*0)) + 0].y += 1;
                modifiedVertsPath[(hitTri-(8*0)) + 1].y += 1;
                
            }

            // Vector3 p0 = modifiedVertsPath[triangles[hitTri * 3 + 0]];
            // Vector3 p1 = modifiedVertsPath[triangles[hitTri * 3 + 1]];
            // Vector3 p2 = modifiedVertsPath[triangles[hitTri * 3 + 2]];

            // modifiedVertsTexture[v] =
            RecalculateMesh();
        }

    }

}