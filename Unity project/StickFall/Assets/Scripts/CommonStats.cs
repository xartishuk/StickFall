using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CommonStats {

    private const string SCORE_KEY = "SCORE_KEY";

    public static int Score
    {
        get
        {
            return CustomPlayerPrefs.GetInt(SCORE_KEY, 0);
        }
    }



}
