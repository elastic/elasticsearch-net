using System;
using System.Collections.Generic;
using System.Linq;
using AutoPoco.Engine;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.MockData.DataSources
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
