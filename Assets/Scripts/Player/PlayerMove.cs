using UnityEngine;
using static UnityEngine.EventSystems.StandaloneInputModule;

public class PlayerMove : MonoBehaviour
{
    private const string Speed = "Speed";
    public float speed = 5f;
    public float lerpSpeed = .2f;
    public float maxLength = 1f;
    public float rotationSpeed = 1f;

    public float jumpForce = 5f;
    public float gravity = 9.8f;
    public float verticalVelocity;

    private Vector3 moveVector;

    public Animator anim;
    public Transform ThirdPersonCamera;
    private CharacterController controller;

    float horizontalInput;
    float verticalInput;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {

         horizontalInput = Input.GetAxis("Horizontal");
         verticalInput = Input.GetAxis("Vertical");

        Rotate();


        moveVector = new Vector3(horizontalInput, 0, verticalInput);
        InputMove();
        Gravity();
        controller.Move(moveVector * Time.deltaTime * speed);


    }

    void InputMove() => anim.SetFloat(Speed, Vector3.ClampMagnitude(moveVector, 1).magnitude, maxLength, Time.deltaTime * 10);

    void Rotate()
    {
        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);

        if (movementDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            // Karakteri dönüþtür
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

    }

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
