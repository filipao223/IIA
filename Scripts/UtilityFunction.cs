using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DeepCopyExtensions;

public class UtilityFunction
{

    public float evaluate(State s)
    {
        /////////////////
        // You should implement these
        /////////////////

        List<Unit> theirUnits = s.AdversaryUnits;
        float theirHealth = 0.0F;
        foreach (Unit unit in theirUnits)
        {
            theirHealth += unit.hp;
        }

        if (theirHealth == 0)
        {
            return Int32.MaxValue;
        }

        return 0;
    }
}
