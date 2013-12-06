using System;

namespace EVCO
{
	public interface ISelection
	{
		/// <summary>
		/// Decides which of the two to select
		/// </summary>
		/// <param name='first'>
		/// The first member to evaluate
		/// </param>
		/// <param name='second'>
		/// The second member to evaluate
		/// </param>
		/// <returns>True if the first is chosen, false if the second</returns>
		bool select(PopulationMember first, PopulationMember second);
	}
}

