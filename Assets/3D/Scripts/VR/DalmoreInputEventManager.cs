using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public struct InputEventData
{
    // 왼손
    public UnityAction<float> leftIndexTrigger;         // Axis1D.PrimaryIndexTrigger
    public UnityAction<float> leftHandTrigger;          // Axis1D.PrimaryHandTrigger
    public UnityAction<Vector2> leftThumbstick;         // Axis2D.PrimaryThumbstick
    public UnityAction leftIndexTriggerPressed;         // Axis1D.PrimaryInedxTrigger (Pressed)
    public UnityAction leftHandTriggerPressed;          // Axis1D.PrimaryHandTrigger (Pressed)
    public UnityAction leftThumbstickPressed;           // Button.PrimaryThumbstick (left stick press)
    public UnityAction xButton;                         // Button.Three
    public UnityAction yButton;                         // Button.Four
    public UnityAction startButton;                     // Button.Start


    // 오른손
    public UnityAction<float> rightIndexTrigger;        // Axis1D.SecondaryIndexTrigger
    public UnityAction<float> rightHandTrigger;         // Axis1D.SecondaryHandTrigger
    public UnityAction<Vector2> rightThumbstick;        // Axis2D.SecondaryThumbstick
    public UnityAction rightIndexTriggerPressed;        // Axis1D.SecondaryInedxTrigger (Pressed)
    public UnityAction rightHandTriggerPressed;         // Axis1D.SecondaryHandTrigger (Pressed)
    public UnityAction rightThumbstickPressed;          // Button.SecondaryThumbstick (right stick press)
    public UnityAction aButton;                         // Button.One
    public UnityAction bButton;                         // Button.Two
    public UnityAction reservedButton;                  // Button.Reserved
    public UnityAction<Vector3> rightHandPosition;      // Right Controller Pos


    // +,- 연산자 오버로딩                                                        
    public static InputEventData operator +(InputEventData A, InputEventData B)
    {
        // 왼손
        if (B.leftIndexTrigger != null) A.leftIndexTrigger += B.leftIndexTrigger;
        if (B.leftHandTrigger != null) A.leftHandTrigger += B.leftHandTrigger;
        if (B.leftThumbstick != null) A.leftThumbstick += B.leftThumbstick;
        if (B.leftIndexTriggerPressed != null) A.leftIndexTriggerPressed += B.leftIndexTriggerPressed;
        if (B.leftHandTriggerPressed != null) A.leftHandTriggerPressed += B.leftHandTriggerPressed;
        if (B.leftThumbstickPressed != null) A.leftThumbstickPressed += B.leftThumbstickPressed;
        if (B.xButton != null) A.xButton += B.xButton;
        if (B.yButton != null) A.yButton += B.yButton;
        if (B.startButton != null) A.startButton += B.startButton;

        // 오른손
        if (B.rightIndexTrigger != null) A.rightIndexTrigger += B.rightIndexTrigger;
        if (B.rightHandTrigger != null) A.rightHandTrigger += B.rightHandTrigger;
        if (B.rightThumbstick != null) A.rightThumbstick += B.rightThumbstick;
        if (B.rightIndexTriggerPressed != null) A.rightIndexTriggerPressed += B.rightIndexTriggerPressed;
        if (B.rightHandTriggerPressed != null) A.rightHandTriggerPressed += B.rightHandTriggerPressed;
        if (B.rightThumbstickPressed != null) A.rightThumbstickPressed += B.rightThumbstickPressed;
        if (B.aButton != null) A.aButton += B.aButton;
        if (B.bButton != null) A.bButton += B.bButton;
        if (B.reservedButton != null) A.reservedButton += B.reservedButton;
        if (B.rightHandPosition != null) A.rightHandPosition += B.rightHandPosition;

        return A;
    }
    public static InputEventData operator -(InputEventData A, InputEventData B)
    {
        // 왼손
        A.leftIndexTrigger -= B.leftIndexTrigger;
        A.leftHandTrigger -= B.leftHandTrigger;
        A.leftThumbstick -= B.leftThumbstick;
        A.leftIndexTriggerPressed -= B.leftIndexTriggerPressed;
        A.leftHandTriggerPressed -= B.leftHandTriggerPressed;
        A.leftThumbstickPressed -= B.leftThumbstickPressed;
        A.xButton -= B.xButton;
        A.yButton -= B.yButton;
        A.startButton -= B.startButton;

        // 오른손
        A.rightIndexTrigger -= B.rightIndexTrigger;
        A.rightHandTrigger -= B.rightHandTrigger;
        A.rightThumbstick -= B.rightThumbstick;
        A.rightIndexTriggerPressed -= B.rightIndexTriggerPressed;
        A.rightHandTriggerPressed -= B.rightHandTriggerPressed;
        A.rightThumbstickPressed -= B.rightThumbstickPressed;
        A.aButton -= B.aButton;
        A.bButton -= B.bButton;
        A.reservedButton -= B.reservedButton;
        A.rightHandPosition -= B.rightHandPosition;

        return A;
    }
}

