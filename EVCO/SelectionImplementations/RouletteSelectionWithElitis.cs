using System;
using System.Collections.Generic;

namespace EVCO
{
	public class RouletteSelection : ISelection
	{
		IRandom _generator;
		const float PROPORTION_TO_PICK = 0.66f;

		public RouletteSelection ()
		{
			_generator = ClassFactory.GetRandomGenertor ();
		}

		public PopulationMember[] select (PopulationMember[] current)
		{
			List<int> distList = new List<int> ();

			for (int i = 0; i < current.Length; i++) {
				for (int x = 0; x < current [i].lastFitness; x++)
					distList.Add (i);
			}

			int toAdd = (int)Math.Round (current.Length * PROPORTION_TO_PICK, 0);

			PopulationMember[] pNew = new PopulationMember[toAdd];

			for (int i = 0; i < pNew.Length; i++) {
				pNew [i] = current [_generator.next (0, current.Length)];
			}

			return pNew;
		}
	}
}

