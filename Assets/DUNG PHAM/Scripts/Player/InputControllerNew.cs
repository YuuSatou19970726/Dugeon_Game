using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControllerNew : MonoBehaviour
{
    public static InputControllerNew instance;
    public bool canInput = true;

    public float inputX, inputY;
    public float inputXRaw, inputYRaw;
    public bool isLeftMousePress, isRightMousePress;
    public bool isJumpPress;
    public bool isJumpHold;
    public bool isDashPress;
    public bool isInteractPress;
    public bool isQPress, isTabPress, isTPress, isFPress, isRPress, isCtrlPress;
    void Awake()
    {
        instance = this;
    }
    void Update()
    {
        if (!canInput)
        {
            inputX = 0;
            return;
        }

        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
        inputXRaw = Input.GetAxisRaw("Horizontal");
        inputYRaw = Input.GetAxisRaw("Vertical");

        isLeftMousePress = Input.GetMouseButtonDown(0);
        isRightMousePress = Input.GetMouseButtonDown(1);

        isJumpPress = Input.GetKeyDown(KeyCode.Space);
        isJumpHold = Input.GetKey(KeyCode.Space);
        isDashPress = Input.GetKeyDown(KeyCode.LeftShift);
        isInteractPress = Input.GetKeyDown(KeyCode.E);

        isCtrlPress = Input.GetKeyDown(KeyCode.LeftControl);
        isQPress = Input.GetKeyDown(KeyCode.Q);
        isRPress = Input.GetKeyDown(KeyCode.R);
        isTPress = Input.GetKeyDown(KeyCode.T);
        isFPress = Input.GetKeyDown(KeyCode.F);
        isTabPress = Input.GetKeyDown(KeyCode.Tab);
    }
}
