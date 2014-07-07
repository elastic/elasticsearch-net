using System;
using AutoPoco.Engine;

namespace Nest.Tests.MockData.DataSources
{
	public class FloatSource : DatasourceBase<float>
	{
		private Random mRandom = new Random(1337);
		public override float Next(IGenerationSession session)
		{
			float f = mRandom.Next(0, 100);
			f = f + (float)mRandom.NextDouble();
			return f;
		}

	}
}
