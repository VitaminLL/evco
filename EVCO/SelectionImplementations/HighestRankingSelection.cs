using System;

namespace EVCO
{
	/// <summary>
	/// Selects the highest ranking population members
	/// If there are a lot the same, then some are chosen at random
	/// </summary>
	public class HighestRankingSelection : ISelection
	{
		IRandom _generator;
		const float PROPORTION_TO_PICK = 0.66f;

		public HighestRankingSelection ()
		{
			_generator = ClassFactory.GetRandomGenertor ();
		}

		public PopulationMember[] select (PopulationMember[] current)
		{
			//Console.WriteLine("Selecting fittest population members...");
			Array.Sort (current);

			int min = current[current.Length - 1].lastFitness;
			int knownMin = 1;
			for (int i = current.Length - 2; i>=0; i--)
			{
				if (current[i].lastFitness != min)
					break;
				else
					knownMin++;
			}

			int required = (int) Math.Round (PROPORTION_TO_PICK * current.Length, 0);
			int usable = current.Length - knownMin;
			int toAddAtRandom = required - usable;

			//Console.WriteLine ("Required: {0}, Usable: {1}, To Randomly Select: {2}", required, usable, toAddAtRandom);

			PopulationMember[] newPop = new PopulationMember[required];

			// Add the top n that aren't the lowest fitness in the group
			for (int i = 0; i < usable && i < required; i++)
			{
				newPop[i] = current[i];
			}

			//Console.WriteLine ("Added usable members");

			// Randomly choose the remaining from the lowest fitness population members
			for (int i = usable; i < usable + toAddAtRandom; i++)
			{
				int n = _generator.next (usable, current.Length);
				newPop[i] = current[n];
			}

			//Console.WriteLine ("Added random members");

			return newPop;
		}
	}
}

