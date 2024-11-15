using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridMapController))]
public class GridMapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GridMapController gridMapController = (GridMapController)target;
        if (GUILayout.Button("Generate Map"))
        {
            gridMapController.GenerateMap();
        }
    }
}
