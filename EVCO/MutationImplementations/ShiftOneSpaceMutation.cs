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

		public PopulationMember mutate (PopulationMember member)
		{
			PopulationMember newMember = new PopulationMember();
			newMember.parameters = member.parameters;
			for (int i = 0; i < member.parameters.Length; i+=4) {
				if (_generator.next (0, MUTATION_PROBABILITY_INVERSE) == 0) {
					switch (_generator.next (0, 8)) {
					case 0:
						//right
						newMember.parameters [i]++;
						newMember.parameters [i + 2]++;
						break;
					case 1:
						//down
						newMember.parameters [i + 1]--;
						newMember.parameters [i + 3]--;
						break;
					case 2:
						//left
						newMember.parameters [i]--;
						newMember.parameters [i - 2]--;
						break;
					case 3:
						//up
						newMember.parameters [i + 1]++;
						newMember.parameters [i + 3]++;
						break;
					case 4:
						//up-right
						newMember.parameters [i]++;
						newMember.parameters [i + 1]++;
						newMember.parameters [i + 2]++;
						newMember.parameters [i + 3]++;
						break;
					case 5:
						//down-right
						newMember.parameters [i]++;
						newMember.parameters [i + 1]--;
						newMember.parameters [i + 2]++;
						newMember.parameters [i + 3]--;
						break;
					case 6:
						//down-left
						newMember.parameters [i]--;
						newMember.parameters [i + 1]--;
						newMember.parameters [i + 2]--;
						newMember.parameters [i + 3]--;
						break;
					case 7:
						//up-left
						newMember.parameters [i]--;
						newMember.parameters [i + 1]++;
						member.parameters [i + 2]--;
						member.parameters [i + 3]++;
						break;
					}
				}
			}

			return newMember;
		}
	}
}

