using UnityEngine;
using System.Collections;
using System;

public const int MaxValue = 2147483647;

public class UtilityFunction
{

    public float evaluate(State s)
    {
        /////////////////
        // You should implement these
        /////////////////

        List<Unit> theirUnits = s.AdversaryUnits;
        float theirHealth = 0.0;
        foreach(Unit unit in theirUnits){
            theirHealth += unit.hp;
        }

        if(theirHealth == 0){
            return MaxValue;
        }

        return 0;
    }
}
