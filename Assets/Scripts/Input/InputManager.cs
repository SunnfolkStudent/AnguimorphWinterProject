using UnityEngine;
using Mirror;
using UnityEngine.InputSystem;
public class InputManager : NetworkBehaviour
{
    private TouchControls _touchControls;
    
    public float SwipeHorizontal;
    public float SwipeVertical;
    

    void Awake()
    {
        _touchControls.Enable();
    }

    void Update()
    {
        SwipeHorizontal = _touchControls.Touch.Swipe.ReadValue<Vector2>().x;
        SwipeHorizontal = _touchControls.Touch.Swipe.ReadValue<Vector2>().y;
        
    }
    
    private void onDisable() { _touchControls.Disable();}
    private void onEnable() { _touchControls.Enable();}


    
}
