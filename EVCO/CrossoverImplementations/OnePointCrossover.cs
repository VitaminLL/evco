using System;
using System.Collections.Generic;

namespace EVCO
{
	public class OnePointCrossover : ICrossover
	{
		IRandom _generator;

		public OnePointCrossover ()
		{
			_generator = ClassFactory.GetRandomGenertor ();
		}

		public PopulationMember crossover (PopulationMember[] parent)
		{
			PopulationMember first = parent [0];
			PopulationMember second = parent [1];

			int shortest = first.parameters.Length > second.parameters.Length ? second.parameters.Length : first.parameters.Length;
			int crossoverPoint = _generator.next (4, shortest);

			while (crossoverPoint % 4 != 0)
				crossoverPoint--;

			List<int> firstList = new List<int> ();
			List<int> secondList = new List<int> ();

			for (int i = 0; i < crossoverPoint; i++) {
				firstList.Add (first.parameters [i]);
				secondList.Add (second.parameters [i]);
			}

			for (int i = crossoverPoint; i < second.parameters.Length; i++) {
				firstList.Add (second.parameters [i]);
			}

			for (int i = crossoverPoint; i < first.parameters.Length; i++) {
				secondList.Add (first.parameters [i]);
			}

			// Choose at random which to go with

			PopulationMember p = new PopulationMember();

			if (_generator.next (0,2) == 0)
				p.parameters = firstList.ToArray ();
			else
				p.parameters = secondList.ToArray ();

			return p;
		}
	}
}

