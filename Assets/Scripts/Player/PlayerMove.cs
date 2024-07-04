using UnityEngine;
using static UnityEngine.EventSystems.StandaloneInputModule;

public class PlayerMove : MonoBehaviour
{
    private const string Speed = "Speed";
    public float speed = 5f;
    public float lerpSpeed = .2f;
    public float maxLength = 1f;

    public float jumpForce = 5f;
    public float gravity = 9.8f;
    public float verticalVelocity;

    private Vector3 moveVector;

    public Animator anim;
    public Transform ThirdPersonCamera;
    private CharacterController controller;



    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        moveVector = new Vector3(horizontalInput, 0, verticalInput);

        InputMove();
        Gravity();
        Rotate();
        controller.Move(moveVector * Time.deltaTime * speed);


    }

    void InputMove()
    {
        anim.SetFloat(Speed, Vector3.ClampMagnitude(moveVector, 1).magnitude, maxLength, Time.deltaTime * 10);
    }

    void Rotate()
    {
        Vector3 rotOffset = ThirdPersonCamera.transform.TransformDirection(moveVector);
        rotOffset.y = 0;
        transform.forward = Vector3.Lerp(transform.forward, rotOffset, lerpSpeed);

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
