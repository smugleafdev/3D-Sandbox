using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pane : MonoBehaviour {
    [SerializeField]
    Material transparentMaterial, glowMaterial;
    GameObject exterior, interior;
    float scale = 5f;
    Vector3 a, b, c;
    int id = 0;

    public void InitPane(Vector3 a, Vector3 b, Vector3 c, int id) {
        this.a = a;
        this.b = b;
        this.c = c;
        this.id = id;

        CreateExteriorPane();
        CreateInteriorPane();

        // this.name = $"Pane {id}";
    }

    void CreateExteriorPane() {
        Vector3[] vertices = new Vector3[3];
        Vector2[] uv = new Vector2[3];
        int[] triangles = new int[3];

        vertices[0] = a;
        vertices[1] = b;
        vertices[2] = c;

        uv[0] = a;
        uv[1] = b;
        uv[2] = c;

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        Mesh mesh = new Mesh();

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();

        exterior = new GameObject($"Exterior {id}", typeof(MeshFilter), typeof(MeshRenderer));
        exterior.transform.localScale = new Vector3(scale, scale, scale);
        exterior.GetComponent<MeshFilter>().mesh = mesh;
        exterior.GetComponent<MeshRenderer>().material = transparentMaterial;
        MeshCollider meshC = exterior.AddComponent(typeof(MeshCollider)) as MeshCollider;
        meshC.sharedMesh = mesh;
        exterior.layer = LayerMask.NameToLayer("TankShield");
        exterior.tag = "Shield";

        exterior.transform.parent = transform;
        exterior.SetActive(false);
    }

    void CreateInteriorPane() {
        Vector3[] vertices = new Vector3[3];
        Vector2[] uv = new Vector2[3];
        int[] triangles = new int[3];

        vertices[0] = a;
        vertices[1] = c;
        vertices[2] = b;

        uv[0] = a;
        uv[1] = c;
        uv[2] = b;

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        Mesh mesh = new Mesh();

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();

        interior = new GameObject($"Interior {id}", typeof(MeshFilter), typeof(MeshRenderer));
        interior.transform.localScale = new Vector3(scale, scale, scale);
        interior.GetComponent<MeshFilter>().mesh = mesh;
        interior.GetComponent<MeshRenderer>().material = transparentMaterial;
        MeshCollider meshC = interior.AddComponent(typeof(MeshCollider)) as MeshCollider;
        meshC.sharedMesh = mesh;
        interior.layer = LayerMask.NameToLayer("TankUI");

        interior.transform.parent = transform;
    }

    public void ToggleActivation(bool activate) {
        if (activate && !exterior.activeInHierarchy) {
            exterior.SetActive(activate);
            exterior.GetComponent<MeshRenderer>().material = glowMaterial;
            interior.GetComponent<MeshRenderer>().material = glowMaterial;
        } else if (!activate && exterior.activeInHierarchy) {
            exterior.SetActive(activate);
            exterior.GetComponent<MeshRenderer>().material = transparentMaterial;
            interior.GetComponent<MeshRenderer>().material = transparentMaterial;
        }
    }
}