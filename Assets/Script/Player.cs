using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [SerializeField, Range(0.01f, 1f)] float moveDuration = 0.2f;
    [SerializeField, Range(0.01f, 1f)] float jumpHeight = 0.5f;

    private void Update()
    {
        var moveDir = Vector3.zero;
        if(Input.GetKey(KeyCode.W))
        {
            moveDir += new Vector3(1,0,0);
        }
        
        if(Input.GetKey(KeyCode.S))
        {
            moveDir += new Vector3(-1,0,0);
        }

        if(Input.GetKey(KeyCode.D))
        {
            moveDir += new Vector3(0,0,-1);
        }

        if(Input.GetKey(KeyCode.A))
        {
            moveDir += new Vector3(0,0,1);
        }

        if (moveDir != Vector3.zero && IsJumping() == false)
            Jump(moveDir);
        
    }

    private void Jump(Vector3 targetDirection)
    {
        var TargetPosition = transform.position + targetDirection;
        transform.LookAt(TargetPosition);
        var moveSeq = DOTween.Sequence(transform);
        moveSeq.Append(transform.DOMoveY(jumpHeight, moveDuration / 2));
        moveSeq.Append(transform.DOMoveY(0, moveDuration / 2));

        transform.DOMoveX(TargetPosition.x, moveDuration);
        transform.DOMoveZ(TargetPosition.z, moveDuration);
    }

    private bool IsJumping()
    {
        return DOTween.IsTweening(transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        var car = other.GetComponent<Car>();
        if(car != null)
        {
            AnimateDie(car);
        }

        if(other.tag == "car")
        {
            // AnimateDie();
        }
    }

    private void AnimateDie(Car car)
    {
        // var isRight = car.transform.rotation.y == 90;

        // transform.DOMoveX(isRight ? 8 : -8, 0.2f);
        // transform.DORotate(Vector3.forward*360,0.2f).SetLoops(100, LoopType.Restart);

        transform.DOScaleY(0.1f, 0.2f);
        transform.DOScaleX(3, 0.2f);
        transform.DOScaleZ(2, 0.2f);
        this.enabled = false;   
    }

    private void OnTriggerStay(Collider other)
    {

    }

    private void OnTriggerExit(Collider other)
    {

    }
}
