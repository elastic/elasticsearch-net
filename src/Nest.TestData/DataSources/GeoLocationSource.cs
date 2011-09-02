using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Engine;
using Nest.TestData.Domain;

namespace Nest.TestData.DataSources
{
	public class GeoLocationSource : DatasourceBase<GeoLocation>
	{
		private Random mRandom = new Random(1337);
		public override GeoLocation Next(IGenerationSession session)
		{
			return session.Single<GeoLocation>().Get();
		}

	}
}
