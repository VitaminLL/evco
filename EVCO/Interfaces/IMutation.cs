using System;

namespace EVCO
{
	public interface IMutation
	{
		void mutate(ref PopulationMember member);
	}
}

