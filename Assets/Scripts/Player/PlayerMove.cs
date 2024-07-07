using UnityEngine;

public class PlayerMove : Singleton<PlayerMove>
{
    public float speed = 5f;
    public float lerpSpeed = .2f;
    public float maxLength = 1f;
    public float rotationSpeed = 1f;

    [Header("Gravity")]
    public float jumpForce = 5f;
    private float gravity = 9.8f;
    private float verticalVelocity;

    public Animator anim;
    public Transform ThirdPersonCamera;
    private CharacterController controller;

    private const string Speed = "Speed";
    private string horizontalInput = "Horizontal";
    private string verticalInput = "Vertical";
    private int jump = Animator.StringToHash("Jump");
    private Vector3 moveVector;
    Vector3 rotOffset;

    public bool isPlayerMoving = false;




    void Start() => controller = GetComponent<CharacterController>();

    void Update()
    {

        float horizontal = Input.GetAxis(horizontalInput);
        float vertical = Input.GetAxis(verticalInput);
        moveVector = new Vector3(horizontal, 0, vertical);

        Jump();
        Rotate();
        Move();
        MovecClamp();
        Gravity();

    }

    void Jump()
    {
        if (controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
            anim.SetTrigger(jump);
    }
    void Rotate()
    {
        if (moveVector != Vector3.zero)
        {

            rotOffset = ThirdPersonCamera.TransformDirection(moveVector);
            rotOffset.y = 0;
            
            rotOffset.Normalize();

            Quaternion rotTarget = Quaternion.LookRotation(rotOffset);
            rotTarget.z = 0;    
            rotTarget.Normalize();
            transform.rotation = Quaternion.Lerp(transform.rotation, rotTarget, Time.deltaTime * rotationSpeed);
        }
    }
    void Move()
    {
        if (moveVector != Vector3.zero)
        {
            Vector3 move = Vector3.Slerp(transform.forward, rotOffset, Time.deltaTime * speed);
            controller.Move((move.normalized / 2) * Time.deltaTime);
            isPlayerMoving = true;
        }
        else
            isPlayerMoving = false;
    }


    void MovecClamp() => anim.SetFloat(Speed, 
                Vector3.ClampMagnitude(moveVector, 1).magnitude, maxLength, Time.deltaTime * 10);

    private void Gravity()
    {

        if (controller.isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Space))
                verticalVelocity = jumpForce;
        }
        else
            verticalVelocity -= gravity * Time.deltaTime;

        moveVector.y = verticalVelocity;
    }

}
