using System;
using UnityEngine;

public class Movable : MonoBehaviour
{
    private Rigidbody2D _rb;
    private bool _mouseOn;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void CursorHit(Vector2 force)
    {
        _rb.AddForce(force * 20.0f);
    }

    private void OnMouseEnter()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (!_mouseOn)
            {
                Vector3 mouseVelocity = Input.mousePositionDelta;
                //Vector3 hitPos = Input.mousePosition;
                //Vector3 dir = transform.position - hitPos;
                //Vector3 force = Vector3.Project(mouseVelocity, dir);
                CursorHit(new Vector2(mouseVelocity.x, mouseVelocity.y));
                //CursorHit(new Vector2(force.x, force.y));
                _mouseOn = true;
            }
        }
    }

    private void OnMouseExit()
    {
        _mouseOn = false;
    }
}
