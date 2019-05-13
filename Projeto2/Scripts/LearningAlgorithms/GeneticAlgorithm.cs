using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticAlgorithm : MetaHeuristic {
	public float mutationProbability;
	public float crossoverProbability;
	public int tournamentSize;
	public bool elitist;

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
	public override void Step() {
		//You should implement the code runs in each generation here
		throw new System.NotImplementedException ();
	}

}
