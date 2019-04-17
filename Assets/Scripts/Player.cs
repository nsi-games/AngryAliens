﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpringJoint2D))]
public class Player : MonoBehaviour
{
    // <access-specifier> <return-type> <function-name> (<optional-arguments>)
    public float maxDragDistance = 2f;

    private SpringJoint2D spring;
    private Rigidbody2D rigid;
    private bool isReleased = false;

    void Awake()
    { 
        spring = GetComponent<SpringJoint2D>();
        rigid = GetComponent<Rigidbody2D>();
    }
    private void OnDrawGizmos()
    {
        spring = GetComponent<SpringJoint2D>();
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spring.connectedBody.position, .1f);
    }

    void Update()
    {
        Vector2 playerPos = rigid.position;
        Vector2 hookPos = spring.connectedBody.position;

        // If the rigidbody is disabled
        if (rigid.isKinematic)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePos - hookPos).normalized;
            // Get distance between player and hook
            float distance = Vector2.Distance(mousePos, hookPos);
            // If distance is greater than max drag distance
            if (distance > maxDragDistance)
            {
                // cap the position
                rigid.position = hookPos + direction * maxDragDistance;
            }
            else
            {
                // set position to mouse position
                rigid.position = mousePos;
            }
        }

        if (isReleased)
        {
            if(playerPos.x > hookPos.x)
            {
                spring.enabled = false;
            }
        }
    }
    void OnMouseDown()
    {
        // Disable Rigidbody
        rigid.isKinematic = true;
    }
    void OnMouseUp()
    {
        // Enable Rigidbody
        rigid.isKinematic = false;
        // Let go
        isReleased = true;
    }
}
