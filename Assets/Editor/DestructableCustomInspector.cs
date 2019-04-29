using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
[CustomEditor(typeof(Destructable))]
public class RandomScript_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawDefaultInspector(); // for other non-HideInInspector fields
        Destructable script = (Destructable)target;

        // draw checkbox for the bool
        script.isRewarded = EditorGUILayout.Toggle("isRewarded", script.isRewarded);
        if (script.isRewarded) // if bool is true, show other fields
        {
            // script.iField = EditorGUILayout.ObjectField("I Field", script.iField, typeof(InputField), true) as InputField;
            //script.Template = EditorGUILayout.ObjectField("Template", script.Template, typeof(GameObject), true) as GameObject;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("rewards"), true);
            EditorGUILayout.
            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif
