using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    [HideInInspector] public bool isPrimary = false;

    protected event Action OnUse = null;

    private void Update()
    {
        if (isPrimary)
        {

        }
        else
        {

        }
    }
}
