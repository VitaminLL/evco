using System;
using System.Reflection;

namespace EVCO
{
	public class Configuration
	{
		public static string RANDOM_GENERTOR_TYPE = "EVCO.StandardRandom";
		public static string SELECTION_OPERATOR_TYPE = "EVCO.RouletteSelectionWithElitism";
		public static string MUTATION_OPERATOR_TYPE = "EVCO.ShiftNRotateMutation";
		public static string CROSSOVER_OPERATOR_TYPE = "EVCO.OnePointCrossover";

		public static int POPULATION_SIZE = 500;
		public static int CROSSOVER_PARENT_COUNT = 2;
		public static float SELECTION_RATIO = 0.66f;
		public static int MUTATION_CHANCE_INVERSE = 20;
		public static int NEW_DURING_MUTATION_INVERSE = 100;

		public static string POPULATION_FILE = String.Empty;
		public static string SAVE_FILE = String.Empty;

		public Configuration ()
		{
		}

		public static Type GetRandomGenerator()
		{
			return Type.GetType (RANDOM_GENERTOR_TYPE);
		}

		public static Type GetSelectionOperator()
		{
			return Type.GetType(SELECTION_OPERATOR_TYPE);
		}

		public static Type GetMutationOperator()
		{
			return Type.GetType (MUTATION_OPERATOR_TYPE);
		}

		public static Type GetCrossoverOperator()
		{
			return Type.GetType (CROSSOVER_OPERATOR_TYPE);
		}

		public static void PrintConfiguration ()
		{
			Console.WriteLine ("RANDOM_GENERTOR=" + RANDOM_GENERTOR_TYPE);
			Console.WriteLine ("SELECTION_OPERATOR=" + SELECTION_OPERATOR_TYPE);
			Console.WriteLine ("MUTATION_OPERATOR=" + MUTATION_OPERATOR_TYPE);
			Console.WriteLine ("CROSSOVER_OPERATOR=" + CROSSOVER_OPERATOR_TYPE);
			Console.WriteLine ("POPULATION_SIZE=" + POPULATION_SIZE);
			Console.WriteLine ("SELECTION_RATIO=" + SELECTION_RATIO);
			Console.WriteLine ("MUTATION_CHANCE_INVERSE=" + MUTATION_CHANCE_INVERSE);
			Console.WriteLine ("NEW_DURING_MUTATION_INVERSE=" + NEW_DURING_MUTATION_INVERSE);
			Console.WriteLine ("POPULATION_FILE=" + POPULATION_FILE);
			Console.WriteLine ("CROSSOVER_PARENT_COUNT=" + CROSSOVER_PARENT_COUNT);
		}

	}
}

