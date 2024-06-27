
using UnityEngine;

public class MovePetsTest : MonoBehaviour
{
    private Vector3 newDirection;
    private Rigidbody personRigidbody;
    private float speed = 8f;

    public void Awake()
    {
        personRigidbody = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        GetDirectionMove();
        MovePerson();
    }

    private void GetDirectionMove()
    {
        float x = Input.GetAxis("Horizontal"); // от -1 до 1 или 0  - key = a,d
        float z = Input.GetAxis("Vertical"); // от -1 до 1 или 0 - key = w,s
        newDirection = new Vector3(x,0,z);
    }
    private void MovePerson()
    {
        personRigidbody.MovePosition(personRigidbody.position + newDirection * speed * Time.deltaTime);
    }
}
