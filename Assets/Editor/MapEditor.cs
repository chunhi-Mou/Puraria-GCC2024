using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GenerateMap))]
public class MapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GenerateMap generateMap = (GenerateMap)target;

        if (GUILayout.Button("Generate Map")) 
        {
            generateMap.CreateMap();
        }
    }
}
