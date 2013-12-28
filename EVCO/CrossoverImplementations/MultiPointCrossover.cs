using System;


namespace EVCO
{
	public class MultiPointCrossover : ICrossover
	{
		IRandom _generator;

		public MultiPointCrossover ()
		{
			_generator = ClassFactory.GetRandomGenertor ();
		}

		public PopulationMember crossover (PopulationMember first, PopulationMember second)
		{
			int numberOfCrossoverPoints = _generator.next (1, Math.Min (first.parameters.Length, second.parameters.Length) / 16);
			int[] crossoverPoints = new int[numberOfCrossoverPoints];

			for (int i = 0; i < crossoverPoints.Length; i++) {
				do {
					crossoverPoints [i] = _generator.next (0, Math.Min (first.parameters.Length, second.parameters.Length));
				} while (crossoverPoints[i] % 4 != 0);

			}

			Array.Sort (crossoverPoints);

			bool workingOnFirst = (_generator.next (0, 2) == 1);
			int[] newParameters = new int[Math.Max (first.parameters.Length, second.parameters.Length)];
			int crossPtr = 0;

			for (int i = 0; i < newParameters.Length; i++) {
				if (crossoverPoints [crossPtr] == i)
					workingOnFirst = !workingOnFirst;

				// Swap if we get to the end of the shorter one
				if (workingOnFirst && i == first.parameters.Length)
					workingOnFirst = false;
				else if (!workingOnFirst && i == second.parameters.Length)
					workingOnFirst = true;

				if (workingOnFirst)
					newParameters [i] = first.parameters [i];
				else
					newParameters [i] = second.parameters [i];
			}

			return (new PopulationMember { parameters = newParameters});

		}
	}
}

