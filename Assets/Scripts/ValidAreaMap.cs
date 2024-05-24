using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ValidAreaMap : MonoBehaviour
{
   [SerializeField] GameObject north, south, east, west;
    public void SetShape(Vector3 pos, bool n, bool s, bool e, bool w)
    {
        this.transform.position = pos;
        north.SetActive(n);
        south.SetActive(s);
        west.SetActive(w);
        east.SetActive(e);
    }
}
