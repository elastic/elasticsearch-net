using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Engine;

namespace Nest.Tests.MockData.DataSources
{
	/// <summary>
	/// Generator of random int list.
	/// </summary>
	public class IntListSource : DatasourceBase<List<int>>
	{
		IntSource intSource = new IntSource();
		public override List<int> Next(IGenerationSession session)
		{
			var count = (Math.Abs(intSource.Next(session)) % 3) +1;

			var values = new List<int>();
			for (var i = 0; i < count; i++ )
			{
				values.Add(intSource.Next(session));
			}

			return values;
		}
	}
}
