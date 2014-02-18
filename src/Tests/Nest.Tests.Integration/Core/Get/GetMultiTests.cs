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
	public class GetMultiTests : IntegrationTests
	{
		[Test]
		public void GetMultiSimple()
		{
			var result = this._client.MultiGet(a => a
				.Get<ElasticsearchProject>(g=>g.Id(1))
				.Get<Person>(g => g.Id(100))
			);
			var objects = result.Documents;

			objects.Should().NotBeNull().And.HaveCount(2);


			var person = result.Source<Person>(100);
			person.Should().NotBeNull();
			person.FirstName.Should().NotBeNullOrEmpty();

		}

		[Test]
		public void GetMultiSimpleWithMissingItem()
		{
			var result = this._client.MultiGet(a => a
				.Get<ElasticsearchProject>(g => g.Id(1))
				.Get<Person>(g => g.Id(100000))
				.Get<Person>(g => g.Id(105))
			);
			var objects = result.Documents;

			objects.Should().NotBeNull()
				.And.HaveCount(3);

			var missingPerson = result.Get<Person>(100000);
			missingPerson.Should().NotBeNull();
			missingPerson.Found.Should().BeFalse();

			var missingPersonDirect = result.Source<Person>(100000);
			missingPersonDirect.Should().BeNull();

			var lewis = result.Source<Person>(105);
			lewis.Should().NotBeNull();
			lewis.FirstName.Should().NotBeNullOrEmpty();
		}
		
		[Test]
		public void GetMultiWithMetaData()
		{
			var result = this._client.MultiGet(a => a
				.Get<ElasticsearchProject>(g => g.Id(1).Fields(p=>p.Id, p=>p.Followers.First().FirstName))
				.Get<Person>(g => g.Id(100).Type("person").Index(ElasticsearchConfiguration.DefaultIndex).Fields(p => p.Id, p => p.FirstName))
			);

			var objects = result.Documents;
			objects.Should().NotBeNull()
				.And.HaveCount(2);

			var people = objects.OfType<MultiGetHit<Person>>();
			people.Should().HaveCount(1);

			var personHit = people.FirstOrDefault(p => p.Id == "100");
			personHit.Should().NotBeNull();
			personHit.Found.Should().BeTrue();
			personHit.Version.Should().NotBeNullOrEmpty().And.Match("1");

			var person = personHit.FieldSelection;
			person.Should().NotBeNull();
			person.FieldValue(p=>p.Id).Should().BeEquivalentTo(new []{100});
			person.FieldValue(p => p.FirstName)
				.Should().NotBeEmpty();

		}

		[Test]
		public void GetMultiWithMetaDataUsingCleanApi()
		{
			var result = this._client.MultiGet(a => a
				.Get<ElasticsearchProject>(g => g.Id(1).Fields(p => p.Id, p => p.Followers.First().FirstName))
				.Get<Person>(g => g
					.Id(100)
					.Type("person")
					.Index(ElasticsearchConfiguration.DefaultIndex)
					.Fields(p => p.Id, p => p.FirstName)
				)
			);

			var personHit = result.Get<Person>(100);
			personHit.Should().NotBeNull();
			personHit.Found.Should().BeTrue();
			personHit.Version.Should().NotBeNullOrEmpty().And.Match("1");

			//personHit.FieldSelection would work too
			var personFieldSelection = result.GetFieldSelection<Person>(100);
			personFieldSelection.Should().NotBeNull();
			personFieldSelection.FieldValue(p => p.Id).Should().BeEquivalentTo(new []{100});
			personFieldSelection.FieldValue(p => p.FirstName)
				.Should().NotBeEmpty();

			var projectFieldSelection = result.GetFieldSelection<ElasticsearchProject>(1);
			projectFieldSelection.Should().NotBeNull();
			projectFieldSelection.FieldValue(p => p.Id).Should().BeEquivalentTo(new []{1});
			projectFieldSelection.FieldValue(p => p.Followers.First().FirstName)
				.Should().NotBeEmpty();

		}

	}
}
