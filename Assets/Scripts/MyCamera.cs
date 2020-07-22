using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour
{
    public GameObject _player;
    Transform _tempTransform;
    float _dist = 10f;
    float _rotSpeed = 100f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetPosition()
    {
        transform.position = -_player.transform.forward.normalized * _dist;
    }

    void Drag()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        x -= Input.GetAxis("Mouse Y") * _rotSpeed;
        y += Input.GetAxis("Mouse X") * _rotSpeed;

    }
}
