using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonigoteController : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator _animator;
    private const string MOVE_HANDS = "Move Hands";
    private const string MOVE_X = "Move X";
    private const string MOVE_Y = "Move Y";
    private const string IS_MOVING = "IsMoving";

    private bool isMovingHands = false;
    private bool isMoving = false;
    private float moveX = 0.0f;
    private float moveY = 0.0f;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool(MOVE_HANDS, isMovingHands);
        _animator.SetBool(IS_MOVING, isMoving);
        _animator.SetFloat(MOVE_X, moveX);
        _animator.SetFloat(MOVE_Y, moveY);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
		{
            isMovingHands = !isMovingHands;
            
            _animator.SetBool(MOVE_HANDS, isMovingHands);
        }

        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        if (Mathf.Sqrt(moveX * moveX + moveY * moveY) > 0.01)
        {
            isMoving = true;
            _animator.SetBool(IS_MOVING, isMoving);
            _animator.SetFloat(MOVE_X, moveX);
            _animator.SetFloat(MOVE_Y, moveY);
        }
        else
        {
            isMoving = false;
            _animator.SetBool(IS_MOVING, isMoving);
        }
    }
}
