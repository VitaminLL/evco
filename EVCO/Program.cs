using System;
using System.IO;

namespace EVCO
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			PopulationController controller = new PopulationController();
			controller.InitialisePopulation (args);
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

		public static void PrintPopFitness (PopulationMember[] members)
		{
			int maxFitness = 0;
			for (int i = 0; i < members.Length; i++) {
				if (members [i].calculateFitness () > maxFitness)
					maxFitness = members [i].lastFitness;
			}

			Console.WriteLine ("Population Fitness: " + maxFitness);
		}


	}
}
