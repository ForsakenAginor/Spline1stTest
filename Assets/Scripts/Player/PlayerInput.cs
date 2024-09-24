using UnityEngine;

public class PlayerInput : IInput
{
    public float GetX()
    {
#if UNITY_EDITOR
        return Input.mousePosition.x;
#endif
        if (Input.touchCount > 0)
            return Input.GetTouch(0).position.x;
        else
            return 0f;
    }
}
