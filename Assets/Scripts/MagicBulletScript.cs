using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBulletScript : MonoBehaviour
{
    public float speed = 10f;

    void Start() { }

    void Update()
    {
        // transform.Translate(transform.forward * speed * Time.deltaTime);
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}