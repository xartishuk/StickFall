using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CommonStats {

    public static event System.Action OnMoneyValueChanged;

    private const string SCORE_KEY = "SCORE_KEY";
    private const string MONEY_KEY = "MONEY_KEY";

    public static int Score
    {
        get
        {
            return CustomPlayerPrefs.GetInt(SCORE_KEY, 0);
        }

        set
        {
            if (Score != value)
            {
                CustomPlayerPrefs.SetInt(SCORE_KEY, value);
            }
        }
    }

    public static int Money
    {
        get
        {
            return CustomPlayerPrefs.GetInt(MONEY_KEY, 0);
        }

        set
        {
            if (Score != value)
            {
                CustomPlayerPrefs.SetInt(MONEY_KEY, value);
                if (OnMoneyValueChanged != null)
                {
                    OnMoneyValueChanged();
                }
            }
        }
    }



}
