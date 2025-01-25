using System;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private PlayerBubble playerBubble;
    [SerializeField] private Transform cameraOverallTransform;
    
    [SerializeField] private Transform topLimitTransformRef;
    [SerializeField] private Transform bottomLimitTransformRef;
    [SerializeField] private Transform leftLimitTransformRef;
    [SerializeField] private Transform rightLimitTransformRef;
    
    [SerializeField] private Transform topWallTransformRef;
    [SerializeField] private Transform bottomWallTransformRef;
    [SerializeField] private Transform leftWallTransformRef;
    [SerializeField] private Transform rightWallTransformRef;
    
    [SerializeField] private Transform topWorldTransformRef;
    [SerializeField] private Transform bottomWorldTransformRef;
    [SerializeField] private Transform leftWorldTransformRef;
    [SerializeField] private Transform rightWorldTransformRef;

    public float speed;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerBubble.transform.position.x < leftLimitTransformRef.position.x)
        {
            // bouger vers la gauche
            if (CanGoLeft())
            {
                transform.Translate(-Vector3.right * Time.deltaTime * speed);
            }
        }
        else if (playerBubble.transform.position.x > rightLimitTransformRef.position.x)
        {
            // bouger vers la droite
            if (CanGoRight())
            {
                transform.Translate(Vector3.right * Time.deltaTime * speed);
            }
        }
        else if (playerBubble.transform.position.y > topLimitTransformRef.position.y)
        {
            // bouger vers le haut
            if (CanGoUp())
            {
                transform.Translate(Vector3.up * Time.deltaTime * speed);
            }
        }
        else if (playerBubble.transform.position.y < bottomLimitTransformRef.position.y)
        {
            // bouger vers le bas
            if (CanGoDown())
            {
                transform.Translate(-Vector3.up * Time.deltaTime * speed);
            }
        }
    }

    private bool CanGoLeft()
    {
        return (leftWallTransformRef.transform.position.x > leftWorldTransformRef.transform.position.x) ;
    }
    
    private bool CanGoRight()
    {
        return (rightWallTransformRef.transform.position.x < rightWorldTransformRef.transform.position.x);
    }
    
    private bool CanGoUp()
    {
        return (topWallTransformRef.transform.position.y < topWorldTransformRef.transform.position.y);
    }
    
    private bool CanGoDown()
    {
        return (bottomWallTransformRef.transform.position.y > bottomWorldTransformRef.transform.position.y);
    }
}
