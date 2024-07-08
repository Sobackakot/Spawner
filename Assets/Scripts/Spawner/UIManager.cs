using TMPro;
using UnityEngine;
using UnityEngine.UI; 


public class UIManager : MonoBehaviour
{   
    public static UIManager instance;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private TextMeshProUGUI intervalText;
    [SerializeField] private InputField inputFieldSpeed;
    [SerializeField] private InputField inputFieldDistance;
    [SerializeField] private InputField inputFieldInterval;
    public float speed { get; set; }
    public float distance { get; set; }
    public float interval { get; set; } 
    #region Singleton
    private void Awake()
    {
        instance = this;
    }
    #endregion 

    public void SaveInputText()
    {  
        if(float.TryParse(inputFieldSpeed.text, out float speedValue))
        speed = speedValue;
        if(float.TryParse(inputFieldDistance.text, out float distanceValue))
        distance = distanceValue;
        if(float.TryParse(inputFieldInterval.text, out float intervalValue))
        interval = intervalValue;
    }
    public void ShowText()
    {
        speedText.text = speed.ToString();
        distanceText.text = distance.ToString();
        intervalText.text = interval.ToString();
    }
}
