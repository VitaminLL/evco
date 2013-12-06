using System;

namespace EVCO
{
	public interface ICrossover
	{
		void crossover(out PopulationMember first, out PopulationMember second);
	}
}

