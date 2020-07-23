using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour
{
    public Transform _player;
    Transform _tempTransform;
    float _dist = 10f;
    public float _rotSpeed = 100f;
    public float _rotSmooth = 15f;
    [SerializeField]
    Vector3 _rotation;

    // Start is called before the first frame update
    void Start()
    {
        _rotation = transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        SetPosition();
        Drag();
    }

    void SetPosition()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 8, transform.position.z - 5);
    }

    void Drag()
    {
        //if (Input.GetMouseButton(1))
        //{
        //    _rotation.x -= Input.GetAxis("Mouse Y") * _rotSpeed;
        //    _rotation.y += Input.GetAxis("Mouse X") * _rotSpeed;
        //}
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(_rotation), Time.deltaTime * _rotSmooth);
        //transform.LookAt(_player.transform);
    }
}
