using UnityEngine;
using UnityEditor;
using BuildingTools;
using Unity.Mathematics;

public class LevelBuilder : EditorWindow
{

    BuilderScriptableObject[] props;
    int columns = 4;
    float elementWidth = 100f;

    GameObject activeModel;

    Vector2 scrollPosition;



    Vector3 hitPositon = Vector3.zero;



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
                        activeModel = props[row * columns + column].Model;
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

    private bool PerformRaycast(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            hitPositon = hit.point;
            return true;
        }
        else
        {
            hitPositon = Vector3.left * 50;
            return false;
        }
    }



    private void OnSceneGUI(SceneView sceneView)
    {
        if (Tools.current != Tool.Custom) { return; }

        Event e = Event.current;

        if (e.type == EventType.MouseDown && e.button == 0)
        {
           Undo.RegisterCreatedObjectUndo (Instantiate(activeModel, hitPositon, quaternion.identity), "Created " + activeModel.name);

            e.Use();
        }

        if (e.type == EventType.Repaint)
        {
            Vector2 mousePosition = e.mousePosition;

            // Correct the mouse position
            //mousePosition.y = SceneView.currentDrawingSceneView.camera.pixelHeight - mousePosition.y;

            Ray ray = HandleUtility.GUIPointToWorldRay(mousePosition);
            PerformRaycast(ray);
        }

        int3 coords = BuildingUtils.PositionToTile(hitPositon);
        hitPositon = new Vector3(coords.x, coords.y, coords.z) * 10;

        // Visualize the ray in the Scene view
        Handles.color = Color.red;
        Handles.DrawWireCube(hitPositon + (Vector3.up * 5), Vector3.one * 10);

    }


    private void OnEnable()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }


}
