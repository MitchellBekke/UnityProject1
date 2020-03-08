﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardScript : MonoBehaviour
{
    public Transform cam;

    private void Start()
    {
        cam = GameObject.Find("FirstPersonCharacter").GetComponent<Transform>(); ;
    }
    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
