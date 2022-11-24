using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

[CustomEditor(typeof(Test))]
public class TestGUI : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Test test = (Test)target;

        if (GUILayout.Button("Deal damage"))
        {
            test.DealDamage();
        }

        if (GUILayout.Button("Heal"))
        {
            test.Heal();
        }

        if (GUILayout.Button("Up level"))
        {
            test.UpLevel();
        }

        if (GUILayout.Button("Restore health"))
        {
            test.Restore();
        }
    }
}
