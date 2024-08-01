using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;

public class UIController : MonoBehaviour
{
    //Const Strings root Q elements
    private const string  CONTAINER_BOTTOM = "Container_Bottom";
    private const string  BOTTOM_SHEET = "BottomSheet";
    private const string  SCRIM = "Scrim";
    private const string  IMAGE_SPACEBOY = "Image_Spaceboy";
    private const string  IMAGE_GIRL = "Image_Girl";
    private const string  MESSAGE = "Message";

    //Const Class Names
    private const string BOTTOMSHEET_UP = "bottomsheet--up";
    private const string SCRIM_FADEIN = "scrim--fadein";
    private const string IMAGE_GIRL_UP = "image--girl--up";
    private const string IMAGE_BOY_INAIR = "image--boy--inair";



    //made open and close bottom sheet
    private VisualElement _bottomContainer;
    private Button _openButton;
    private Button _closeButton;

    //made animation of bottom sheet
    private VisualElement _bottomSheet;
    private VisualElement _scrim;

    //spaceboy animation
    private VisualElement _spaceBoy;

    //girl portrait animation
    private VisualElement _spaceGirl;

    //message work
    private Label _labelMessage;
    private const string MESSAGE_TEXT = "\"Lorem ipsum odor amet, consectetuer adipiscing.\"";

    private void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        #region root Q get elements
        _bottomContainer = root.Q<VisualElement>(CONTAINER_BOTTOM);

        _openButton = root.Q<Button>("Button_Open");
        _closeButton = root.Q<Button>("Button_Close");

        _bottomSheet = root.Q<VisualElement>(BOTTOM_SHEET);
        _scrim = root.Q<VisualElement>(SCRIM);
        _spaceBoy = root.Q<VisualElement>(IMAGE_SPACEBOY);
        _spaceGirl = root.Q<VisualElement>(IMAGE_GIRL);

        _labelMessage = root.Q<Label>(MESSAGE);

        #endregion

        //hide all bottom sheets elements at start
        _bottomContainer.style.display = DisplayStyle.None;

        Invoke(nameof(AnimateBoy), .1f);

        #region Register Callbacks

        _openButton.RegisterCallback<ClickEvent>(OnOpenButtonClicked);
        _closeButton.RegisterCallback<ClickEvent>(OnCloseButtonClicked);
        _bottomSheet.RegisterCallback<TransitionEndEvent>(OnBottomSheetDown);

        #endregion
    }

    private void OnDisable()
    {
        _openButton.UnregisterCallback<ClickEvent>(OnOpenButtonClicked);
        _closeButton.UnregisterCallback<ClickEvent>(OnCloseButtonClicked);
        _bottomSheet.UnregisterCallback<TransitionEndEvent>(OnBottomSheetDown);
    }

    private void OnOpenButtonClicked(ClickEvent evt)
    {
        //display bottom container
        _bottomContainer.style.display = DisplayStyle.Flex;

        //play animation
        _bottomSheet.ToggleInClassList(BOTTOMSHEET_UP);
        _scrim.AddToClassList(SCRIM_FADEIN);

        //play spacegirl animation
        AnimateGirl();
    }

    private void OnCloseButtonClicked(ClickEvent evt)
    {
        //making loop animation
        _bottomSheet.ToggleInClassList(BOTTOMSHEET_UP);
        _scrim.RemoveFromClassList(SCRIM_FADEIN);
    }

    private void OnBottomSheetDown(TransitionEndEvent evt)
    {
        if (!_bottomSheet.ClassListContains(BOTTOMSHEET_UP))
        {
            _bottomContainer.style.display = DisplayStyle.None;
        }
    }

    private void AnimateBoy()
    {
        _spaceBoy.RemoveFromClassList(IMAGE_BOY_INAIR);
    }

    private void AnimateGirl()
    {
        _spaceGirl.ToggleInClassList(IMAGE_GIRL_UP);

        _spaceGirl.RegisterCallback<TransitionEndEvent>
            (
            evt => _spaceGirl.ToggleInClassList(IMAGE_GIRL_UP)
            );
        
        //animating message
        _labelMessage.text = string.Empty;
        string m = MESSAGE_TEXT;
        DOTween.To(() => _labelMessage.text, x => _labelMessage.text = x, m, 3f).SetEase(Ease.Linear);
    }
}
