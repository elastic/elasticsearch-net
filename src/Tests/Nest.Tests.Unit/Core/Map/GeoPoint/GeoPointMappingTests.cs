using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Tests.Unit.Core.Map.GeoPoint
{
	[TestFixture]
	public class GeoPointMappingTests : BaseJsonTests
	{
		[Test]
		public void FielddataSerializes()
		{
			var result = this._client.Map<ElasticsearchProject>(m => m
				.Properties(ps => ps
					.GeoPoint(gp => gp
						.Name(p => p.Origin)
						.FieldData(fd => fd
							.Format(FieldDataNonStringFormat.Compressed)
							.Precision(1, GeoPrecisionUnit.Kilometers)
						)
					)
				)
			);
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
		}
	}
}
