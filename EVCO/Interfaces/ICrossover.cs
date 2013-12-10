using System;

namespace EVCO
{
	public interface ICrossover
	{
		PopulationMember crossover(PopulationMember first, PopulationMember second);
	}
}

