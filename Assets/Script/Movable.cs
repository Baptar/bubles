using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour
{
    [SerializeField] protected float _moveStrength = 20.0f;
    
    protected Rigidbody2D _rb;
    private bool _mouseOn;
    
    private List<GameObject> _hittingObjects = new List<GameObject>();
    
    
    //feedback
    [SerializeField, Range(0, 1f)]
    private float _animationDuration = 0.25f;

    [SerializeField] private AnimationCurve _feedbackAnimCurve = new AnimationCurve(
        new Keyframe(0f, 0f),
        new Keyframe(0.25f, 1f),
        new Keyframe(1f, 0f)
    );

    [SerializeField] private float _maxSize;
    
    private Vector2 _startSize;
    private bool _isReverse;
    private bool _isFeedbacking;

    
    private SpriteRenderer _renderer;
    
    [SerializeField] private AudioClip _hitClip;

    private AudioSource _audioSource;
    
    public virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
    }

    public virtual void Start()
    {
        _startSize = _renderer.size;
    }

    public virtual void CursorHit(Vector2 force)
    {
        _rb.AddForce(force * _moveStrength);
        Feedback(force);
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
                if (dfr > 0)
                {
                    CursorHit(new Vector2(mouseVelocity.x, mouseVelocity.y) / dfr);
                }

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
    
    public void Feedback(Vector2 hitDir)
    {
        if (!_isFeedbacking)
        {
            _audioSource.PlayOneShot(_hitClip, 0.5f);
            _isReverse = false;
            //Vector2Int axisAffect = new Vector2Int(Mathf.RoundToInt(hitDir.normalized.x),Mathf.RoundToInt(hitDir.normalized.y));

            Vector2Int axisAffect = new Vector2Int(1, 0);
            if (hitDir.normalized.x < hitDir.normalized.y)
                axisAffect = new Vector2Int(0, 1);
            StartCoroutine(EffectCoroutine(axisAffect));
        }
    }

    private IEnumerator EffectCoroutine(Vector2Int affectAxis)
    {
        _isReverse = !_isReverse;
                
        float elapsedTime = 0;
        Vector3 modifiedSize = _startSize;

        while (elapsedTime < _animationDuration)
        {
            elapsedTime += Time.deltaTime;
                    
            float curvePosition;
                    
            if (_isReverse)
                curvePosition = 1 - (elapsedTime / _animationDuration);
            else
                curvePosition = elapsedTime / _animationDuration;
                    
            float curveValue = _feedbackAnimCurve.Evaluate(curvePosition);
            float remappedValue = 1f + (curveValue * (_maxSize - 1f));

            float minimumThreshold = 0.0001f;
            if (Mathf.Abs(remappedValue) < minimumThreshold)
                remappedValue = minimumThreshold;
            
            
            if (affectAxis.x == 1)
                modifiedSize.x = _startSize.x * remappedValue;
            else
                modifiedSize.x = _startSize.x / remappedValue;

            if (affectAxis.y == 1)
                modifiedSize.y = _startSize.y * remappedValue;
            else
                modifiedSize.y = _startSize.y / remappedValue;

            _renderer.size = modifiedSize;

            yield return null;
        }
    }
    
}
