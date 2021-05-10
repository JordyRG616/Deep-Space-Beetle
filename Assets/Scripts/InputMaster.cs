using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMaster : MonoBehaviour
{
    public event EventHandler OnLeftMousePressed;
    public event EventHandler OnRightMousePressed;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            OnLeftMousePressed?.Invoke(this, EventArgs.Empty);
        }

        if(Input.GetMouseButtonDown(1))
        {
            OnRightMousePressed?.Invoke(this, EventArgs.Empty);
        }
    }
}
