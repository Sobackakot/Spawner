
using UnityEngine;

public class PlayerController : MonoBehaviour
{    
    public Transform transformCamera; // ������� ������ �� ������ ����� ��������  �������������
    public Animator anim_Person; //������� ������ �� ��������� �������� ��������� ����� �������� ��������
    public Rigidbody rb_Person;  // ������� ������ �� ��������� Rigidbody ����� ��������� �������� ��������� ����� ������

    public float currentSpeed = 10f; // �������� �������� ���������
    public float jumpForce = 5f; // ���� ������ ���������
       
    private bool isTerra = false; // ������ ��� �������� ��� �������� ����� �� ����������� ����� � �� �� �������
    private bool isRunning = false; //��������� ��� �� ����� ����� ���� 

    private Vector3 directionInput;

  

    private void Update()
    {
        CalculateDirectionMove();
        Jumping(); 
    }
    private void CalculateDirectionMove()
    {    
        Vector3 cameraAxisZ = Vector3.ProjectOnPlane(transformCamera.forward, Vector3.up); // �������� ����������� ������ �� ��� Z
        Vector3 cameraAxisX = Vector3.ProjectOnPlane(transformCamera.right, Vector3.up); // �������� ����������� ������ �� ��� X

        //Input.GetAxis("Horizontal") = ��� ����� ����� ���� �� ������� ������ � ����� ��� � ���� (D)=1 ��� (A)=-1  
        //Input.GetAxis("Vertical") = ��� ����� ����� ���� �� ������� ������ � ������ ��� ����� (W)=1 ���  (S)=-1  
        //new Vector3 ��� ��� ��� �� X,Y,Z
        float X = Input.GetAxis("Horizontal");
        float Z = Input.GetAxis("Vertical");  

        directionInput = new Vector3(X, 0, Z); // �������� ������� ����������� �������� ���������

        Vector3 newDirectionMove = (directionInput.z * cameraAxisZ) + (directionInput.x * cameraAxisX);// �������� ���������� � ����������� ������

        // ��������� ��� �������� ����� �������� ��� ���� ����� ������ ������� ��������� � ������� ����������� ������
        if (newDirectionMove.sqrMagnitude >= 0.2f) 
            transform.rotation = Quaternion.LookRotation(newDirectionMove, Vector3.up); //������������ ��������� � ������� ������

        MovePerson(newDirectionMove); // �������� �������� ��������� �� ����������� ������
        PlayPersonAnim(directionInput); // �������� �������� ��������� � ������� ��������� ����
    }
    private void MovePerson(Vector3 directionMove)// �������� �������� � �������� ��������� ����������� ��������
    {
        isRunning = Input.GetKey(KeyCode.LeftShift); //��������� ������� ������ Shift

        //���� ������ ���� �� �������� �������� �������� �� 1, ��� shift -> ��������� ������� �������� ������ ������� �������� �������� �� 0,4
        float moveSpeed = currentSpeed * (isRunning ? 1 : 0.4f); // ������ �������� �������� ����� currentSpeed = 10
        rb_Person.MovePosition(rb_Person.position + directionMove * moveSpeed * Time.deltaTime); // �������� �������� ������ �� ����
        
    }
    private void PlayPersonAnim(Vector3 m_Input)
    {
        float animationSpeed = isRunning ? 1 : 0.5f; // ��� shift -> ��������� ������� �������� ��������
        if (m_Input.sqrMagnitude > 0) // �������� �������� � ��������� ����� ��������� ��� ��� �������� ��������� � ��������
        { 
            // ��� ���� �������� ��������� �� ��� X � ��������� ��������� 0,1 � �����
            anim_Person.SetFloat("velocityX", m_Input.x * animationSpeed, 0.1f, Time.deltaTime);
            // ��� ���� �������� ��������� �� ��� Z (������ Y) � ��������� ��������� 0,1 � �����
            anim_Person.SetFloat("velocityY", m_Input.z * animationSpeed, 0.1f, Time.deltaTime); 
        }
        else
        {
            // ��������� �������� = 0 ��� ���� ���� � 0,1 ��� ��������� ��������� ������������ ����� ����������  � ����� Time
            anim_Person.SetFloat("velocityX", 0, 0.1f, Time.deltaTime);
            //��������� �������� = 0 ��� ���� ���� � 0,1 ��������� ��������� ����������������� ���������� � ����� Time
            anim_Person.SetFloat("velocityY", 0, 0.1f, Time.deltaTime); 
        }
    }
    private void Jumping()
    {
        bool isJump = Input.GetKeyDown(KeyCode.Space); // ��������� ������� ������ ������ 
        if (isTerra & isJump) // ��������� ��� ������ ����� Space � ��� �������� ��������� �� ������������ �����
        {
            anim_Person.SetBool("isJumping", true); // ��� ���� ����

            //AddForce ��� ������� ������ �������� ������ � ����������� Rigidbody
            rb_Person.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // ��������� ���� � ���� ��� ������,
            isTerra = false; // ����� ������ ���������� ������ ��  false ����� �������� �� ��� �������
        }
        else anim_Person.SetBool("isJumping", false); // ��������� �������� ������
    }
    private void OnCollisionEnter(Collision collision) // ������� ������������ ������� ��������� � �������������
    {
        if(collision.gameObject.tag == "Terra") //�������� ���� �������� �� ����������� ����� �� ����� �������
            isTerra = true; // ������������� ������ �� true ����� ����� ���� �������

    }

}
