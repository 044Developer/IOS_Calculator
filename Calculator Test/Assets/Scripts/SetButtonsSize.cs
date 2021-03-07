using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetButtonsSize : MonoBehaviour
{
    private const int ButtonsInRow = 4;
    private const int ButtonWidthMultiplier = 2;
    private const int FirstButtonInRow = 0;

    [SerializeField] 
    private List<RectTransform> _buttonsList;
    [SerializeField] 
    private RectTransform _canvasRect;
    [SerializeField] 
    private HorizontalLayoutGroup _bottonRow;

    private void Start()
    {
        _bottonRow.childControlWidth = false;
    }

    private void OnRectTransformDimensionsChange()
    {
        SetCorrectSize();
    }

    private void SetCorrectSize()
    {
        float buttonSize = _canvasRect.sizeDelta.x / ButtonsInRow;
        float buttonWidth = buttonSize - _bottonRow.spacing;
        for (int i = 0; i < _buttonsList.Count; i++)
        {
            _buttonsList[i].SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, buttonWidth);
        }

        _buttonsList[FirstButtonInRow].SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, buttonWidth * ButtonWidthMultiplier + _bottonRow.spacing);
    }
}
