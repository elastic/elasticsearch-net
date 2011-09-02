using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Engine;

namespace Nest.TestData.DataSources
{
	public class LOCSource : DatasourceBase<int>
	{
		private Random mRandom = new Random(1337);
		public override int Next(IGenerationSession session)
		{
			int f = mRandom.Next(10000, 20000);
			return f;
		}

	}
}
