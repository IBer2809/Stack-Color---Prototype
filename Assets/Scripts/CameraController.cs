using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform player;
    private float delta = .5f;

    private void Start()
    {
        delta = transform.position.z - player.position.z;

    }
    private void LateUpdate()
    {
        
        transform.position = new Vector3(transform.position.x,transform.position.y,player.position.z + delta);
    }
}
