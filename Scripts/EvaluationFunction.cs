using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DeepCopyExtensions;

public class EvaluationFunction
{
    // Do the logic to evaluate the state of the game !
    public float evaluate(State s)
    {
        /////////////////
        // You should implement these
        /////////////////

        //Check our total health
        List<Unit> ourUnits = s.PlayersUnits;
        float ourHealth = 0.0F;
        foreach (Unit unit in ourUnits)
        {
            ourHealth += unit.hp;
        }

        //Check adversary total health
        List<Unit> theirUnits = s.AdversaryUnits;
        float theirHealth = 0.0F;
        foreach (Unit unit in theirUnits)
        {
            theirHealth += unit.hp;
        }

        float healthScore = ourHealth - theirHealth;

        /* int gridSizeX = s.board.GetLength(0);
        int gridSizeY = s.board.GetLength(1);

        //Check if we are in attack range of an oponent
        foreach (Unit unit in ourUnits)
        {
            //Code copied from Unit class, GetAttackable method
            for (int i = 0; i < unit.attackrange.GetLength(0); i++)
            {
                int checkX = unit.x + unit.attackrange[i, 0];
                int checkY = unit.y + unit.attackrange[i, 1];
                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    Unit neighbour = s.board[checkX, checkY];
                    if (neighbour != null && theirUnits.Contains(neighbour) && !neighbour.IsDead())
                    {
                        healthScore = healthScore * 5;
                    }
                }
            }
        }*/

        return healthScore;
    }
}
