using UnityEngine;
using Mirror;
public class InputManager : NetworkBehaviour
{
    private InputSystem_Actions _inputSystem;

    void Awake(){ _inputSystem.Enable();}
    private void onDisable() { _inputSystem.Disable();}
    private void onEnable() { _inputSystem.Enable();}


    void Update()
    {
        
    }
}
