using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Camera _cam;

    [SerializeField] private float _movementSpeed = 1f;
    [SerializeField] private Vector2 _movementRangeX;
    [SerializeField] private Vector2 _movementRangeY;
    [SerializeField] private float _zoomSense = 1f;


    private PlayerBubble _bubble;

    public PlayerBubble Bubble
    {
        set { _bubble = value; }
    }
    
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        //move
        if (Input.GetKey(KeyCode.Mouse1))
        {
            Vector2 mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            if (mouseMovement.x != 0 || mouseMovement.y != 0)
            {
                if (CheckBounds(mouseMovement))
                {
                    Vector2 movement = mouseMovement * (Time.deltaTime * _movementSpeed * 10.0f);
                    transform.Translate(-movement.x, -movement.y, 0f);
                }
            }
        }
        
        //zoom
        float mouseScroll = Input.mouseScrollDelta.y;
        if (mouseScroll != 0)
        {
            _cam.orthographicSize -= mouseScroll * (Time.deltaTime * _zoomSense);
        }
        
        //recenter
        if (Input.GetKey(KeyCode.Mouse2))
        {
            transform.position = new Vector3(_bubble.transform.position.x, _bubble.transform.position.y,
                transform.position.z);
        }
    }

    private bool CheckBounds(Vector2 mouseMovement)
    {
        if (mouseMovement.x < 0 && transform.position.x <= _movementRangeX.x)
        {
            //ClampPos(mouseMovement);
            return false;
        }

        if (mouseMovement.x > 0 && transform.position.x >= _movementRangeX.y)
        {
            //ClampPos(mouseMovement);
            return false;
        }

        if (mouseMovement.y < 0 && transform.position.y <= _movementRangeY.x)
        {
            //ClampPos(mouseMovement);
            return false;
        }

        if (mouseMovement.y > 0 && transform.position.y >= _movementRangeY.y)
        {
            //ClampPos(mouseMovement);
            return false;
        }

        return true;
    }

    private void ClampPos(Vector2 mouseMovement)
    {
        float xClamped = transform.position.x;
        float yClamped = transform.position.y;

        if (mouseMovement.x < 0 && transform.position.x <= _movementRangeX.x)
            xClamped = _movementRangeX.x;

        if (mouseMovement.x > 0 && transform.position.x >= _movementRangeX.y)
            xClamped = _movementRangeX.y;

        if (mouseMovement.y < 0 && transform.position.y <= _movementRangeY.x)
            yClamped = _movementRangeY.x;

        if (mouseMovement.y > 0 && transform.position.y >= _movementRangeY.y)
            yClamped = _movementRangeY.y;

        transform.position = new Vector3(xClamped, yClamped, 0);
    }
}