public class DalmoreInputEventManager : Singleton<DalmoreInputEventManager>
{
    [SerializeField] private PlayerInput m_pi;
    private InputEventData m_inputEvent = new InputEventData();
    // 입력 이벤트 관리
    public void AddInputEvent(InputEventData inputEvent)
    {
        m_inputEvent += inputEvent;
    }
    public void RemoveInputEvent(InputEventData inputEvent)
    {
        m_inputEvent -= inputEvent;
    }
    public void RemoveAllInputEvent()
    {
        m_inputEvent = new InputEventData();
    }
    #region InputEvents
    // 왼손 이벤트
    public void OnLeftIndexTrigger(InputAction.CallbackContext context)
    {
        float axis1D = context.ReadValue<float>();
        m_inputEvent.leftIndexTrigger?.Invoke(axis1D);
    }
    public void OnLeftHandTrigger(InputAction.CallbackContext context)
    {
        float axis1D = context.ReadValue<float>();
        m_inputEvent.leftHandTrigger?.Invoke(axis1D);
    }
    public void OnLeftThumbstick(InputAction.CallbackContext context)
    {
        Vector2 axis2D = context.ReadValue<Vector2>();
        m_inputEvent.leftThumbstick?.Invoke(axis2D);
    }
    public void OnLeftIndexTriggerPressed(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        m_inputEvent.leftIndexTriggerPressed?.Invoke();
    }
    public void OnLeftHandTriggerPressed(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        m_inputEvent.leftHandTriggerPressed?.Invoke();
    }
    public void OnLeftThumbstickPressed(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        m_inputEvent.leftThumbstickPressed?.Invoke();
    }
    public void OnXButton(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        
        m_inputEvent.xButton?.Invoke();
    }
    public void OnYButton(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        m_inputEvent.yButton?.Invoke();
    }
    public void OnStartButton(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        m_inputEvent.startButton?.Invoke();
    }
    // 오른손 이벤트
    public void OnRightIndexTrigger(InputAction.CallbackContext context)
    {
        float axis1D = context.ReadValue<float>();
        m_inputEvent.rightIndexTrigger?.Invoke(axis1D);
    }
    public void OnRightHandTrigger(InputAction.CallbackContext context)
    {
        float axis1D = context.ReadValue<float>();
        m_inputEvent.rightHandTrigger?.Invoke(axis1D);
    }
    public void OnRightThumbstick(InputAction.CallbackContext context)
    {
        Vector2 axis2D = context.ReadValue<Vector2>();
        m_inputEvent.rightThumbstick?.Invoke(axis2D);
    }
    public void OnRightIndexTriggerPressed(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        m_inputEvent.rightIndexTriggerPressed?.Invoke();
    }
    public void OnRightHandTriggerPressed(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        m_inputEvent.rightHandTriggerPressed?.Invoke();
    }
    public void OnRightThumbstickPressed(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        m_inputEvent.rightThumbstickPressed?.Invoke();
    }
    public void OnAButton(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        m_inputEvent.aButton?.Invoke();
    }
    public void OnBButton(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        m_inputEvent.bButton?.Invoke();
    }
    public void OnReservedButton(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        m_inputEvent.reservedButton?.Invoke();
    }

    public void OnRightHandPosition(InputAction.CallbackContext context)
    {
        m_inputEvent.rightHandPosition?.Invoke(context.ReadValue<Vector3>());
    }
    public void OnRightHandMousePosition(InputAction.CallbackContext context)
    {
        Vector3 pos = Input.mousePosition;
        pos.x /= Screen.width;
        pos.y /= Screen.height;
        m_inputEvent.rightHandPosition?.Invoke(pos);
    }
    #endregion
}
