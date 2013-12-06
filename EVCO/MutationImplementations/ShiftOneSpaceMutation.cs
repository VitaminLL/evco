using System;

namespace EVCO
{
	public class ShiftOneSpaceMutation : IMutation
	{
		IRandom _generator;

		// Set this to 10 for prob=0.1, 100 for prob=0.01 etc.
		const int MUTATION_PROBABILITY_INVERSE = 10;

		public ShiftOneSpaceMutation ()
		{
			_generator = ClassFactory.GetRandomGenertor ();
		}

		PopulationMember mutate(PopulationMember member)
		{

		}
	}
}

