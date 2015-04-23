using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Integration.Reproduce
{
	[TestFixture]
	public class Reproduce1317Tests : IntegrationTests
	{
		[Test]
		public void FieldValuesShouldNotThrowOnNonExistentFields()
		{
			// With no field values returned
			var result = this.Client.Get<ElasticsearchProject>(g => g
				.Id(4)
				.Fields("fieldthatdoesntexist")
			);

			result.Fields.FieldValues<string[]>("fieldthatdoesntexist").Should().BeNull();

			// With field values returned
			result = this.Client.Get<ElasticsearchProject>(g => g
				.Id(4)
				.Fields("fieldthatdoesntexist", "name")
			);

			result.Fields.FieldValues<string[]>("name").Count().Should().BeGreaterThan(0);
			result.Fields.FieldValues<string[]>("fieldthatdoesntexist").Should().BeNull();
		}
	}
}
