using TMPro;
using UnityEngine;

public class DebuggerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textInfo;

    private void Update()
    {
        _textInfo.text = Debugger.WholeTextInfo;
    }
}
