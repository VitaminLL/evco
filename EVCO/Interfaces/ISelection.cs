using System;

namespace EVCO
{
	public interface ISelection
	{
		/// <summary>
		/// Returns the set of population members that should be carried forwards into the next iteration
		/// </summary>
		/// <param name='current'>
		/// The current population
		/// </param>
		PopulationMember[] select(PopulationMember[] current);
	}
}

