using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private GameObject _camera;
    // Start is called before the first frame update
    void Start()
    {
        _camera = this.gameObject;
        //print(_camera.name);
    }

    // Update is called once per frame
    void Update()
    {
        if (_camera.transform.position.y < 0)
        {
            _camera.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
        _camera.transform.rotation = new Quaternion(0, 0, 0, 0);
    }
}
