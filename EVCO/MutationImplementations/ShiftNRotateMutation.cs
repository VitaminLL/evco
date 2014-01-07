using System;

namespace EVCO
{
	public class ShiftNRotateMutation : IMutation
	{
		IRandom _generator;

		// Changes the probability that each move will be changed
		// Set this to 10 for prob=0.1, 100 for prob=0.01 etc.
		int MUTATION_PROBABILITY_INVERSE = Configuration.MUTATION_CHANCE_INVERSE;

		enum Direction { HorizontalLeft = 0, 
			HorizontalRight = 1, 
			VerticalBottom = 2, 
			VerticalTop = 3, 
			BackSlashBottom = 4, 
			BackSlashTop = 5, 
			ForwardSlashBottom = 6, 
			ForwardSlashTop = 7 };

		public ShiftNRotateMutation ()
		{
			_generator = ClassFactory.GetRandomGenertor ();
		}

		public PopulationMember mutate (PopulationMember member)
		{
			PopulationMember newMember = new PopulationMember();
			newMember.parameters = member.parameters;

			int shiftAmount = _generator.next (1, 10); // Move up to 5 in either direction
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
						newMember.parameters [i + 2]-=shiftAmount;
						newMember.parameters [i + 3]+=shiftAmount;
						break;
					}

					Direction newDir = (Direction) (_generator.next (0, 8));
					Direction currentDir = determineDirection (newMember.parameters, i);
					int[] parameters = newMember.parameters;
					changeDirection (ref parameters, i, currentDir, newDir);
					newMember.parameters = parameters;
				}
			}

			return newMember;
		}

		private Direction determineDirection(int[] parameters, int position)
		{
			int x1 = parameters [position];
			int y1 = parameters [position + 1];
			int x2 = parameters [position + 2];
			int y2 = parameters [position + 3];

			if (x1 == x2 && x1 > x2)
				return Direction.VerticalTop;
			else if (x1 == x2)
				return Direction.VerticalBottom;
			else if (y1 == y2 && y1 > y2)
				return Direction.HorizontalRight;
			else if (y1 == y2)
				return Direction.HorizontalLeft;
			else if (x1 + 4 == x2 && y1 < y2)
				return Direction.ForwardSlashBottom;
			else if (x1 + 4 == x2)
				return Direction.BackSlashTop;
			else if (x1 - 4 == x2 && y1 < y2)
				return Direction.ForwardSlashTop;
			else if (x1 - 4 == x2)
				return Direction.BackSlashBottom;
			else {
				// Not already a valid move, so choose something random
				return (Direction)(_generator.next (0, 8));
			}

		}

		private static void changeDirection(ref int[] parameters, int position, Direction currentDirection, Direction newDirection)
		{
			int x1 = parameters [position];
			int y1 = parameters [position + 1];
			int x2 = parameters [position + 2];
			int y2 = parameters [position + 3];

			int centreX = -1, centreY = -1;

			switch (currentDirection) {
			case Direction.BackSlashBottom:
				centreX = x1 - 2;
				centreY = y1 + 2;
				break;
			case Direction.BackSlashTop:
				centreX = x1 + 2;
				centreY = y1 - 2;
				break;
			case Direction.ForwardSlashBottom:
				centreX = x1 + 2;
				centreY = y1 + 2;
				break;
			case Direction.ForwardSlashTop:
				centreX = x1 - 2;
				centreY = y1 - 2;
				break;
			case Direction.HorizontalRight:
				centreX = x1 - 2;
				centreY = y1;
				break;
			case Direction.HorizontalLeft:
				centreX = x1 + 2;
				centreY = y1;
				break;
			case Direction.VerticalTop:
				centreX = x1;
				centreY = y2 - 2;
				break; 
			case Direction.VerticalBottom:
				centreX = x1;
				centreY = y2 - 2;
				break; 
			}

			switch (newDirection) {
			case Direction.BackSlashBottom:
				x1 = centreX + 2;
				y1 = centreY - 2;
				x2 = centreX - 2;
				y2 = centreY + 2;
				break;
			case Direction.BackSlashTop: 
				x1 = centreX - 2;
				y1 = centreX + 2;
				x2 = centreX + 2;
				y2 = centreX - 2;
				break;
			case Direction.ForwardSlashBottom:
				x1 = centreX - 2;
				y1 = centreY - 2;
				x2 = centreX + 2;
				y2 = centreY + 2;
				break;
			case Direction.ForwardSlashTop:
				x1 = centreX + 2;
				y1 = centreY + 2;
				x2 = centreX - 2;
				y2 = centreY - 2;
				break;
			case Direction.HorizontalLeft:
				x1 = centreX - 2;
				y1 = centreY;
				x2 = centreX + 2;
				y2 = centreY;
				break;
			case Direction.HorizontalRight:
				x1 = centreX + 2;
				y1 = centreY;
				x2 = centreX - 2;
				y2 = centreY;
				break;
			case Direction.VerticalBottom:
				x1 = centreX;
				y1 = centreY - 2;
				x2 = centreX;
				y2 = centreY + 2;
				break;
			case Direction.VerticalTop:
				x1 = centreX;
				y1 = centreY + 2;
				x2 = centreX;
				y2 = centreY - 2;
				break;
			}

			parameters [position] = x1;
			parameters [position + 1] = y1;
			parameters [position + 2] = x2;
			parameters [position + 3] = y2;

		}


			
	}
}

