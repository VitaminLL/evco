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
	}
}

