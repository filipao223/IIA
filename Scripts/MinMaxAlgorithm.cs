using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DeepCopyExtensions;

public class MinMaxAlgorithm: MoveMaker
{
    public EvaluationFunction evaluator;
    private UtilityFunction utilityfunc; 
    public int depth = 3;
    private PlayerController MaxPlayer;
    private PlayerController MinPlayer;
    
    public MinMaxAlgorithm(PlayerController MaxPlayer, EvaluationFunction eval, UtilityFunction utilf, PlayerController MinPlayer)
    {
        this.MaxPlayer = MaxPlayer;
        this.MinPlayer = MinPlayer;
        this.evaluator = eval;
        this.utilityfunc = utilf;
    }

    public override State MakeMove()
    {
        // The move is decided by the selected state
        return GenerateNewState(); 
    }

    private State GenerateNewState()
    {
        // Creates initial state
        State newState = new State(this.MaxPlayer, this.MinPlayer);
        // Call the MinMax implementation
        State bestMove = MinMax(newState, depth, true,Int32.MinValue,Int32.MaxValue);
        // returning the actual state. You should modify this
        return bestMove;
    }

    public State MinMax(State currentState, int depth, bool maximizingPlayer,int alpha,int beta)
    {
        /////////////////////////////
        // You should implement this
        /////////////////////////////

        State bestState = null;

        int maxEval = Int32.MinValue;
        //Generate all posible states
        List<State> allPossibleStates = GeneratePossibleStates(currentState);
        //Iterate over all states and evalue them
        foreach (State newState in allPossibleStates)
        {
            int eval = MinMaxValue(newState, depth - 1, true,alpha,beta);
            //If depth is original depth, save the current maximum value state
            if (eval >= maxEval) bestState = newState;
            maxEval = Math.Max(maxEval, eval);
        }

        return bestState;
    }


    public int MinMaxValue(State currentState, int depth, bool maximizingPlayer,int alpha,int beta)
    {

        if(utilityfunc.evaluate(currentState) == Int32.MaxValue){
            return Int32.MaxValue;
        }

        if(utilityfunc.evaluate(currentState) == Int32.MinValue){
             return Int32.MinValue;
        }

        //If depth is 0 or game has ended
        //Utility function returns int32.MaxValue if game has ended and 0 otherwise
        if (depth == 0)
        {
            //Evaluate current state
            return (int) evaluator.evaluate(currentState);
        }

        if (maximizingPlayer)
        {
            int maxEval = Int32.MinValue;
            for(int i = 0;i <2;i++){
                //Generate all posible states
                List<State> allPossibleStates = GeneratePossibleStates(currentState);
                //Iterate over all states and evalue them
                foreach (State newState in allPossibleStates)
                {
                    State inv = newState.DeepCopyByExpressionTree();
                    State aux = new State(inv);
                    int eval = MinMaxValue(newState, depth - 1, false,alpha,beta);
                    //If depth is original depth, save the current maximum value state
                    maxEval = Math.Max(maxEval, eval);
                    alpha = Math.Max(alpha,maxEval);

                    if(beta <= alpha){
                        break;
                    }
                }
            }
            return maxEval;
        }
        else
        {
            int minEval = Int32.MaxValue;
            for(int i = 0;i <2;i++){
                //Generate all posible states
                List<State> allPossibleStates = GeneratePossibleStates(currentState);
                //Iterate over all states and evalue them
                foreach (State newState in allPossibleStates)
                {
                    State inv = newState.DeepCopyByExpressionTree();
                    State aux = new State(inv);
                    int eval = MinMaxValue(newState, depth - 1, true,alpha,beta);
                    minEval = Math.Min(minEval, eval);

                    beta = Math.Min(beta,minEval);

                    if(beta <= alpha){
                        break;
                    }
                }
            }
            return minEval;
        }
    }


    private List<State> GeneratePossibleStates(State state)
    {
        List<State> states = new List<State>();
        //Generate the possible states available to expand
        foreach(Unit currentUnit in state.PlayersUnits)
        {
            // Attack states
            List<Unit> attackOptions = currentUnit.GetAttackable(state, state.AdversaryUnits);
            foreach (Unit t in attackOptions)
            {
                State newState = new State(state, currentUnit, false);
                newState = AttackUnit(newState, t);
                states.Add(newState);
            }
            // Movement States
            List<Tile> neighbours = currentUnit.GetFreeNeighbours(state);
            foreach (Tile t in neighbours)
            {
                State newState = new State(state, currentUnit, true);
                newState = MoveUnit(newState, t);
                states.Add(newState);
            }

        }

        // YOU SHOULD NOT REMOVE THIS
        // Counts the number of expanded nodes;
        this.MaxPlayer.ExpandedNodes += states.Count;
        //

        return states;
    }

    private State MoveUnit(State state,  Tile destination)
    {
        Unit currentUnit = state.unitToPermormAction;
        //First: Update Board
        state.board[(int)destination.gridPosition.x, (int)destination.gridPosition.y] = currentUnit;
        state.board[currentUnit.x, currentUnit.y] = null;
        //Second: Update Players Unit Position
        currentUnit.x = (int)destination.gridPosition.x;
        currentUnit.y = (int)destination.gridPosition.y;
        state.isMove = true;
        state.isAttack = false;
        return state;
    }

    private State AttackUnit(State state, Unit toAttack)
    {
        Unit currentUnit = state.unitToPermormAction;
        Unit attacked = toAttack.DeepCopyByExpressionTree();

        Tuple<float, float> currentUnitBonus = currentUnit.GetBonus(state.board, state.PlayersUnits);
        Tuple<float, float> attackedUnitBonus = attacked.GetBonus(state.board, state.AdversaryUnits);


        attacked.hp += Math.Min(0, (attackedUnitBonus.Item1)) - (currentUnitBonus.Item2 + currentUnit.attack);
        state.unitAttacked = attacked;

        state.board[attacked.x, attacked.y] = attacked;
        int index = state.AdversaryUnits.IndexOf(attacked);
        state.AdversaryUnits[index] = attacked;



        if (attacked.hp <= 0)
        {
            //Board update by killing the unit!
            state.board[attacked.x, attacked.y] = null;
            index = state.AdversaryUnits.IndexOf(attacked);
            state.AdversaryUnits.RemoveAt(index);

        }
        state.isMove = false;
        state.isAttack = true;

        return state;

    }
}
