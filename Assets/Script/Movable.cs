using System;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour
{
    [SerializeField] protected float _moveStrength = 20.0f;
    
    protected Rigidbody2D _rb;
    private bool _mouseOn;
    
    private List<GameObject> _hittingObjects = new List<GameObject>();

    public virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void CursorHit(Vector2 force)
    {
        _rb.AddForce(force * _moveStrength);
    }

    private void OnMouseEnter()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (!_mouseOn)
            {
                Vector2 mouseVelocity = Input.mousePositionDelta;
                //Vector3 hitPos = Input.mousePosition;
                //Vector3 dir = transform.position - hitPos;
                //Vector3 force = Vector3.Project(mouseVelocity, dir);
                float rate = 1 / Time.deltaTime;
                float dfr = 60.0f / rate;
                CursorHit(new Vector2(mouseVelocity.x, mouseVelocity.y) / dfr);
                //CursorHit(new Vector2(force.x, force.y));
                _mouseOn = true;
            }
        }
    }

    private void OnMouseExit()
    {
        _mouseOn = false;
    }

    public void BlockMovement()
    {
        _rb.bodyType = RigidbodyType2D.Kinematic;
        _rb.linearVelocity = Vector2.zero;
        _rb.angularVelocity = 0.0f;
    }

    public void UnblockMovement()
    {
        _rb.bodyType = RigidbodyType2D.Dynamic;
    }

}
