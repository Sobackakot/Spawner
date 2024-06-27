
using UnityEngine; 

public  class TestFunction : MonoBehaviour 
{
    public GameObject myPrefabCube;

    private void Awake()
    {
        // 1 ��� ��� ������ ���� 
        Debug.Log("Awake");
    }
    private void OnEnable()
    {
        // 1 ��� ��� ��������� �������� �������� �� ������� ����� ������ ������
        Debug.Log("on Enable");
        
    }
    private void Start()
    {

        // 1 ��� ��� ������ ����
        Debug.Log("start"); 
    }
    private void Update()
    {
        // ���������� ��� ������ ���� � �� ��������� ���� 
        Debug.Log("Update");
    }
    private void LateUpdate()
    {
        Debug.Log("LateUpdate");
    }
    private void OnDisable()
    {
        // 1 ��� ��� ����������� �������� �������� �� ������� ����� ������ ������
        // 1 ��� ��� �������� �������� �������� �� ������� ����� ������ ������
        Debug.Log("on DISABLE"); 
    }
    private void OnDestroy()
    {
        Debug.Log("on Destroy");
    } 
    private void OnMouseEnter()
    {
        Debug.Log("OnMouseEnter");
    }
    private void OnMouseExit()
    {
        Debug.Log("OnMouseExit");
    }
    private void OnMouseDown()
    {
        Debug.Log("OnMouseDown");
    } 
    private void OnMouseUp()
    {
        Debug.Log("OnMouseUp");
    } 
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if(other.gameObject.tag == "Player")
        {    
            Destroy(other.gameObject); 
        } 
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("stay");
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit");
    }

    private void OnCollisionEnter(Collision other)
    { 
    }
    private void OnCollisionStay(Collision other)
    {
        
    }
    private void OnCollisionExit(Collision other)
    {
        
    }
}

 