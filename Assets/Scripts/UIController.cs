using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    //made open and close bottom sheet
    private VisualElement _bottomContainer;
    private Button _openButton;
    private Button _closeButton;

    //made animation of bottom sheet
    private VisualElement _bottomSheet;
    private VisualElement _scrim;

    //spaceboy animation
    private VisualElement _spaceBoy;

    private void Start()
    {
        #region Bottom Container, Open and Close Button Logic

        var root = GetComponent<UIDocument>().rootVisualElement;

        _bottomContainer = root.Q<VisualElement>("Container_Bottom");

        _openButton = root.Q<Button>("Button_Open");
        _closeButton = root.Q<Button>("Button_Close");
        //hide all bottom sheets elements at start
        _bottomContainer.style.display = DisplayStyle.None;

        _openButton.RegisterCallback<ClickEvent>(OnOpenButtonClicked);
        _closeButton.RegisterCallback<ClickEvent>(OnCloseButtonClicked);

        #endregion

        #region BottomSheet and Scrim Variables

        _bottomSheet = root.Q<VisualElement>("BottomSheet");
        _scrim = root.Q<VisualElement>("Scrim");

        #endregion

        #region spaceboy Animation

        _spaceBoy = root.Q<VisualElement>("Image_Spaceboy");
        Invoke(nameof(AnimateBoy), .1f);

        #endregion
    }

    private void Update()
    {
        //Debug.Log(_spaceBoy.ClassListContains("image--boy--inair"));
    }

    private void OnOpenButtonClicked(ClickEvent evt)
    {
        //display bottom container
        _bottomContainer.style.display = DisplayStyle.Flex;

        //play animation
        _bottomSheet.AddToClassList("bottomsheet--up");
        _scrim.AddToClassList("scrim--fadein");
    }

    private void OnCloseButtonClicked(ClickEvent evt)
    {
        _bottomContainer.style.display = DisplayStyle.None;

        //making loop animation
        _bottomSheet.RemoveFromClassList("bottomsheet--up");
        _scrim.RemoveFromClassList("scrim--fadein");
    }

    private void AnimateBoy()
    {
        _spaceBoy.RemoveFromClassList("image--boy--inair");
    }
}
