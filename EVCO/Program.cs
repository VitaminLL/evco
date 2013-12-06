using System;
using System.IO;

namespace EVCO
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			PopulationMember[] members = GetInitialPopulation (args);
		}

		public static PopulationMember[] GetInitialPopulation (string[] args)
		{
			PopulationMember[] members;

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

			return members;
		}

		public static void TestCrossover ()
		{
			// Code to test crossover function
			PopulationMember m = new PopulationMember ();
			m.parameters = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };

			PopulationMember p = new PopulationMember ();
			p.parameters = new int[] { 16, 15 , 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };

			Console.WriteLine (m.ToString ());
			Console.WriteLine (p.ToString ());

			// Set OnePointCrossover to the crossover function to test
			ICrossover crossover = new OnePointCrossover ();
			crossover.crossover (ref m, ref p);

			Console.WriteLine ();
			Console.WriteLine (m.ToString ());
			Console.WriteLine (p.ToString ());
		}

	}
}
