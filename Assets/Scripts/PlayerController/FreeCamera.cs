
using UnityEngine; 

public class FreeCamera : MonoBehaviour
{
    private CharacterMove player;
    [SerializeField]  private Transform target;
    private Transform trCam;
    public Vector3 offset;
    [SerializeField] private float transitionSpeed = 9f;

    public float sensitivityMouse = 5f;
    public float maxAngle = 70f;
    public float minAngle = -70f;
     
    public float currentZoomMouse = 3f;
    public float minZoom =2f;
    public float maxZoom = 25f;
    public float speedZoom = 5f;

    private float mouseX;
    private float mouseY;
    public Vector3 input { get; private set; }
    private void Awake()
    {
        trCam = GetComponent<Transform>();
        player = FindObjectOfType<CharacterMove>(); 
    }
    private void Start()
    {
        offset = trCam.position - target.position;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void LateUpdate()
    {
        input = GetDirectionRotateCamera();
        RotateCamera(input);
        FollowCamera();
        ZoomCamera();
    }

    private void RotateCamera(Vector3 input)
    {
        Quaternion newRot = Quaternion.Euler(input.y, input.x, 0);
        trCam.rotation = Quaternion.Slerp(trCam.rotation, newRot, Time.smoothDeltaTime * transitionSpeed);
    }

    public void FollowCamera()
    {
        Vector3 newPosition = trCam.localRotation * offset + target.position;
        trCam.position = Vector3.Lerp(trCam.position, newPosition, Time.deltaTime * transitionSpeed);
    }
    private Vector3 GetDirectionRotateCamera()
    {
        mouseX += Input.GetAxis("Mouse X") * sensitivityMouse;// -1, 0,  1
        mouseY -= Input.GetAxis("Mouse Y") * sensitivityMouse;// -1, 0, 1
        mouseY = Mathf.Clamp(mouseY, minAngle, maxAngle);
        return new Vector3(mouseX, mouseY, 0);
    }
    public void ZoomCamera() 
    {
        currentZoomMouse -= Input.GetAxis("Mouse ScrollWheel") * speedZoom; // -1,0,1
        currentZoomMouse = Mathf.Clamp(currentZoomMouse, minZoom, maxZoom);

        transform.position = target.position - transform.forward * currentZoomMouse;
    }
    public bool CheckCameraRotateAngle()
    {
        Vector3 cameraZ = Vector3.ProjectOnPlane(trCam.forward, Vector3.up).normalized;
        Vector3 characterZ = Vector3.ProjectOnPlane(target.forward, Vector3.up).normalized;
        float currentAngle = Vector3.SignedAngle(cameraZ, characterZ, Vector3.up); 
        if (Mathf.Abs(currentAngle) > 15)
        {
            Debug.Log("angle > 15");
            return true;
        }
        else if (Mathf.Abs(currentAngle) > 45)
        {
            Debug.Log("angle > 45");
            return true;
        }
        else return false;
    }
}
