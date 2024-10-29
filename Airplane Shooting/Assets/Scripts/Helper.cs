using System;
using UnityEngine.Events;
using UnityEngine.UI;


    public static class Helper
    {
        public static void AddOnClick(this Button self,UnityAction action)
        {
            self.onClick.RemoveAllListeners();
            self.onClick.AddListener(action);
        }
    }
