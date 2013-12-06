using System;

namespace EVCO
{
	public interface ICrossover
	{
		void crossover(ref PopulationMember first, ref PopulationMember second);
	}
}

