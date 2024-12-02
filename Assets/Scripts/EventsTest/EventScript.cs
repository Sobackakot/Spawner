
using System; 
using UnityEngine;

public class EventScript : MonoBehaviour
{   
   // Action ������� ������ �� ����������
    public event Action<string> onShowText; // ������� Action ����� ����� ���������,
                                                          // ����� ����� ������ � ����� ���������� 
    public event Action onMouseEnter2; // ��� ���������� ����� �������������

    // ������������ ��������� �����-������ ��� ������
    // (����� ������ ������� � ����), � ����� ����� ����� ���������� ����������
    public event Func<int, int, string, float > onMouseEnter3;
    public event Func<string> onMouseEnter4;

    public void Start()
    {
        
        OtherFunbc();
    }
 
    public void OtherFunbc()
    {
        onShowText.Invoke("Hello World");
    }
}
