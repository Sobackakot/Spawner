
using System; 
using UnityEngine;

public class EventScript : MonoBehaviour
{   
   // Action никогда ничего не вовзращает
    public event Action<string> onShowText; // событие Action может иметь параметры,
                                                          // любых типов данных и любое количество 
    public event Action onMouseEnter2; // или папраметры могут отсудствовать

    // объязательно возращает какой-нибудь тип данных
    // (пишем всегда вначале и один), и может иметь любое количество параметров
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
