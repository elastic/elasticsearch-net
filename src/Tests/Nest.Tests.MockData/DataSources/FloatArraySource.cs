using System;
using AutoPoco.Engine;

namespace Nest.Tests.MockData.DataSources
{
	/// <summary>
	/// Generator of random float arrays.
	/// </summary>
	public class FloatArraySource : DatasourceBase<float[]>
	{
		private FloatSource floatSource = new FloatSource();
		private IntSource intSource = new IntSource();
		public override float[] Next(IGenerationSession session)
		{
			var count = Math.Abs(intSource.Next(session)) % 3;

			var values = new float[count];
			for (var i = 0; i < count; i++)
			{
				values[i] = floatSource.Next(session);
			}

			return values;
		}
	}
}
