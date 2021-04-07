using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pane : MonoBehaviour {
    [SerializeField]
    bool pulse = true;
    [SerializeField]
    Material transparentMaterial, glowMaterial;
    GameObject exterior, interior;
    float scale = 5f;
    Vector3 a, b, c;
    int id = 0;
    bool flashing, increasing, waitAtPeak;
    float startWaitTime;
    float waitTime = 0.1f;

    public void InitPane(Vector3 a, Vector3 b, Vector3 c, int id) {
        this.a = a;
        this.b = b;
        this.c = c;
        this.id = id;

        CreateExteriorPane();
        CreateInteriorPane();
    }

    void Update() {
        if (pulse) {
            PulseRandom();
        }
    }

    void PulseRandom() {
        if (exterior.activeInHierarchy) {
            flashing = false;
            increasing = false;
        }

        if (!exterior.activeInHierarchy && !flashing && Random.Range(1, 10000) == 1) {
            flashing = true;
            increasing = true;
        }

        if (Time.time - startWaitTime > waitTime) {
            waitAtPeak = false;
        }

        if (flashing && increasing) {
            Color color = interior.GetComponent<MeshRenderer>().material.color;
            color.a += 0.005f;
            interior.GetComponent<MeshRenderer>().material.color = color;

            if (color.a > 0.5f) {
                increasing = false;
                waitAtPeak = true;
                startWaitTime = Time.time;
            }
        } else if (flashing && !waitAtPeak) {
            Color color = interior.GetComponent<MeshRenderer>().material.color;
            color.a -= 0.01f;
            if (color.a < 0.1f) {
                flashing = false;
                color.a = 0.1f;
            }
            interior.GetComponent<MeshRenderer>().material.color = color;
        }
    }

    void CreateExteriorPane() {
        Vector3[] vertices = new Vector3[3];
        Vector2[] uv = new Vector2[] { new Vector2(0.025f, 0.14f), new Vector2(0.5f, 0.96f), new Vector2(0.975f, 0.14f) };
        int[] triangles = new int[3];

        vertices[0] = a;
        vertices[1] = b;
        vertices[2] = c;

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
        exterior.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
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

        uv = new Vector2[] { new Vector2(0.025f, 0.14f), new Vector2(0.5f, 0.96f), new Vector2(0.975f, 0.14f) };

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
        interior.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
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