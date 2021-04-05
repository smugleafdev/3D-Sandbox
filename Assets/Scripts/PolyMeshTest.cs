using UnityEngine;

public class PolyMeshTest : MonoBehaviour {

    public Material material;

    private void Start() {
        Vector3[] vertices = new Vector3[3];
        Vector2[] uv = new Vector2[3];
        int[] triangles = new int[3];

        vertices[0] = new Vector3(0, 0);
        vertices[1] = new Vector3(0, 1);
        vertices[2] = new Vector3(1, 0);

        uv[0] = new Vector3(0, 0);
        uv[1] = new Vector3(0, 1);
        uv[2] = new Vector3(1, 0);

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

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
    }
}