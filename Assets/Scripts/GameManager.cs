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
            Debug.Log("CURRENT UI" + MonstroUtil.PositionToTile(hit.point));

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

