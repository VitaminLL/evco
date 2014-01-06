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

		public PopulationMember crossover (PopulationMember[] parent)
		{
			int minLength = Int32.MaxValue;
			int maxLength = 0;

			for (int i = 0; i < parent.Length; i++) {
				if (parent [i].parameters.Length < minLength)
					minLength = parent [i].parameters.Length;

				if (parent [i].parameters.Length > maxLength) {
					maxLength = parent [i].parameters.Length;
				}
			}

			int numberOfCrossoverPoints = _generator.next (1, minLength / 16);
			int[] crossoverPoints = new int[numberOfCrossoverPoints];

			for (int i = 0; i < crossoverPoints.Length; i++) {
				do {
					crossoverPoints [i] = _generator.next (0, minLength);
				} while (crossoverPoints[i] % 4 != 0);

			}

			Array.Sort (crossoverPoints);

			int[] newParameters = new int[maxLength];
			int crossPtr = 0;
			int workingOn = _generator.next (0, parent.Length);
			for (int i = 0; i < newParameters.Length; i++) {
				if (crossoverPoints [crossPtr] == i || parent [workingOn].parameters.Length == i) {
					do {
						workingOn = _generator.next (0, parent.Length);
					} while (parent [workingOn].parameters.Length <= i);

					if (crossoverPoints [crossPtr] == i && crossPtr + 1 < crossoverPoints.Length)
						crossPtr++;
				}
					newParameters [i] = parent [workingOn].parameters [i];
				
			}

			return (new PopulationMember { parameters = newParameters});

		}
	}
}

