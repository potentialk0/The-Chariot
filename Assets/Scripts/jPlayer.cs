using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;
using Unity;

public class jPlayer : MonoBehaviour
{
    Animator anim;
    CharacterController controller;

    [SerializeField]
    private float moveSpeed = 4f;
    private float currentSpeed = 0f;
    private float speedSmoothVelocity = 0f;
    private float speedSmoothTime = 0.1f;
    private float rotSpeed = 0.1f;
    private float gravity = 8f;

    private Transform mainCameraTransform;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        mainCameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Animation();
        Test();
    }

    void Test()
    {
        Debug.Log(Input.GetAxisRaw("Horizontal"));
    }

    private void Movement()
    {
        Vector2 movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        Vector3 forward = mainCameraTransform.forward;
        Vector3 right = mainCameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 desiredMoveDir = (right * movementInput.x + forward * movementInput.y).normalized;
        Vector3 gravityVec = Vector3.zero;

        if (!controller.isGrounded)
        {
            gravityVec.y -= gravity;
        }

        float targetSpeed = moveSpeed * movementInput.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

        controller.Move(desiredMoveDir * currentSpeed * Time.deltaTime);
        controller.Move(gravityVec * Time.deltaTime);
    }

    private void Animation()
    {
        float movementX = Input.GetAxisRaw("Horizontal");
        float movementY = Input.GetAxisRaw("Vertical");
        if(movementY > 0)
        {
            anim.SetInteger("condition", 1);
        }
        if(movementY < 0)
        {
            anim.SetInteger("condition", 2);
        }
        if (movementX < 0)
        {
            anim.SetInteger("condition", 3);
        }
        if (movementX > 0)
        {
            anim.SetInteger("condition", 4);
        }
        if (Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0)
            anim.SetInteger("condition", 0);
    }
}
