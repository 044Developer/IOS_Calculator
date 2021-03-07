using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CalculatorLogic
{
    // Operations Signs
    private const char Add = '+';
    private const char Subtract = '-';
    private const char Multiply = 'X';
    private const char Devide = '÷';
    private const char Equal = '=';
    private const char Percentage = '%';
    private const char PlusMinus = '±';
    private const char Clear = 'C';

    // Number Digit
    private const char MinKeyboardDigit = '0';
    private const char MaxKeyboardDigit = '9';
    private const char FloatingPoint = ',';

    //Output Texts
    private const char EmptyChar = ' ';
    private const string EmptyField = "";
    private const string ZeroOutput = "0";
    private const string Error = "ERROR";

    // Math constants
    private const double GetPercentage = 100.0d;

    public CalculatorLogic (TextMeshProUGUI digitsField, TextMeshProUGUI operatorField, int maxDigitsCount)
    {
        _digitField = digitsField;
        _operatorField = operatorField;
        _maxDigitsCount = maxDigitsCount;
        Initialize();
    }

    private TextMeshProUGUI _digitField;
    private TextMeshProUGUI _operatorField;
    private double _currentValue;
    private double _storedValue;
    private double _result;
    private int _maxDigitsCount;
    private char _currentOperator;
    private bool _errorWithOperation;
    private bool _specialAction;
    private bool _inputIsRegistered;

    public void ButtonPressed(char pressedButton)
    {
        if (_errorWithOperation)
        {
            ClearCalculatorCash();
        }

        HandleInput(pressedButton);
    }
                   
    private void Initialize()
    {
        ButtonPressed(Clear);
    }

    private void HandleInput(char pressedButton)
    {
        if (pressedButton >= MinKeyboardDigit && pressedButton <= MaxKeyboardDigit || pressedButton == FloatingPoint)
        {
            if (_digitField.text.Length < _maxDigitsCount || !_inputIsRegistered)
            {
                if (!_inputIsRegistered)
                {
                    _digitField.text = (pressedButton == FloatingPoint ? ZeroOutput : EmptyField);
                }
                else if (_digitField.text == ZeroOutput && pressedButton != FloatingPoint)
                {
                    _digitField.text = EmptyField;
                }
                _digitField.text += pressedButton;
                _inputIsRegistered = true;
            }
        }
        else if (pressedButton == Clear)
        {
            ClearCalculatorCash();
        }
        else if (pressedButton == PlusMinus)
        {
            _currentValue = -double.Parse(_digitField.text);
            UpdateOutputText();
            _specialAction = true;
        }
        else if (pressedButton == Percentage)
        {
            _currentValue = double.Parse(_digitField.text) / GetPercentage;
            UpdateOutputText();
            _specialAction = true;
        }
        else if (_inputIsRegistered || _currentOperator == Equal || _specialAction)
        {
            _currentValue = double.Parse(_digitField.text);
            _inputIsRegistered = false;
            if (_currentOperator != EmptyChar)
            {
                CalculateResult(_currentOperator);
                _currentOperator = EmptyChar;
            }
            _operatorField.text = pressedButton.ToString();
            _currentOperator = pressedButton;
            _storedValue = _currentValue;
            UpdateOutputText();
            _specialAction = false;
        }
    }


    private void ClearCalculatorCash()
    {
        _digitField.text = ZeroOutput;
        _operatorField.text = EmptyField;
        _currentValue = 0;
        _storedValue = 0;
        _result = 0;
        _currentOperator = EmptyChar;
        _errorWithOperation = false;
        _specialAction = false;
        _inputIsRegistered = false;
    }

    private void UpdateOutputText()
    {
        if (!_errorWithOperation)
        {
            _digitField.text = _currentValue.ToString();
        }
        _inputIsRegistered = false;
    }

    private void CalculateResult(char activeOperator)
    {
        switch (activeOperator)
        {
            case Equal:
                _result = _currentValue;
                break;
            case Add:
                _result = _storedValue + _currentValue;
                break;
            case Subtract:
                _result = _storedValue - _currentValue;
                break;
            case Multiply:
                _result = _storedValue * _currentValue;
                break;
            case Devide:
                if (_currentValue != 0)
                {
                    _result = _storedValue / _currentValue;
                }
                else
                {
                    _errorWithOperation = true;
                    _digitField.text = Error;
                }
                break;
            default:
                Debug.Log($"Error with operation {activeOperator}");
                break;
        }

        _currentValue = _result;
        UpdateOutputText();
    }
}
