using System;

namespace EVCO
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			PopulationMember[] members = new PopulationMember[10];
			for (int i = 0; i < members.Length; i++) {
				do {
					//System.Threading.Thread.Sleep(100);
					members[i] = new PopulationMember ();
					Console.WriteLine (members[i].calculateFitness() + " : " + members[i].ToString());
				} while (members[i].calculateFitness () == 0);

			}
		}
	}
}
