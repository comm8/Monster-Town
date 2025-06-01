using UnityEngine;

public class TextureArrayCycler : MonoBehaviour
{
    [SerializeField] Material material;

    [SerializeField] int increment;
    [SerializeField] int ArrayLength = 64; // Number of textures in array
    int currentIndex = 0;

    void Update()
    {
        currentIndex = (currentIndex + increment) % ArrayLength;
        material.SetFloat("_SliceIndex", currentIndex);
    }
}
