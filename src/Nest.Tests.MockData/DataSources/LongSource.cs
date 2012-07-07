using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Engine;

namespace Nest.Tests.MockData.DataSources
{
	public class LongSource : DatasourceBase<long>
	{
		private Random mRandom = new Random(1337);
		public override long Next(IGenerationSession session)
		{
			long f = (long)((mRandom.NextDouble() * 2.0 - 1.0) * long.MaxValue); ;
			return f;
		}
	}
}
