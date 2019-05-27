using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GeneticSelection : SelectionMethod {

	public GeneticSelection() {

	}

	//override on each specific selection class
	override public List<Individual> selectIndividuals (List<Individual> oldpop, int num)
    {
        System.Random random = new System.Random();

        List<Individual> tournament1 = new List<Individual>();
        List<Individual> tournament2 = new List<Individual>();

        int t1=0, t2 = 0;

        //Place individuals from oldpop into 2 tournaments
        //Check if an individual is already in the other tournament
        //Continue until both tournaments are full
        do
        {
            GeneticIndividual current = (GeneticIndividual)oldpop[random.Next(0, oldpop.Count)];
            if (!tournament2.Contains(current) && t1 < num)
            {
                tournament1.Add(current.Clone());
                t1++;
            }

            current = (GeneticIndividual)oldpop[random.Next(0, oldpop.Count)];
            if (!tournament1.Contains(current) && t2 < num)
            {
                tournament2.Add(current.Clone());
                t2++;
            }
        } while (t1 < num || t2 < num);



        //Sort the tournament individuals by fitness
        tournament1.Sort((x, y) => x.Fitness.CompareTo(y.Fitness));
        tournament2.Sort((x, y) => x.Fitness.CompareTo(y.Fitness));

        List<Individual> best_of_pop = new List<Individual>();

        //Place the best individual of each tournament
        best_of_pop.Add(tournament1[0]);
        best_of_pop.Add(tournament2[0]);

        return best_of_pop;
    }

}
