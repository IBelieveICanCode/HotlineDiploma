using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(GameMapGenerator))]
public class MapEditor : Editor
{

    public override void OnInspectorGUI()
    {

        GameMapGenerator map = target as GameMapGenerator;

        if (DrawDefaultInspector())
        {
            map.GenerateMap();
        }

        if (GUILayout.Button("Generate Map"))
        {
            map.GenerateMap();
        }


    }

}