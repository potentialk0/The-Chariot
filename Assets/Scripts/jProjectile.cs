using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jProjectile : MonoBehaviour
{
    public float moveSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        transform.position += transform.forward.normalized * moveSpeed * Time.deltaTime;
    }
}
