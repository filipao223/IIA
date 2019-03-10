using UnityEngine;
using System.Collections;
using System;

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
        float ourHealth = 0.0;
        foreach (Unit unit in ourUnits){
            ourHealth += unit.hp;
        }

        //Check adversary total health
        List<Unit> theirUnits = s.AdversaryUnits;
        float theirHealth = 0.0;
        foreach(Unit unit in theirUnits){
            theirHealth += unit.hp;
        }

        float healthScore = ourHealth - theirHealth;

        //Check if we are in attack range of an oponent
        foreach (Unit unit in ourUnits)
        {
            //Code copied from Unit class, GetAttackable method
            for (int i = 0; i < unit.attackrange.GetLength(0); i++)
            {
                int checkX = unit.x + attackrange[i, 0];
                int checkY = unit.y + attackrange[i, 1];
                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    Unit neighbour = state.board[checkX, checkY];
                    if (neighbour != null && adversary.Contains(neighbour) && !neighbour.IsDead())
                    {
                        //Found an oponent
                        //Change value in some way
                        //Possibly save which unit has oponents
                    }
                }
            }
        }

        return 0;
    }
}
