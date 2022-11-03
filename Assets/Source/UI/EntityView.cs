using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthBarUI))]
public class EntityView : MonoBehaviour
{
    private HealthBarUI _healthBarUI;

    private void Awake()
    {
        _healthBarUI = GetComponent<HealthBarUI>();
    }
}
