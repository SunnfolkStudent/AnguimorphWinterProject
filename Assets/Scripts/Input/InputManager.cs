using System;
using UnityEngine;
using Mirror;
using UnityEngine.InputSystem;
public class InputManager : MonoBehaviour
{
    private TouchControls _inputSystem;

    public Vector2 touchPosition;
    public bool tap;
    public Vector2 DeltaSwipe;
    public bool touchPress;
    private void Awake()
    {
        _inputSystem = new TouchControls();
    }

    private void Update()
    {
        touchPosition = _inputSystem.Touch.TouchPosition.ReadValue<Vector2>();
        tap = _inputSystem.Touch.Tap.WasPressedThisFrame();
        DeltaSwipe = _inputSystem.Touch.Swipe.ReadValue<Vector2>();
        touchPress = _inputSystem.Touch.TouchPress.WasPressedThisFrame();


    }

    private void OnEnable()
    {
        _inputSystem.Enable();  
    }

    private void OnDisable()
    {
        _inputSystem.Disable();
    }
}

