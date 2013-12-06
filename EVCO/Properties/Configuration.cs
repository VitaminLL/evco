using System;

namespace EVCO
{
	public class Configuration
	{
		public Configuration ()
		{
		}

		public static Type GetRandomGenerator()
		{
			return typeof(StandardRandom);
		}

	}
}

