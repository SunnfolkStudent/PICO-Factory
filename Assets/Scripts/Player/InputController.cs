using UnityEngine;

public class InputController : MonoBehaviour
{
    private InputSystem_Actions _inputSystem;
    void Awake()
    {
        _inputSystem = new InputSystem_Actions();
    }
    void OnEnable() {_inputSystem.Enable();}
    void OnDisable() {_inputSystem.Disable();}
    public float Horizontal;
    public float Vertical;
    public bool Jump;
    
    public void Update()
    {
        Horizontal = _inputSystem.Player.Move.ReadValue<Vector2>().x;
        Vertical = _inputSystem.Player.Move.ReadValue<Vector2>().y;
        Jump = _inputSystem.Player.Jump.WasPressedThisFrame();
    }

}
