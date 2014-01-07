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

			Console.WriteLine ("Enter unique ID: ");
			int id = Convert.ToInt32 (Console.ReadLine ());

			Configuration.SAVE_FILE = "OUTPUT_" + id.ToString () + ".TXT";


			DateTime dtStart = DateTime.Now;

			while (DateTime.Now < dtStart.AddSeconds(1)) {
				//controller.ThreadedPrintPopFitness ();
				controller.PrintPopFitness ();
				controller.executeNextRound ();
				round++;
				if (round % 10 == 0)
					Console.WriteLine ("Generation " + round.ToString());
			}

			TextWriter tw = new StreamWriter ("OUTPUT_" + id.ToString () + ".TXT", true);

			tw.WriteLine ("RANDOM_GENERTOR=" + Configuration.RANDOM_GENERTOR_TYPE);
			tw.WriteLine ("SELECTION_OPERATOR=" + Configuration.SELECTION_OPERATOR_TYPE);
			tw.WriteLine ("MUTATION_OPERATOR=" + Configuration.MUTATION_OPERATOR_TYPE);
			tw.WriteLine ("CROSSOVER_OPERATOR=" + Configuration.CROSSOVER_OPERATOR_TYPE);
			tw.WriteLine ("POPULATION_SIZE=" + Configuration.POPULATION_SIZE);
			tw.WriteLine ("SELECTION_RATIO=" + Configuration.SELECTION_RATIO);
			tw.WriteLine ("MUTATION_CHANCE_INVERSE=" + Configuration.MUTATION_CHANCE_INVERSE);
			tw.WriteLine ("NEW_DURING_MUTATION_INVERSE=" + Configuration.NEW_DURING_MUTATION_INVERSE);
			tw.WriteLine ("POPULATION_FILE=" + Configuration.POPULATION_FILE);
			tw.WriteLine ("CROSSOVER_PARENT_COUNT=" + Configuration.CROSSOVER_PARENT_COUNT);

			tw.WriteLine ("Generations:" + round.ToString ());
			tw.WriteLine ("Best Fitness:" + controller.PrintPopFitness ());
			tw.WriteLine ("Average Fitness:" + controller.averagePopulationFitness().ToString ());
			tw.WriteLine ("Fitness StDev:" + controller.popFitnessStdDev (controller.averagePopulationFitness()).ToString());

			tw.Close ();

			Console.WriteLine ("DONE!");
			Console.ReadLine ();
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
					case "CROSSOVER_PARENT_COUNT":
						Configuration.CROSSOVER_PARENT_COUNT = Convert.ToInt32(split[1]);
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
