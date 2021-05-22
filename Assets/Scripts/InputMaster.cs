using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMaster : MonoBehaviour
{
    #region SIGLETON
    private static InputMaster _instance;
    public static InputMaster Main
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<InputMaster>();

                if(_instance == null)
                {
                    GameObject container = new GameObject("Action Archive");
                    _instance = container.AddComponent<InputMaster>();
                }
            }              

            return _instance;
        } 
    }
    #endregion  


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
