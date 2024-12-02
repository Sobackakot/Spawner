 
using UnityEngine;

public class ListenerEvents : MonoBehaviour
{
    public EventScript eventScript;

    private void OnEnable()
    {
        eventScript.onShowText += ShowText; // �������� 
    }
    private void OnDisable()
    {
        eventScript.onShowText -= ShowText;
    }

    public void ShowText(string text)
    {
        Debug.Log(text);
    }
}
