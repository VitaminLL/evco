using System;

namespace EVCO
{
	/*public class TournamentSelection : ISelection
	{
		IRandom _generator;

		public TournamentSelection ()
		{
			_generator = ClassFactory.GetRandomGenertor ();
		}

		public bool select (PopulationMember first, PopulationMember second)
		{
			int firstScore = first.calculateFitness ();
			int secondScore = second.calculateFitness ();

			if (firstScore > secondScore)
				return true;
			else if (secondScore > firstScore)
				return false;
			else {
				// Must be equal! Choose one at random
				return (_generator.next (0, 2) == 0);
			}
		}
	}*/

	// Outdated since changing IComparable
}

