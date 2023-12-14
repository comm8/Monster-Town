using UnityEngine;
using Unity.Entities;

namespace MonstroCity
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        [Header("Tile Utilities")]

        [SerializeField] Transform cameraTransform;

        [Header("Day Night Cycle")]

        [SerializeField] Transform sunTransform, moonTransform;
        [SerializeField] float speedMultiplier;


        [SerializeField] Gradient SunColor, moonColor;

        [Header("Selection")]

        [SerializeField] Transform Selection;
        [SerializeField] Material material;


        //
        void Update()
        {

            GetCurrentTile();
            UpdateDayNightCycle();
        }

        void GetCurrentTile()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            Selection.position = new Vector3(MonstroUtil.PositionToTile(hit.point).x * 10, 0, MonstroUtil.PositionToTile(hit.point).y * 10);
            var tempe = hit.point - Selection.position;
            material.SetVector("_Top_Offset", tempe);

            var EM = World.DefaultGameObjectInjectionWorld.EntityManager;
            //Somehow get the the corresponding Tile from ECS
        }

        void UpdateDayNightCycle()
        {
            float deltaTime = Time.deltaTime;

            sunTransform.Rotate(new Vector3(speedMultiplier, 0, 0) * deltaTime);
            moonTransform.Rotate(new Vector3(speedMultiplier, 0, 0) * deltaTime);

        }

    }
}

