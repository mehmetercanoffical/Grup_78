using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
using static UnityEngine.EventSystems.StandaloneInputModule;

public class PlayerMove : MonoBehaviour
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
    private  string verticalInput = "Vertical";
    private Vector3 moveVector;

    void Start() => controller = GetComponent<CharacterController>();

    void Update()
    {

        float horizontal= Input.GetAxis(horizontalInput);
        float vertical = Input.GetAxis(verticalInput);
        moveVector = new Vector3(horizontal, 0, vertical);


        Rotate();
        MovecClamp();
        Gravity();

        controller.Move(moveVector * Time.deltaTime * speed);


    }

    void Rotate()
    {
        if (moveVector != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveVector, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }


    void MovecClamp() => anim.SetFloat(Speed, Vector3.ClampMagnitude(moveVector, 1).magnitude, maxLength, Time.deltaTime * 10);

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
