using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.EditorTools;
using UnityEditor.PackageManager.UI;
using UnityEditor;


[EditorTool("PropTool")]
public class PropTool : EditorTool
{

    public override GUIContent toolbarIcon => EditorGUIUtility.IconContent("d_PlatformEffector2D Icon");
    public override void OnToolGUI(EditorWindow window)
    {
        if (window is not SceneView)
        {
            return;
        }

        Handles.RectangleHandleCap( 0,
                new Vector3(3f, 0f, 0f),
                Quaternion.LookRotation(Vector3.right),
                10,
                EventType.Repaint );


    }
}

