using System;

namespace EVCO
{
	public class StandardRandom : IRandom
	{
		Random r;

		public StandardRandom ()
		{
			r = new Random ();
		}

		public int next(int min, int max)
		{
			return r.Next (min, max);
		}
	}
}

