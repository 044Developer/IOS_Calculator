using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CulculatorButtonBehaviour : MonoBehaviour
{
    private const int FirstDigit = 0;

    [SerializeField] 
    private TextMeshProUGUI _symbolText;

    private DisplayController _displayController;

    private void Start()
    {
        _displayController = GetComponentInParent<DisplayController>();
    }

    public void OnButtonPressed()
    {
        _displayController.RegisterInput(_symbolText.text[FirstDigit]);
    }
}
