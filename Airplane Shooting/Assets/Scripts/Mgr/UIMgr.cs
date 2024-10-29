using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIMgr
{
    private static UIMgr instance;

    public static UIMgr Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }

            instance = new UIMgr();
            return instance;
        }
    }

    private Transform m_canvas;
    private Dictionary<string, GameObject> m_panels = new Dictionary<string, GameObject>();
    
    public void ShowUI(string name)
    {
        m_canvas = GameObject.Find("Canvas").transform;
        if (m_panels.TryGetValue(name, out var panel))
        {
            panel.transform.SetParent(m_canvas);
            panel.SetActive(true);
            return;
        }
        var path = "Panel/" + name;
        var prefab = ResourceMgr.Instance.LoadRes(path);
        var obj = Object.Instantiate(prefab, m_canvas, false);
        obj.SetActive(true);
        m_panels.Add(name,obj);
    }
    public void HideUI(string name)
    {
        if (!m_panels.TryGetValue(name, out var panel)) return;
        panel.SetActive(false);
        m_panels.Remove(name);
        Object.Destroy(panel);
    }
}
