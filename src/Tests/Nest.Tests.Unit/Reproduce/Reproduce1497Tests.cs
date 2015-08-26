using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Reproduce
{
	[TestFixture]
	public class Reproduce1497Tests : BaseJsonTests
	{
		[Test]
		public void UpdateDescriptorFieldsExpressionSerialization()
		{
			var update = this._client.Update<ElasticsearchProject>(u => u
				.Id(1)
				.Fields(p => p.Name, p => p.Id)
			);
			update.ConnectionStatus.RequestUrl.Should().EndWith("fields=name%2Cid");
		}
	}
}
