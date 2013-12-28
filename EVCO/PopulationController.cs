using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace EVCO
{
	public class PopulationController
	{
		private PopulationMember[] members;

		ICrossover crosser;
		IMutation mutator;
		ISelection selector;
		IRandom generator;

		public PopulationController ()
		{
			members = new PopulationMember[0];

			crosser = ClassFactory.GetCrossover();
			mutator = ClassFactory.GetMutator();
			selector = ClassFactory.GetSelector();
			generator = ClassFactory.GetRandomGenertor ();
		}

		public void InitialisePopulation()
		{
			if (Configuration.POPULATION_FILE != String.Empty) {
				Console.Write ("Loading Initial Population... ");
				members = Serializer.DeserializePopulation (new StreamReader (Configuration.POPULATION_FILE));
				Console.WriteLine ("Done");
			} else {
				int POPULATION_SIZE = Configuration.POPULATION_SIZE;
				Console.WriteLine ("Generating Initial Population... (0/" + POPULATION_SIZE + ")");
				members = new PopulationMember[POPULATION_SIZE];
				for (int i = 0; i < members.Length; i++) {
					do {
						members [i] = new PopulationMember ();
					} while (members[i].calculateFitness () == 0);

					//Console.SetCursorPosition (0, 0);
					//Console.Write ("Generating Initial Population... (" + i + "/" + INITIAL_POPULATION_SIZE + ")");
				}
				//Console.SetCursorPosition (0, 0);
				Console.WriteLine ("Generating Initial Population... Completed   ");
				Console.Write ("Saving Population... ");
				Serializer.SerializePopulation (members, new StreamWriter ("LASTPOP_" + POPULATION_SIZE.ToString () + ".TXT", false));
				Serializer.SerializePopulation (members, new StreamWriter ("LASTPOP.TXT", false));
				Console.WriteLine ("Done");
			}
		}

		private PopulationMember[] selectFittestMembers ()
		{
			return selector.select (members);
		}

		private List<PopulationMember> addCrossoverMembers (List<PopulationMember> newPop, int numberToAdd)
		{
			//Console.WriteLine ("Beginning Crossover");
			// The fittest members have are more likely to be crossed (likelihood is proportional to their fitness)
			List<int> probabilityArray = new List<int> ();

			//Console.WriteLine ("Caluclating Probability Array...");

			for (int x = 0; x < newPop.Count; x++) {
				for (int i = 0; i < newPop[x].lastFitness; i++)
					probabilityArray.Add (x);
			}

			for (int i = 0; i < numberToAdd; i++) {
				int first = probabilityArray [generator.next (0, probabilityArray.Count)];
				int second = -1;
				do {
					second = probabilityArray [generator.next (0, probabilityArray.Count)];
				} while (second == first);

				//Console.WriteLine ("{4}/{5}: Crossing {0} (fitness {1}) and {2} (fitness {3})", first, newPop[first].lastFitness, second, newPop[second].lastFitness, i, numberToAdd);

				// Mutate crossover members
				PopulationMember newMember = crosser.crossover(newPop[first], newPop[second]);
				newMember = mutator.mutate (newMember);

				// With a certain probability, the crossover will be dismissed and a brand new population member introduced
				if (generator.next (0, Configuration.NEW_DURING_MUTATION_INVERSE) == 0)
				{
					PopulationMember p = new PopulationMember();
					while (p.calculateFitness () < 1)
						p = new PopulationMember();
					newPop.Add (p);
				}
				else
				{
					newPop.Add (newMember);
				}

				//newPop.Add(crosser.crossover (newPop[first], newPop[second]));
			}

			return newPop;
		}

		/*private List<PopulationMember> mutateRandomly (List<PopulationMember> newPop)
		{
			//Console.WriteLine ("Beginning Mutation");

			for (int i = 0; i < newPop.Count; i++) {
				if (generator.next (0, 5) == 4) {
					//Console.WriteLine ("Mutating {0}...", i);
					newPop [i] = mutator.mutate (newPop [i]);
				}
			}

			return newPop;
		}*/

		public void executeNextRound ()
		{
			//Console.WriteLine ("Beginning next round");
			// Select the fittest population members
			// Proportion is decided by selection implementation
			List<PopulationMember> newPop = new List<PopulationMember>(this.selectFittestMembers());

			// Remainder to be generated from crossover
			int toCrossover = members.Length - newPop.Count;
			newPop = this.addCrossoverMembers (newPop, toCrossover);

			// Test: Mutate only crossovers removes this line
			// Mutate the new population a little
			//newPop = this.mutateRandomly(newPop);

			members = newPop.ToArray ();
		}

		public void PrintPopFitness ()
		{
			//Console.WriteLine ("Evaluating Fitness... ");
			int maxFitness = 0;

			for (int i = 0; i < members.Length; i++) {
				if (members [i].calculateFitness () > maxFitness)
				{
					maxFitness = members [i].lastFitness;
				}
			}

			Console.WriteLine ("Fittest Population Member's Fitness: " + maxFitness);
			//System.Threading.Thread.Sleep (3000);
		}

		public void ThreadedPrintPopFitness ()
		{
			int THREAD_COUNT = 4;
			int membersPerThread = members.Length / THREAD_COUNT;

			Thread[] fitnessCalculator = new Thread[THREAD_COUNT];
			int[] threadResult = new int[THREAD_COUNT];

			for (int i = 0; i < THREAD_COUNT; i++) {
				int iCopy = i;
				int start = iCopy * membersPerThread;
				int stop = Math.Max ((iCopy+1) * membersPerThread, members.Length);
				fitnessCalculator [iCopy] = new Thread (() => getMaxFitness (members, start, stop, ref threadResult [iCopy]));
			}

			for (int i = 0; i < THREAD_COUNT; i++) {
				fitnessCalculator [i].Start ();
			}

			for (int i = 0; i < THREAD_COUNT; i++) {
				fitnessCalculator [i].Join ();
			}

			int maxFitness = 0;
			for (int i = 0; i < THREAD_COUNT; i++) {
				if (maxFitness < threadResult [i])
					maxFitness = threadResult [i];
			}

			Console.WriteLine ("(ThreadedCount) Fittest Population Member's Fitness: " + maxFitness);
		}

		private static void getMaxFitness (PopulationMember[] members, int start, int stop, ref int result)
		{
			int maxFitness = 0;

			for (int i = start; i < stop && i < members.Length; i++) {
				if (members [i].calculateFitness () > maxFitness) {
					maxFitness = members [i].lastFitness;
					//Console.WriteLine (members[i].ToString ());
				}
			}

			result = maxFitness;
		}
	}
}

