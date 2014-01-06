using System;

namespace EVCO
{
	public class ShiftNSpacesMutation : IMutation
	{
		IRandom _generator;

		// Changes the probability that each move will be changed
		// Set this to 10 for prob=0.1, 100 for prob=0.01 etc.
		int MUTATION_PROBABILITY_INVERSE = Configuration.MUTATION_CHANCE_INVERSE;

		public ShiftNSpacesMutation ()
		{
			_generator = ClassFactory.GetRandomGenertor ();
		}

		public PopulationMember mutate (PopulationMember member)
		{
			PopulationMember newMember = new PopulationMember();
			newMember.parameters = member.parameters;
			int shiftAmount = 1;//_generator.next (1, 10); // Move up to 5 in either direction
			for (int i = 0; i < member.parameters.Length; i+=4) {
				if (_generator.next (0, MUTATION_PROBABILITY_INVERSE) == 0) {
					switch (_generator.next (0, 8)) {
					case 0:
						//right
						newMember.parameters [i]+=shiftAmount;
						newMember.parameters [i + 2]+=shiftAmount;
						break;
					case 1:
						//down
						newMember.parameters [i + 1]-=shiftAmount;
						newMember.parameters [i + 3]-=shiftAmount;
						break;
					case 2:
						//left
						newMember.parameters [i]-=shiftAmount;
						newMember.parameters [i + 2]-=shiftAmount;
						break;
					case 3:
						//up
						newMember.parameters [i + 1]+=shiftAmount;
						newMember.parameters [i + 3]+=shiftAmount;
						break;
					case 4:
						//up-right
						newMember.parameters [i]+=shiftAmount;
						newMember.parameters [i + 1]+=shiftAmount;
						newMember.parameters [i + 2]+=shiftAmount;
						newMember.parameters [i + 3]+=shiftAmount;
						break;
					case 5:
						//down-right
						newMember.parameters [i]++;
						newMember.parameters [i + 1]-=shiftAmount;
						newMember.parameters [i + 2]+=shiftAmount;
						newMember.parameters [i + 3]-=shiftAmount;
						break;
					case 6:
						//down-left
						newMember.parameters [i]-=shiftAmount;
						newMember.parameters [i + 1]-=shiftAmount;
						newMember.parameters [i + 2]-=shiftAmount;
						newMember.parameters [i + 3]-=shiftAmount;
						break;
					case 7:
						//up-left
						newMember.parameters [i]-=shiftAmount;
						newMember.parameters [i + 1]+=shiftAmount;
						member.parameters [i + 2]-=shiftAmount;
						member.parameters [i + 3]+=shiftAmount;
						break;
					}
				}
			}

			return newMember;
		}
	}
}

