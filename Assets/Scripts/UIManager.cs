using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{   
    public static UIManager instance;
    [SerializeField] private Text inputTextSpeed, speedText;
    [SerializeField] private Text inputTextDistace, distanceText;
    [SerializeField] private Text inputTextInterval, intervalText;
    [SerializeField] private InputField inputFieldSpeed;
    [SerializeField] private InputField inputFieldDistance;
    [SerializeField] private InputField inputFieldInterval;
    public string speed;
    public string distance;
    public string interval;
    #region S
    private void Awake()
    {
        instance = this;
    }
    #endregion 
    public void SaveInputText()
    {
        speed = inputFieldSpeed.text;
        distance = inputFieldDistance.text;
        interval = inputFieldInterval.text;
    }
    public void ShowText()
    {
        speedText.text = speed;
        distanceText.text = distance;
        intervalText.text = interval;
    }
}
