using System;

namespace EVCO
{
	public class ShiftOneSpaceMutation : IMutation
	{
		IRandom _generator;

		// Changes the probability that each move will be changed
		// Set this to 10 for prob=0.1, 100 for prob=0.01 etc.
		const int MUTATION_PROBABILITY_INVERSE = 10;

		public ShiftOneSpaceMutation ()
		{
			_generator = ClassFactory.GetRandomGenertor ();
		}

		public void mutate (ref PopulationMember member)
		{
			for (int i = 0; i < member.parameters.Length; i+=4) {
				if (_generator.next (0, MUTATION_PROBABILITY_INVERSE) == 0) {
					switch (_generator.next (0, 8)) {
					case 0:
						//right
						member.parameters [i]++;
						member.parameters [i + 2]++;
						break;
					case 1:
						//down
						member.parameters [i + 1]--;
						member.parameters [i + 3]--;
						break;
					case 2:
						//left
						member.parameters [i]--;
						member.parameters [i - 2]--;
						break;
					case 3:
						//up
						member.parameters [i + 1]++;
						member.parameters [i + 3]++;
						break;
					case 4:
						//up-right
						member.parameters [i]++;
						member.parameters [i + 1]++;
						member.parameters [i + 2]++;
						member.parameters [i + 3]++;
						break;
					case 5:
						//down-right
						member.parameters [i]++;
						member.parameters [i + 1]--;
						member.parameters [i + 2]++;
						member.parameters [i + 3]--;
						break;
					case 6:
						//down-left
						member.parameters [i]--;
						member.parameters [i + 1]--;
						member.parameters [i + 2]--;
						member.parameters [i + 3]--;
						break;
					case 7:
						//up-left
						member.parameters [i]--;
						member.parameters [i + 1]++;
						member.parameters [i + 2]--;
						member.parameters [i + 3]++;
						break;
					}
				}
			}
		}
	}
}

