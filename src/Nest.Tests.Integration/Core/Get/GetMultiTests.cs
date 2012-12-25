using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Nest;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using Nest.Tests.MockData.Domain;
using FluentAssertions;

namespace Nest.Tests.Integration.Core.Get
{
	[TestFixture]
	public class GetMultiTests : BaseElasticSearchTests
	{
		[Test]
		public void GetMultiSimple()
		{
			var objects = this.ConnectedClient.MultiGet(a => a
				.Get<ElasticSearchProject>(g=>g.Id(1))
				.Get<Person>(g => g.Id(100))
			);


			objects.Should().NotBeNull()
				.And.HaveCount(2);

			var people = objects.OfType<Person>();
			people.Should().HaveCount(1);

			var person = people.FirstOrDefault(p => p.Id == 100);
			person.Should().NotBeNull();
			person.FirstName.Should().NotBeNullOrEmpty()
				.And.Match("Ellie");

		}

		[Test]
		public void GetMultiSimpleWithMissingItem()
		{
			var objects = this.ConnectedClient.MultiGet(a => a
				.Get<ElasticSearchProject>(g => g.Id(1))
				.Get<Person>(g => g.Id(100000))
				.Get<Person>(g => g.Id(105))
			);

			objects.Should().NotBeNull()
				.And.HaveCount(2);

			var people = objects.OfType<Person>();
			people.Should().HaveCount(1);

			var lewis = people.FirstOrDefault(p => p.Id == 105);
			lewis.FirstName.Should().NotBeNullOrEmpty().And.Match("Lewis");
		}
		[Test]
		public void GetMultiWithFields()
		{
			var objects = this.ConnectedClient.MultiGet(a => a
				.Get<ElasticSearchProject>(g => g.Id(1))
				.Get<Person>(g => g.Id(100).Type("people").Index("nest_test_data").Fields(p=>p.Id, p=>p.FirstName))
			);

			objects.Should().NotBeNull()
				.And.HaveCount(2);

			var people = objects.OfType<Person>();
			people.Should().HaveCount(1);

			var person = people.FirstOrDefault(p => p.Id == 100);
			person.Should().NotBeNull();
			person.FirstName.Should().NotBeNullOrEmpty()
				.And.Match("Ellie");

		}

	}
}
