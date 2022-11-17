using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareTest : MonoBehaviour
{
    void Update()
    {
        Debugger.UpdateText("square world", Camera.main.ScreenToWorldPoint(transform.position));
        Debugger.UpdateText("square viewport", Camera.main.ScreenToViewportPoint(transform.position));
        Debugger.UpdateText("square", transform.position);
    }
}
