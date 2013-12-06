using System;
using System.Collections.Generic;

namespace EVCO
{
	public class PopulationController
	{
		private PopulationMember[] members;

		ICrossover crosser;
		IMutation mutator;
		ISelection selector;

		IRandom _generator;

		public PopulationController ()
		{
			members = new PopulationMember[0];

			crosser = new OnePointCrossover();
			mutator = new ShiftOneSpaceMutation();
			selector = new TournamentSelection();

			_generator = ClassFactory.GetRandomGenertor ();
		}

		public void InitialisePopulation(string[] args)
		{
			if (args.Length > 0) {
				Console.Write ("Loading Initial Population... ");
				members = Serializer.DeserializePopulation (new StreamReader (args [0]));
				Console.WriteLine ("Done");
			} else {
				const int INITIAL_POPULATION_SIZE = 4000;
				Console.Write ("Generating Initial Population... (0/" + INITIAL_POPULATION_SIZE + ")");
				members = new PopulationMember[INITIAL_POPULATION_SIZE];
				for (int i = 0; i < members.Length; i++) {
					do {
						members [i] = new PopulationMember ();
					} while (members[i].calculateFitness () == 0);

					Console.SetCursorPosition (0, 0);
					Console.Write ("Generating Initial Population... (" + i + "/" + INITIAL_POPULATION_SIZE + ")");
				}
				Console.SetCursorPosition (0, 0);
				Console.WriteLine ("Generating Initial Population... Completed");
				Console.Write ("Saving Population... ");
				Serializer.SerializePopulation (members, new StreamWriter ("LASTPOP.TXT", false));
				Console.WriteLine ("Done");
			}
		}

		private PopulationMember[] selectForCrossover (int count)
		{
			List<PopulationMember> chosen = new List<PopulationMember> (count);
			for (int i = 0; i < count; i++) {
				PopulationMember first = members [_generator.next (0, members.Length)];
				PopulationMember second = members [_generator.next (0, members.Length)];

				chosen.Add (selector.select (first, second) ? first : second);
			}

			return chosen.ToArray ();
		}
	}
}

