using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    //grab our bottom sheet elements
    private VisualElement _bottomContainer;
    private Button _openButton;
    private Button _closeButton;

    private void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        _bottomContainer = root.Q<VisualElement>("Container_Bottom");

        _openButton = root.Q<Button>("Button_Open");
        _closeButton = root.Q<Button>("Button_Close");
        //hide all bottom sheets elements at start
        _bottomContainer.style.display = DisplayStyle.None;

        _openButton.RegisterCallback<ClickEvent>(OnOpenButtonClicked);
        _closeButton.RegisterCallback<ClickEvent>(OnCloseButtonClicked);
    }

    private void OnOpenButtonClicked(ClickEvent evt)
    {
        _bottomContainer.style.display = DisplayStyle.Flex;
    }

    private void OnCloseButtonClicked(ClickEvent evt)
    {
        _bottomContainer.style.display = DisplayStyle.None;
    }
}
