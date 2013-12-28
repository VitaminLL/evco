using System;
using System.IO;

namespace EVCO
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			SetupConfiguration (args);
			Configuration.PrintConfiguration ();

			PopulationController controller = new PopulationController ();
			controller.InitialisePopulation ();
			int round = 1;

			while (true) {
				//controller.ThreadedPrintPopFitness ();
				controller.PrintPopFitness ();
				controller.executeNextRound ();
				round++;
				if (round % 10 == 0)
					Console.WriteLine ("Generation " + round.ToString());
			}
		}

		public static void SetupConfiguration (string[] args)
		{
			foreach (string s in args) {
				try {
					string[] split = s.Split ('=');

					switch (split [0]) {
					case "CROSSOVER_OPERATOR":
						Configuration.CROSSOVER_OPERATOR_TYPE = split [1];
						break;
					case "SELECTION_OPERATOR":
						Configuration.SELECTION_OPERATOR_TYPE = split [1];
						break;
					case "MUTATION_OPERATOR":
						Configuration.MUTATION_OPERATOR_TYPE = split [1];
						break;
					case "RANDOM_GENERTOR":
						Configuration.RANDOM_GENERTOR_TYPE = split [1];
						break;
					case "SELECTION_RATIO":
						Configuration.SELECTION_RATIO = (float)Convert.ToDecimal (split [1]);
						break;
					case "POPULATION_SIZE":
						Configuration.POPULATION_SIZE = Convert.ToInt32 (split [1]);
						break;
					case "MUTATION_CHANCE_INVERSE":
						Configuration.MUTATION_CHANCE_INVERSE = Convert.ToInt32(split[1]);
						break;
					case "NEW_DURING_MUTATION_INVERSE":
						Configuration.NEW_DURING_MUTATION_INVERSE = Convert.ToInt32(split[1]);
						break;
					case "POPULATION_FILE":
						Configuration.POPULATION_FILE = split[1];
						break;
					case "SAVE_FILE":
						Configuration.SAVE_FILE = split[1];
						break;
					}
				} catch {
					Console.WriteLine ("Faulty Parameter: " + s);
				}
			}
		}

		/*public static void TestCrossover ()
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
		}*/




	}
}
