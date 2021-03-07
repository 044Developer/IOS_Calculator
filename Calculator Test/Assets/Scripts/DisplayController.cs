using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayController : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI _digitsText;
    [SerializeField] 
    private TextMeshProUGUI _operatorText;
    [SerializeField] 
    private int _maxDigitsCount;

    private CalculatorLogic _calculatorLogic;

    private void Start()
    {
        _calculatorLogic = new CalculatorLogic(_digitsText, _operatorText, _maxDigitsCount);  
    }    

    public void RegisterInput(char pressedButton)
    {
        _calculatorLogic.ButtonPressed(pressedButton);
    }
}
