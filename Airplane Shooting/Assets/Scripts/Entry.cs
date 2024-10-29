using System;
using UnityEngine;

public class Entry : MonoBehaviour
{
    private void Start()
    {
        GameMgr.Instance.Run();
    }


    private void Update()
    {
        GameMgr.Instance.OnUpdate();
    }
}