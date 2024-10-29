using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceMgr
{
    private static ResourceMgr instance;
    public static ResourceMgr Instance
    {
        get
        {
            if (instance != null) return instance;
            instance = new ResourceMgr();
            return instance;
        }
    }

    private Dictionary<string, GameObject> m_res = new Dictionary<string, GameObject>();

    
    /// <summary>
    /// 加载资源
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public GameObject LoadRes(string path)
    {
        if (m_res.TryGetValue(path, out var res))
        {
            return res;
        }
        var obj = Resources.Load<GameObject>(path);
        m_res.Add(path,obj);
        return obj;
    }
}
