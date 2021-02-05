using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private void Update()
    {
        var pos = player.transform.position;
        gameObject.transform.position = new Vector3(pos.x, pos.y + 20, pos.z - 20);
    }
}
