using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeBase : MonoBehaviour
{
    public NodeBase connection {get; private set;}
    public float G {get; private set;}
    public float H {get; private set;}
    public float F => G + H;

    public void SetConnection( NodeBase nodeBase) => connection = nodeBase;

    public void SetG(float g) => G = g;

    public void SetH(float h) => H = h;
}
