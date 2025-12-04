using UnityEngine;
using Mirror;
using UnityEngine.InputSystem;
public class InputManager : MonoBehaviour
{
    private TouchControls _touchControls;
    
    public float SwipeHorizontal;
    public float SwipeVertical;
    

    void Awake()
    {
        _touchControls = new TouchControls();
        
    }
    private void OnEnable() { _touchControls.Enable();}
    private void OnDisable() { _touchControls.Disable();}

    void Update()
    {
        SwipeHorizontal = _touchControls.Touch.Swipe.ReadValue<Vector2>().x;
        SwipeHorizontal = _touchControls.Touch.Swipe.ReadValue<Vector2>().y;
        
    }
    
    


    
}
