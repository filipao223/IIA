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
        /************************************************
         * Complexidade:
         *  Loop 1: O(n) * 2*O(k)
         *  Loop 2: O(n)
         *  Total = O(n) * 2*O(k) + O(n) => O(n) (linear)
         *
         *************************************************/

        //Check our total health
        List<Unit> ourUnits = s.PlayersUnits;
        float ourHealth = 0.0F;
        float theirHealth = 0.0F;
        foreach (Unit unit in ourUnits)  // O(n)
        {
            if(unit is Assassin){
                ourHealth += 0.9f*unit.hp;
            }
            if(unit is Mage){
                ourHealth += 0.7f*unit.hp;
            }
            if(unit is Protector || unit is Warrior){
                ourHealth += 0.5f*unit.hp;
            }
            Tuple<float, float> tuple = unit.GetBonus(s.board,ourUnits); //O(k) <- No maximo, so tem 6 casas onde pode verificar, constante (não depende do numero de unidades (n))
            //verifica se está perto de um warrior ou protector e valoriza isso
            if(tuple.Item1 != 0){
                if(unit is Assassin){
                    ourHealth += 0.2f*tuple.Item1;
                }
                if(unit is Mage){
                    ourHealth += 0.3f*tuple.Item1;
                }
                if(unit is Protector || unit is Warrior){
                    ourHealth += 0.4f*tuple.Item1;
                }
            }
            if(tuple.Item2 != 0){
                if(unit is Assassin){
                    ourHealth += 0.2f*tuple.Item1;
                }
                if(unit is Mage){
                    ourHealth += 0.3f*tuple.Item1;
                }
                if(unit is Protector || unit is Warrior){
                    ourHealth += 0.4f*tuple.Item1;
                }
            }

            List<Unit> attackable = new List<Unit>();

            attackable = unit.GetAttackable(); //O(k) <- No maximo, so tem 6 casas onde pode atacar, constante (não depende do numero de unidades (n))

            if(attackable.Count != 0){
                       foreach(Unit u in attackable){ //O(k) <- No maximo, so tem 6 casas onde pode atacar, constante (não depende do numero de unidades (n))
                           if((unit.attack + unit.attackbonus) >= 2f*(u.attack+u.attackbonus)){
                               theirHealth -= (unit.attack + unit.attackbonus);
                           }
                           if(ourUnits.Count > attackable.Count && (unit.hp + unit.hpbonus)>(u.attack+u.attackbonus)){
                               theirHealth -= (unit.attack + unit.attackbonus);
                           }
                       }
            }

        }

        //Check adversary total health
        List<Unit> theirUnits = s.AdversaryUnits;
        foreach (Unit unit in theirUnits) //O(n)
        {
            if(unit is Assassin){
                theirHealth += 0.9f*unit.hp;
            }
            if(unit is Mage){
                theirHealth += 0.7f*unit.hp;
            }
            if(unit is Protector || unit is Warrior){
                theirHealth += 0.5f*unit.hp;
            }

        }

        float healthScore = ourHealth - theirHealth;

        return healthScore;
    }
}
