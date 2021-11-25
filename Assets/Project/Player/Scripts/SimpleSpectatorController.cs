using UnityEngine;

public class SimpleSpectatorController : MonoBehaviour
{
    [SerializeField]
    private CharacterController character;
    [SerializeField]
    private float moveSpeed = 4f;
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
        Move();
    }

    void MouseLook()
    {
        deltaX -= Input.GetAxis("Mouse Y") * sensitivity;
        deltaY += Input.GetAxis("Mouse X") * sensitivity;

        float x = Mathf.Clamp(startX + deltaX, -90f, 90f);
        head.localEulerAngles = new Vector3(x, head.localEulerAngles.y, head.localEulerAngles.z);

        root.localEulerAngles = new Vector3(root.localEulerAngles.z, startY + deltaY, root.localEulerAngles.z);
    }

    private void Move()
    {
        Vector3 inputsDir = GetMoveDir();
        Vector3 moveDir = head.forward * inputsDir.z + head.right * inputsDir.x + (GetJump()?root.transform.up:Vector3.zero);
        moveDir *= moveSpeed;
        desiredDir = Vector3.MoveTowards(desiredDir, moveDir, movementAcceleration * Time.deltaTime);
        character.Move(moveDir * Time.deltaTime);
    }

    private bool GetJump()
    {
        return Input.GetButton("Jump");
    }

    private Vector3 GetMoveDir()
    {
        return new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
    }
}
