using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Locomotion Parameters")]
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float RotationDamping = 10f;
    [SerializeField] float fallingSpeed = 45f;
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float walkingSpeed = 1f;

    private float gravityValue = -9.81f;
    private Vector3 playerVelocity;
    private float verticalVelocity;
    private const float DampTime = 0.1f;
    private readonly int locomotionSpeedHash = Animator.StringToHash("Speed");

    public Transform MainCameraTransform { get; private set; }

    CharacterController controller;
    InputReader inputReader;
    Animator animator;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        inputReader = GetComponent<InputReader>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        MainCameraTransform = Camera.main.transform;
    }

    void Update()
    {

        Grounded();
        Move();
       // Jump();
    }

    private void Grounded()
    {
        if (verticalVelocity < 0f && controller.isGrounded)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }
    }

    //private void Jump()
    //{
    //    if (inputReader.Jump && controller.isGrounded)
    //    {
    //        playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
    //        inputReader.Jump = false;
    //    }

    //    playerVelocity.y += gravityValue * Time.deltaTime;
    //    Debug.Log(playerVelocity);
    //    controller.Move(playerVelocity * Time.deltaTime);
    //}

    private void Move()
    {
        Vector3 movement = CalculateMovement();
        float speed = runSpeed;

        if (inputReader.MovementValue == Vector2.zero)
        {
            animator.SetFloat(locomotionSpeedHash, 0f);
        }
        else
        {

            if (inputReader.MovementValue != Vector2.zero && !inputReader.IsRunning)
            {
                speed = walkingSpeed;
                animator.SetFloat(locomotionSpeedHash, 0.5f);

            }
            else if (inputReader.MovementValue != Vector2.zero && inputReader.IsRunning)
            {
                speed = runSpeed;
                animator.SetFloat(locomotionSpeedHash, 1f);
            }

            Vector3 moveWithGravity = movement + (Vector3.up * verticalVelocity);

            controller.Move(moveWithGravity * speed * Time.deltaTime);
        }

        FaceMovementDirection(movement);

    }

    private Vector3 CalculateMovement()
    {
        Vector3 forward = MainCameraTransform.forward;
        Vector3 right = MainCameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        return forward * inputReader.MovementValue.y + right * inputReader.MovementValue.x;
    }

    private void FaceMovementDirection(Vector3 movement)
    {
        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(
            transform.rotation,
            Quaternion.LookRotation(movement),
            Time.deltaTime * RotationDamping);
        }
    }

    //public object CaptureState()
    //{
    //    return new SerializableVector3(transform.position);
    //}

    //public void RestoreState(object state)
    //{
    //    SerializableVector3 position = (SerializableVector3)state;
    //    transform.position = position.ToVector();
    //}
}