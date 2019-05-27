using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticAlgorithm : MetaHeuristic {
	public float mutationProbability;
	public float crossoverProbability;
	public int tournamentSize;
	public bool elitist;
    public int elite_num;

	public override void InitPopulation () {
		population = new List<Individual> ();
		// jncor 
		while (population.Count < populationSize) {
			GeneticIndividual new_ind = new GeneticIndividual(topology);
			new_ind.Initialize ();
			population.Add (new_ind);
		}
	}

    //The Step function assumes that the fitness values of all the individuals in the population have been calculated.
    //The Step function assumes that the fitness values of all the individuals in the population have been calculated.
    public override void Step()
    {
        List<Individual> new_pop = new List<Individual>();

        updateReport(); //called to get some stats
                        // fills the rest with mutations of the best !
        
        //Keep some of the old best individuals
        if (elitist)
        {
            int current_elite = 0;
            while (current_elite < elite_num)
            {
                if (new_pop.Count >= populationSize) break;
                
                //Add best individual
                GeneticIndividual best = (GeneticIndividual)overallBest.Clone();
                new_pop.Add(best);
                current_elite++;

                //Remove from old population
                population.Remove(best);
            }
        }

        while (true)
        {
            //Select population
            GeneticSelection selection = new GeneticSelection();
            List<Individual> best_tournament_ind = selection.selectIndividuals(population, tournamentSize);

            //Crossover
            best_tournament_ind[0].Crossover(best_tournament_ind[1], crossoverProbability);

            //Mutation
            best_tournament_ind[0].Mutate(mutationProbability);
            best_tournament_ind[1].Mutate(mutationProbability);

            new_pop.Add(best_tournament_ind[0]);
            if (new_pop.Count >= populationSize) break;

            new_pop.Add(best_tournament_ind[1]);
            if (new_pop.Count >= populationSize) break;
        }

        population = new_pop;

        generation++;
    }

}
