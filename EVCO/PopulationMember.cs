using System;
using System.Text;
using System.Linq;

namespace EVCO
{
	public class PopulationMember
	{
		public int[] parameters { get; set;} 
		private IRandom _generator;
		public int lastFitness = 0;

		public PopulationMember ()
		{
			_generator = ClassFactory.GetRandomGenertor ();
			this.initialisePopulation ();
		}

		private void initialisePopulation()
		{
			do {
				parameters = new int[_generator.next (4, 17)];
			} while (parameters.Length % 4 != 0);

			// Populate the parameters array

			int direction = _generator.next (0, 8);
			// 0 = vertical down
			// 1 = backslash down
			// 2 = horizontal right
			// 3 = forward slash up
			// 4 = vertical up
			// 5 = backslash up
			// 6 = horizontal left
			// 7 = forward slash down

			// x1 y1 x2 y2

			for (int i = 0; i < parameters.Length; i++) {

				do {
				direction = _generator.next (0, 8);

				// ToDo: Weight these towards the center initially
				parameters [i] = _generator.next (0, 41);
				parameters [i + 1] = _generator.next (0, 41);

				switch (direction) {
				case 0:
						parameters [i + 2] = parameters [i];
						parameters [i + 3] = parameters [i + 1] + 4;
					break;
				case 1:
						parameters [i + 2] = parameters [i] - 4;
						parameters [i + 3] = parameters [i + 1] - 4;
					break;
				case 2:
						parameters [i + 2] = parameters [i] + 4;
						parameters [i + 3] = parameters [i + 1];
					break;
				case 3:
						parameters [i + 2] = parameters [i] + 4;
						parameters [i + 3] = parameters [i + 1] + 4;
					break;
				case 4:
						parameters [i + 2] = parameters [i];
						parameters [i + 3] = parameters [i + 1] + 4;
					break;
				case 5:
						parameters [i + 2] = parameters [i] - 4;
						parameters [i + 3] = parameters [i + 1] + 4;
					break;
				case 6:
						parameters [i + 2] = parameters [i] - 4;
						parameters [i + 3] = parameters [i + 1];
					break;
				case 7:
						parameters [i + 2] = parameters [i] - 4;
						parameters [i + 3] = parameters [i + 1] - 4;
					break;
					}
				} while (parameters[i] < 1 || parameters[i] > 40
					|| parameters[i+1] < 1 || parameters[i+1] > 40
					|| parameters[i+2] < 1 || parameters[i+2] > 40
					|| parameters[i+3] < 1 || parameters[i+3] > 40 );


				i += 3;
			}
					
		}

		public int calculateFitness()
		{
			lastFitness = Solitaire.Run(parameters);
			return lastFitness;
		}

		public override string ToString ()
		{
			StringBuilder s = new StringBuilder ("{ ");

			foreach (int n in parameters)
				s.Append (n + ", ");

			s.Append ("}");
			return s.ToString ();
		}
	}
}

