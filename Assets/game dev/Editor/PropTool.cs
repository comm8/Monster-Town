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
        
        //do raycast
        //move cursor to raycast
        
        //
        //on click
        //if in valid zone then create object at tile position
        //register tile

    }

    
}

