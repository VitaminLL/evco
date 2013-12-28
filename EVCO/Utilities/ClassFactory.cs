using System;

namespace EVCO
{
	public class ClassFactory
	{
		public ClassFactory ()
		{
		
		}

		public static IRandom GetRandomGenertor()
		{
			return (IRandom)Activator.CreateInstance (Configuration.GetRandomGenerator ());
		}

		public static ICrossover GetCrossover ()
		{
			return (ICrossover)Activator.CreateInstance (Configuration.GetCrossoverOperator ());
		}

		public static IMutation GetMutator ()
		{
			return (IMutation)Activator.CreateInstance (Configuration.GetMutationOperator());
		}

		public static ISelection GetSelector ()
		{
			return (ISelection)Activator.CreateInstance (Configuration.GetSelectionOperator());
		}
	}
}

