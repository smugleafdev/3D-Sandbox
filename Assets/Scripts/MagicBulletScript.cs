using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBulletScript : MonoBehaviour
{
    public float speed = 100f;

    void Start()
    {

    }

    void Update()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime);
    }
}