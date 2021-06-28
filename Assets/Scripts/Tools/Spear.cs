using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : Tool
{
    [SerializeField] private float attackRange;
    [SerializeField] private float attackSpeed;

    private void OnEnable()
    {
        OnUse += Thrust;
    }

    private void Thrust()
    {

    }

    private void OnDisable()
    {
        OnUse -= Thrust;
    }
}
