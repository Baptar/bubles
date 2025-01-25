using System;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float _bounciness = 1f;
    [SerializeField] private float _damageAmount = 1f;
    
    public float DamageAmount
    {
        get { return _damageAmount; }
    }
    
    private PhysicsMaterial2D _physicsMat;
    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        _physicsMat = new PhysicsMaterial2D();
        _physicsMat.friction = 0f;
        _physicsMat.bounciness = _bounciness;
        _collider.sharedMaterial = _physicsMat;
    }
    
    
}
