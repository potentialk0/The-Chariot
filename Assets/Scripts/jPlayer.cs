using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;

public class jPlayer : MonoBehaviour
{
    public enum STATE
    {
        CREATE, WALK, ATTACK, DAMAGE, DEAD,
    }

    [SerializeField]
    private STATE state;
    Animator anim;
    CharacterController controller;
    public Transform cameraTransform;
    
    private float moveSpeed = 4f;
    private float currentSpeed = 0f;
    private float speedSmoothVelocity = 0f;
    private float speedSmoothTime = 0.1f;
    private float rotSpeed = 0.1f;
    private float gravity = 8f;

    private Transform mainCameraTransform;

    public Vector3 projectilePos;
    public Vector3 projectileDir;

    


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        mainCameraTransform = Camera.main.transform;
        state = STATE.CREATE;
    }

    // Update is called once per frame
    void Update()
    {
        StateProcess();
        FollowCamera();
        Test();
    }

    void Test()
    {
        Debug.Log(anim.GetCurrentAnimatorStateInfo(0).IsName("FireProjectile"));
        Debug.Log("condition : " + anim.GetInteger("condition"));
        //Debug.Log("horizontal : " + Input.GetAxisRaw("Horizontal"));
        //Debug.Log("vertical : " + Input.GetAxisRaw("Vertical"));
    }

    void ChangeState(STATE e)
    {
        if (state == e) return;
        state = e;
        switch(state)
        {
            case STATE.CREATE:
                break;
            case STATE.WALK:
                break;
            case STATE.ATTACK:
                break;
            case STATE.DAMAGE:
                break;
            case STATE.DEAD:
                break;
        }
    }

    void StateProcess()
    {
        switch (state)
        {
            case STATE.CREATE:
                ChangeState(STATE.WALK);
                break;
            case STATE.WALK:
                FireProjectile();
                Movement();
                MoveAnimation();
                break;
            case STATE.ATTACK:
                FireProjectile();
                Movement();
                break;
            case STATE.DAMAGE:
                break;
            case STATE.DEAD:
                break;
        }
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

    private void MoveAnimation()
    {
        float movementX = Input.GetAxisRaw("Horizontal");
        float movementY = Input.GetAxisRaw("Vertical");
        FireProjectile();
        if (movementY > 0)
        {
            anim.SetFloat("X", 1);
        }
        if (movementY < 0)
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

    private void FollowCamera()
    {
        Vector3 camground = new Vector3(cameraTransform.position.x, transform.position.y, cameraTransform.position.z);
        Vector3 dir = (transform.position - camground).normalized;
        transform.forward = dir;
    }

    private void FireProjectile()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ChangeState(STATE.ATTACK);
            anim.SetInteger("condition", 5);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                // projectileDir = new Vector3(hit.point.x, transform.position.y + 1.4f, hit.point.z).normalized;
                projectilePos = new Vector3(transform.position.x, transform.position.y + 1.4f, transform.position.z);
            }
            Instantiate(Resources.Load("Prefabs/lightningBall"), new Vector3(transform.position.x, transform.position.y + 1.4f, transform.position.z), transform.rotation);
            
        }
        //if (!anim.GetCurrentAnimatorStateInfo(0).IsName("FireProjectile"))
        //{
        //    anim.SetInteger("condition", 0);
        //    ChangeState(STATE.WALK);
        //}
    }

    private void ChangeConditionToZero()
    {
        anim.SetInteger("condition", 0);
        ChangeState(STATE.WALK);
    }
}
