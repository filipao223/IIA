using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticIndividual : Individual {


	public GeneticIndividual(int[] topology) : base(topology) {
	}

	public override void Initialize () 
	{
		for (int i = 0; i < totalSize; i++) {
			genotype [i] = Random.Range (-1.0f, 1.0f);
		}
	}
		
	public override void Crossover (Individual partner, float probability)
	{
		int start = Random.Range(0,totalSize);
		int end = Random.Range(start,partner.Size);
		float number = Random.Range(0.0f, 1.0f);
		//float[] temp = partner.Getgenotype();
		
		if(number <= probability){
			float[] temp = partner.Getgenotype();
			for (int i = 0; i < totalSize; i++) {
				if(i >= start && i <= end){
					partner.Setgenotype(i,genotype[i]);
					genotype[i] = temp[i];
				}
			}
		}
		
	}

	public override void Mutate (float probability)
	{
		
		for (int i = 0; i < totalSize; i++) {
			float number = Random.Range(0.0f, 1.0f);
			if(number <= probability){
				genotype[i] = Random.Range(-1.0f, 1.0f);
			}
		}
	}

	public override Individual Clone ()
	{
		GeneticIndividual new_ind = new GeneticIndividual(this.topology);

		genotype.CopyTo (new_ind.genotype, 0);
		new_ind.fitness = this.Fitness;
		new_ind.evaluated = false;

		return new_ind;
	}

}
