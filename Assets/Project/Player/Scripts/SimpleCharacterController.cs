using UnityEngine;

public class SimpleCharacterController : MonoBehaviour
{
    [SerializeField]
    private CharacterController character;
    [SerializeField]
    private LayerMask groundLayers;
    [SerializeField]
    private float moveSpeed = 4f;
    [SerializeField]
    private float jumpSpeed = 10f;
    [SerializeField]
    private float movementAcceleration = 10f;
    [SerializeField]
    private Transform head;
    [SerializeField]
    private Transform root;
    [SerializeField]
    private float sensitivity = 3f;

    private float deltaX;
    private float deltaY;

    private float startX;
    private float startY;
    private Vector3 velocity;
    private Vector3 desiredDir;


    private void Start()
    {
        startX = head.localEulerAngles.x;
        startY = root.localEulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        MouseLook();
        if (character.isGrounded)
        {
            Grounded();
        }
        else
        {
            Airbourne();
        }
    }

    void MouseLook()
    {
        deltaX -= Input.GetAxis("Mouse Y") * sensitivity;
        deltaY += Input.GetAxis("Mouse X") * sensitivity;

        float x = Mathf.Clamp(startX + deltaX, -90f, 90f);
        head.localEulerAngles = new Vector3(x, head.localEulerAngles.y, head.localEulerAngles.z);

        root.localEulerAngles = new Vector3(root.localEulerAngles.z, startY + deltaY, root.localEulerAngles.z);
    }

    private void Airbourne()
    {
        velocity += Physics.gravity * Time.deltaTime;
        character.Move(velocity * Time.deltaTime);
        velocity = character.velocity;
    }

    private void Grounded()
    {
        float checkDistance = character.height / 2 + character.skinWidth * 2;
        Vector3 startPosition = character.transform.position;
        Vector3 endPosition = startPosition + Vector3.down * checkDistance;
        RaycastHit hit;
        bool foundGround = Physics.Linecast(startPosition, endPosition, out hit, groundLayers);
        Vector3 normal = foundGround ? hit.normal : Vector3.up;
        Vector3 inputsDir = GetMoveDir();
        Vector3 dir = transform.forward * inputsDir.z + transform.right * inputsDir.x;
        dir *= moveSpeed;
        desiredDir = Vector3.MoveTowards(desiredDir, dir, movementAcceleration * Time.deltaTime);

        dir = Vector3.ProjectOnPlane(desiredDir, normal);
        if (GetJump())
        {
            dir += Vector3.up * jumpSpeed;
        }
        dir += Vector3.down;
        character.Move(dir * Time.deltaTime);
        velocity = character.velocity;
    }

    private bool GetJump()
    {
        return Input.GetButtonDown("Jump");
    }

    private Vector3 GetMoveDir()
    {
        return new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
    }
}
