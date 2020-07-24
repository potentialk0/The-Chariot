using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jMonster : MonoBehaviour
{
    LayerMask _player;
    Vector3 _rightView;
    Vector3 _leftView;
    public float _agroDist = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        _rightView = (Vector3.forward + Vector3.right).normalized * _agroDist;
        _leftView = -_rightView;
    }

    // Update is called once per frame
    void Update()
    {
        MonsterAI();
    }

    void MonsterAI()
    {
        
    }
}