
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    private FreeCamera cam;
    private Transform trCam;  
    public Rigidbody rb_Person;
    private PersonAim anim;

    public float currentSpeed = 10f;  
    public float jumpForce = 5f;  
       
    private bool isTerra = false;  
    private bool isRunning = false; 

    private Vector3 directionInput;
    private Vector3 newDirectionMove;
    private Vector3 cameraAxisX;
    private Vector3 cameraAxisZ;
    private void Awake()
    {
        anim = GetComponent<PersonAim>();
        cam = FindObjectOfType<FreeCamera>();
        trCam = cam.transform.GetComponent<Transform>();
    }

    private void Update()
    {
        Vector3 input = InputAxis();
        CalculateDirectionMove(input);
        anim.PlayPersonAnim(input, isRunning);
        Jumping(); 
    }
    private void LateUpdate()
    {
        Rotate();
        bool isLimit = cam.CheckCameraRotateAngle();
        anim.TurnAnimation(cam.input, isLimit);
    }
    private void FixedUpdate()
    {
        MovePerson(newDirectionMove);
    }

    private Vector3 InputAxis()
    {
        float X = Input.GetAxis("Horizontal");
        float Z = Input.GetAxis("Vertical");  
        return new Vector3(X, 0, Z);
    }
    private void Rotate()
    {
        Quaternion rotation = Quaternion.LookRotation(cameraAxisZ, Vector3.up);
        Quaternion newRotation = Quaternion.Lerp(rb_Person.rotation, rotation, Time.fixedDeltaTime * 45f);
        rb_Person.MoveRotation(newRotation);
    }
    private void CalculateDirectionMove(Vector3 input)
    {    
        cameraAxisZ = Vector3.ProjectOnPlane(trCam.forward, Vector3.up);  
        cameraAxisX = Vector3.ProjectOnPlane(trCam.right, Vector3.up);  
        newDirectionMove = (input.z * cameraAxisZ) + (input.x * cameraAxisX); 
    }
    private void MovePerson(Vector3 directionMove) 
    {
        isRunning = Input.GetKey(KeyCode.LeftShift);  

        
        float moveSpeed = currentSpeed * (isRunning ? 1 : 0.4f);  
        rb_Person.MovePosition(rb_Person.position + directionMove * moveSpeed * Time.deltaTime);  
        
    }
    
    private void Jumping()
    {
        bool isJump = Input.GetKeyDown(KeyCode.Space);  
        if (isTerra & isJump) 
        {
            rb_Person.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            anim.Jumping(true);
            isTerra = false;  
        } else anim.Jumping(false);
    }
    private void OnCollisionEnter(Collision collision)  
    {
        if(collision.gameObject.tag == "Terra")  
            isTerra = true;  

    }

}
