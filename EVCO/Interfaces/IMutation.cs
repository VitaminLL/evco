using System;

namespace EVCO
{
	public interface IMutation
	{
		PopulationMember mutate(PopulationMember member);
	}
}

