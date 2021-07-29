using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour, InputActions.IGameplayActions
{
    public static event UnityAction onMoveLeft = delegate {};
    public static event UnityAction onMoveRight = delegate {};
    public static event UnityAction onDrop = delegate {};
    public static event UnityAction onCancelDrop = delegate {};
    public static event UnityAction onRotate = delegate {};

    static InputActions inputActions;

    void Awake(){
        inputActions = new InputActions();
        inputActions.Gameplay.SetCallbacks(this);
    }
    void OnEnable(){
        EnableGameplayInputs();
    }
    void OnDisable(){
        DisableAllInputs();
    }

    public static void EnableGameplayInputs(){
        inputActions.Gameplay.Enable();
    }
    public static void DisableAllInputs(){
        inputActions.Gameplay.Disable();
    }

    public void OnMoveLeft(InputAction.CallbackContext context)
    {
        if(context.performed){
            onMoveLeft.Invoke();
        }
    }

    public void OnMoveRight(InputAction.CallbackContext context)
    {
        if(context.performed){
            onMoveRight.Invoke();
        }
    }

    
    public void OnDrop(InputAction.CallbackContext context)
    {
        if(context.performed){
            onDrop.Invoke();
        }
        if(context.canceled){
            onCancelDrop.Invoke();
        }
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        if(context.performed){
            onRotate.Invoke();
        }
    }
}
