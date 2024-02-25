using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderInitMono : MonoBehaviour
{
    private void Awake()
    {
        var gridSize = GameManager.instance.gridSize;
        transform.localScale = 10 * gridSize * Vector3.one;
        transform.position = new Vector3(gridSize / 2, 0, gridSize / 2) * 10 - new Vector3(5, 0, 5);
    }
}
