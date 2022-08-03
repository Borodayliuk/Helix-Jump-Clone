using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    private float _offset;
    // Start is called before the first frame update
    private void Awake()
    {
        _offset =  transform.position.y - _target.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.y = _target.position.y + _offset;
        transform.position = pos;
    }
}
