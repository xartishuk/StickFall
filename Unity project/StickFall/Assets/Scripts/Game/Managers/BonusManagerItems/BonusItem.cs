using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusItem : BaseObject
{
    public static event System.Action<BonusItem> OnPickedBonus;
    private int value;
    public int Value
    {
        get
        {
            return value;
        }

        private set
        {
            this.value = value;
        }
    }

    public void Init(int value)
    {
        this.value = value;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PickBonus()
    {
        if(OnPickedBonus != null)
        {
            OnPickedBonus(this);
        }
    }
}
