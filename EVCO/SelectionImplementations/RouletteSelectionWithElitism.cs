using System;
using System.Collections.Generic;

namespace EVCO
{
	public class RouletteSelectionWithElitism : ISelection
	{
		IRandom _generator;
		const float PROPORTION_TO_PICK = 0.66f;

		public RouletteSelectionWithElitism ()
		{
			_generator = ClassFactory.GetRandomGenertor ();
		}

		public PopulationMember[] select (PopulationMember[] current)
		{
			List<int> distList = new List<int> ();
			int added = 0;

			int toAdd = (int)Math.Round (current.Length * PROPORTION_TO_PICK, 0);
			PopulationMember[] pNew = new PopulationMember[toAdd];

			// Find the max fitness
			int fitness = 0;
			for (int x = 0; x < current.Length; x++) {
				if (current[x].lastFitness > fitness) {
					fitness = current[x].lastFitness;
					added = 0;
				}

				if (current [x].lastFitness == fitness) {
					pNew [added] = current [x];
					added++;
				}
			}


			for (int i = 0; i < current.Length; i++) {
				if (current [i].lastFitness != fitness) {
					for (int x = 0; x < current [i].lastFitness; x++)
						distList.Add (i);
				}
			}



			for (int i = added; i < pNew.Length; i++) {
				pNew [i] = current [distList[_generator.next (0, distList.Count)]];

			}

			return pNew;
		}
	}
}

