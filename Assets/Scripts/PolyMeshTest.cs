using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolyMeshTest : MonoBehaviour {

    public Material material;

    private void Start() {
        Vector3[] vertices = new Vector3[4];
        Vector2[] uv = new Vector2[4];
        int[] triangles = new int[6];

        vertices[0] = new Vector3(0, 0);
        vertices[1] = new Vector3(0, 1);
        vertices[2] = new Vector3(1, 0);
        // vertices[3] = new Vector3(1, 0);

        uv[0] = new Vector3(0, 0);
        uv[1] = new Vector3(0, 1);
        uv[2] = new Vector3(1, 0);
        // uv[3] = new Vector3(1, 0);

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        // triangles[3] = 2;
        // triangles[4] = 1;
        // triangles[5] = 3;

        Mesh mesh = new Mesh();

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();

        GameObject gameObject = new GameObject("Meshy", typeof(MeshFilter), typeof(MeshRenderer));
        gameObject.transform.localScale = new Vector3(10, 10, 1);

        gameObject.GetComponent<MeshFilter>().mesh = mesh;

        gameObject.GetComponent<MeshRenderer>().material = material;

        MeshCollider meshC = gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;
        meshC.sharedMesh = mesh;
        // meshC.convex = true;
    }

    // public Material m_Material;
    // GameObject m_PlanetMesh;

    // List<Polygon> m_Polygons;
    // List<Vector3> m_Vertices;
    // List<Vector2> uv;

    // // Start is called before the first frame update
    // void Start() {
    //     m_Polygons = new List<Polygon>();
    //     m_Vertices = new List<Vector3>();
    //     uv = new List<Vector2>();

    //     m_Vertices.Add(new Vector3(0, 1, 0));
    //     m_Vertices.Add(new Vector3(1, 1, 0));
    //     m_Vertices.Add(new Vector3(0, 0, 0));
    //     // m_Vertices.Add(new Vector3(1, 0, 0));

    //     // uv.Add(new Vector2(0, 1));
    //     // uv.Add(new Vector2(1, 1));
    //     // uv.Add(new Vector2(0, 0));
    //     // uv.Add(new Vector2(1, 0));

    //     m_Polygons.Add(new Polygon(0, 1, 2));
    //     // m_Polygons.Add(new Polygon(2, 1, 3));

    //     GenerateMesh();
    // }

    // public void GenerateMesh() {
    //     if (m_PlanetMesh)
    //         Destroy(m_PlanetMesh);

    //     m_PlanetMesh = new GameObject("square test");
    //     m_PlanetMesh.transform.localScale = new Vector3(10, 10, 1); // TEST

    //     MeshRenderer surfaceRenderer = m_PlanetMesh.AddComponent<MeshRenderer>();
    //     surfaceRenderer.material = m_Material;

    //     Mesh terrainMesh = new Mesh();

    //     int vertexCount = m_Polygons.Count * 3;

    //     int[] indices = new int[vertexCount];

    //     Vector3[] vertices = new Vector3[vertexCount];
    //     Vector3[] normals = new Vector3[vertexCount];
    //     Color32[] colors = new Color32[vertexCount];

    //     Color32 green = new Color32(20, 255, 30, 255);
    //     Color32 brown = new Color32(220, 150, 70, 255);

    //     for (int i = 0; i < m_Polygons.Count; i++) {
    //         var poly = m_Polygons[i];

    //         indices[i * 3 + 0] = i * 3 + 0;
    //         indices[i * 3 + 1] = i * 3 + 1;
    //         indices[i * 3 + 2] = i * 3 + 2;

    //         vertices[i * 3 + 0] = m_Vertices[poly.m_Vertices[0]];
    //         vertices[i * 3 + 1] = m_Vertices[poly.m_Vertices[1]];
    //         vertices[i * 3 + 2] = m_Vertices[poly.m_Vertices[2]];

    //         Color32 polyColor = Color32.Lerp(green, brown, Random.Range(0.0f, 1.0f));

    //         colors[i * 3 + 0] = polyColor;
    //         colors[i * 3 + 1] = polyColor;
    //         colors[i * 3 + 2] = polyColor;

    //         normals[i * 3 + 0] = m_Vertices[poly.m_Vertices[0]];
    //         normals[i * 3 + 1] = m_Vertices[poly.m_Vertices[1]];
    //         normals[i * 3 + 2] = m_Vertices[poly.m_Vertices[2]];
    //     }

    //     foreach (Vector3 vert in vertices) {
    //         uv.Add(new Vector2(vert.x, vert.y));
    //     }

    //     terrainMesh.vertices = vertices;
    //     terrainMesh.normals = normals;
    //     terrainMesh.colors32 = colors;
    //     terrainMesh.uv = uv.ToArray();

    //     terrainMesh.SetTriangles(indices, 0);

    //     MeshFilter terrainFilter = m_PlanetMesh.AddComponent<MeshFilter>();
    //     terrainFilter.mesh = terrainMesh;

    //     MeshCollider meshC = m_PlanetMesh.AddComponent(typeof(MeshCollider)) as MeshCollider;
    //     meshC.sharedMesh = terrainMesh;
    //     meshC.convex = true;
    // }
}
