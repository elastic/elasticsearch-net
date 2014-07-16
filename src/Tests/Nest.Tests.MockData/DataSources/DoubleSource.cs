using System;
using System.Collections.Generic;
using System.Linq;
using AutoPoco.Engine;

namespace Nest.Tests.MockData.DataSources
{
	public class DoubleSource : DatasourceBase<double>
	{
		private Random mRandom = new Random(1337);
		public override double Next(IGenerationSession session)
		{
			double f = mRandom.Next(0, 100);
			f = f + mRandom.NextDouble();
			return f;
		}

	}
}
