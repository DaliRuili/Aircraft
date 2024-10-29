
using System;using System.Collections.Generic;

public static class EventHandler
{
    public static event Action UpdateScoreText;

    public static void CallUpdateScoreText()
    {
        UpdateScoreText?.Invoke();
    }
}




