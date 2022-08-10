using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlInst : Singleton<ControlInst>
{
    public Controls Control;
    void Awake()
    {
        Control = new Controls();
    }
    private void OnEnable()
    {
        Control.Enable();
    }
    private void OnDisable()
    {
        Control.Disable();
    }
}
