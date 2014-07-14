using System;
using System.Collections.Generic;
using System.Linq;
using AutoPoco.Engine;

namespace Nest.Tests.MockData.DataSources
{
	/// <summary>
	/// Generator of random ints.
	/// </summary>
	public class IntSource : DatasourceBase<int>
	{
		LongSource longSource = new LongSource();
		public override int Next(IGenerationSession session)
		{
			var longRandom = longSource.Next(session) % int.MaxValue;
			return (int) longRandom;
		}
	}
}
