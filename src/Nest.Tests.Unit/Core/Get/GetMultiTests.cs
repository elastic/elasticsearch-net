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

namespace Nest.Tests.Unit.Core.Get
{
	[TestFixture]
	public class GetMultiTests : BaseJsonTests
	{
		[Test]
		public void GetMultiSimple()
		{
			var objects = this._client.MultiGet(a => a
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
			var objects = this._client.MultiGet(a => a
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
	}
}
