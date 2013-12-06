using System;
using System.IO;
using System.Collections.Generic;

namespace EVCO
{
	public class Serializer
	{
		public Serializer ()
		{
		}

		public static void SerializePopulation(PopulationMember[] population, StreamWriter stream)
		{
			foreach (PopulationMember p in population)
			{
				stream.WriteLine (p.ToString ());
			}
			stream.Close ();
		}

		public static PopulationMember[] DeserializePopulation (StreamReader stream)
		{
			List<PopulationMember> popList = new List<PopulationMember> (5000);

			while (!stream.EndOfStream) {
				popList.Add (new PopulationMember ());
				string input = stream.ReadLine ().Replace ("{","").Replace ("}","");
				string[] numbers = input.Split (',');
				List<int> numbersList = new List<int> (numbers.Length);
				foreach (string s in numbers) {
					try {
						numbersList.Add (Convert.ToInt32 (s));
					} catch {
						;
					}
				}
				popList [popList.Count - 1].parameters = numbersList.ToArray ();
			}

			stream.Close ();

			return popList.ToArray ();
		}
	}
}

