
using UnityEngine;

public class TriggetTest : MonoBehaviour
{
    [SerializeField] private float forceSpeed = 10f;
    [SerializeField] private float dragMass = 3f;

    private void OnTriggerEnter(Collider other)
    {
        other.attachedRigidbody.drag = 10f;
        other.attachedRigidbody.AddForce(Vector3.up * forceSpeed);
    }
    private void OnTriggerStay(Collider other)
    {
        other.attachedRigidbody.drag = dragMass;
        other.attachedRigidbody.AddForce(Vector3.up * forceSpeed); 
    }
    private void OnTriggerExit(Collider other)
    { 
        other.attachedRigidbody.drag = 0;
    }
}
