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
			var result = this.ConnectedClient.MultiGetFull(a => a
				.Get<ElasticSearchProject>(g=>g.Id(1))
				.Get<Person>(g => g.Id(100))
			);
			var objects = result.Documents;

			objects.Should().NotBeNull()
				.And.HaveCount(2);


			var person = result.Get<Person>(100);
			person.Should().NotBeNull();
			person.FirstName.Should().NotBeNullOrEmpty()
				.And.Match("Ellie");

		}

		[Test]
		public void GetMultiSimpleWithMissingItem()
		{
			var result = this.ConnectedClient.MultiGetFull(a => a
				.Get<ElasticSearchProject>(g => g.Id(1))
				.Get<Person>(g => g.Id(100000))
				.Get<Person>(g => g.Id(105))
			);
			var objects = result.Documents;

			objects.Should().NotBeNull()
				.And.HaveCount(3);

			var missingPerson = result.GetWithMetaData<Person>(100000);
			missingPerson.Should().NotBeNull();
			missingPerson.Exists.Should().BeFalse();

			var missingPersonDirect = result.Get<Person>(100000);
			missingPersonDirect.Should().BeNull();

			var lewis = result.Get<Person>(105);
			lewis.Should().NotBeNull();
			lewis.FirstName.Should().NotBeNullOrEmpty().And.Match("Lewis");
		}
		
		[Test]
		public void GetMultiWithMetaData()
		{
			var result = this.ConnectedClient.MultiGetFull(a => a
				.Get<ElasticSearchProject>(g => g.Id(1).Fields(p=>p.Id, p=>p.Followers.First().FirstName))
				.Get<Person>(g => g.Id(100).Type("people").Index("nest_test_data").Fields(p => p.Id, p => p.FirstName))
			);

			var objects = result.Documents;
			objects.Should().NotBeNull()
				.And.HaveCount(2);

			var people = objects.OfType<MultiGetHit<Person>>();
			people.Should().HaveCount(1);

			var personHit = people.FirstOrDefault(p => p.Id == "100");
			personHit.Should().NotBeNull();
			personHit.Exists.Should().BeTrue();
			personHit.Version.Should().NotBeNullOrEmpty().And.Match("1");

			var person = personHit.FieldSelection;
			person.Should().NotBeNull();
			person.FieldValue<int>(p=>p.Id).Should().Be(100);
			person.FieldValue<string>(p=>p.FirstName)
				.Should().NotBeNullOrEmpty().And.Match("Ellie");

		}

		[Test]
		public void GetMultiWithMetaDataUsingCleanApi()
		{
			var result = this.ConnectedClient.MultiGetFull(a => a
				.Get<ElasticSearchProject>(g => g.Id(1).Fields(p => p.Id, p => p.Followers.First().FirstName))
				.Get<Person>(g => g.Id(100).Type("people").Index("nest_test_data").Fields(p => p.Id, p => p.FirstName))
			);

			var personHit = result.GetWithMetaData<Person>(100);
			personHit.Should().NotBeNull();
			personHit.Exists.Should().BeTrue();
			personHit.Version.Should().NotBeNullOrEmpty().And.Match("1");

			//personHit.FieldSelection would work too
			var personFieldSelection = result.GetFieldSelection<Person>(100);
			personFieldSelection.Should().NotBeNull();
			personFieldSelection.FieldValue<int>(p => p.Id).Should().Be(100);
			personFieldSelection.FieldValue<string>(p => p.FirstName)
				.Should().NotBeNullOrEmpty().And.Match("Ellie");

			var projectFieldSelection = result.GetFieldSelection<ElasticSearchProject>(1);
			projectFieldSelection.Should().NotBeNull();
			projectFieldSelection.FieldValue<int>(p => p.Id).Should().Be(1);
			projectFieldSelection.FieldValue<string[]>(p => p.Followers.First().FirstName)
				.Should().NotBeEmpty().And.Contain("Lewis");

		}

	}
}
