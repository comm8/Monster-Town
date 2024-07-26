using UnityEngine;
public static class GridGizmo
{
    private static bool enabled;

    public static void DrawGrid(float spacing, int gridsize)
    {
        Gizmos.color = Color.yellow;
        for (int x = 0; x < gridsize; x++)
        {

            for (int y = 0; y < gridsize; y++)
            {
                Vector3 offset = new Vector3(x * spacing, y* spacing);
                Gizmos.DrawLine((Vector3.right * spacing * (gridsize / 2)) + offset, (Vector3.left * spacing * (gridsize / 2)) + offset);
            }

        }

        for (int y = 0; y < gridsize; y++)
        {

        }

        for (int z = 0; z < gridsize; z++)
        {

        }
    }
}
