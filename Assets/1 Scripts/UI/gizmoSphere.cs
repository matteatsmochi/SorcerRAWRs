﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gizmoSphere : MonoBehaviour
{
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 1);
    }
}
