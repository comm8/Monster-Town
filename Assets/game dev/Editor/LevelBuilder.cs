using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Xml.Linq;
using System;
using Unity.Mathematics;

public class LevelBuilder : EditorWindow
{

    BuilderScriptableObject[] props;
    int columns = 4;
    float elementWidth = 100f;



    Vector2 scrollPosition;



    [MenuItem("Game/LevelBuilder")]
    private static void OpenWindow()
    {
        var window = GetWindow<LevelBuilder>();
        //window._trackedresources = Extensions.Ge
    }


    void RefreshProps()
    {
        string[] guids = AssetDatabase.FindAssets("t:BuilderScriptableObject");
        Debug.Log("found " + guids.Length + " guids");
        props = new BuilderScriptableObject[guids.Length];

        for (int i = 0; i < guids.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            props[i] = AssetDatabase.LoadAssetAtPath<BuilderScriptableObject>(path);
        }


        Repaint();
    }

    void OnGUI()
    {
        DrawButtons();

        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        elementWidth = EditorGUILayout.FloatField("Element Width", elementWidth);



        GUILayout.Label("Palette", EditorStyles.boldLabel);

        columns = Mathf.Max(1, Mathf.FloorToInt(position.width / elementWidth));
        int rowCount = Mathf.CeilToInt((float)props.Length / columns);
        for (int row = 0; row < rowCount; row++)



        {
            GUILayout.BeginHorizontal();

            for (int column = 0; column < columns; column++)
            {
                if (row * columns + column < props.Length)
                {
                    if (GUILayout.Button(props[row * columns + column].Icon, GUILayout.Width(elementWidth), GUILayout.Height(elementWidth)))
                    {

                    }
                }
                else
                {
                    GUILayout.Label("", GUILayout.Width(elementWidth), GUILayout.Height(elementWidth));
                }
            }
            GUILayout.EndHorizontal();
        }

        GUILayout.EndScrollView();



    }


    void DrawButtons()
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Draw 3D Grid"))
        {
         
        }
        if (GUILayout.Button("Refresh Props"))
        {
            RefreshProps();
        }


        GUILayout.EndHorizontal();
    }





}
