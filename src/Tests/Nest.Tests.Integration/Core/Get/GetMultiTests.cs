using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Tests.MockData;
using NUnit.Framework;
using Nest;
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
				.Get<ElasticsearchProject>(g=>g.Id(NestTestData.Data[1].Id))
				.Get<Person>(g => g.Id(NestTestData.People[1].Id))
			);
			var objects = result.Documents;

			objects.Should().NotBeNull().And.HaveCount(2);

			var person = result.Source<Person>(NestTestData.People[1].Id);
			person.Should().NotBeNull();
			person.FirstName.Should().NotBeNullOrEmpty();

		}

		[Test]
		public void GetMultiSimpleWithMissingItem()
		{
			var project = -200;
			var frank = -204;
			var lewisId = NestTestData.People[5].Id;

			var result = this._client.MultiGet(a => a
				.Get<Person>(g => g.Id(project))
				.Get<Person>(g => g.Id(frank))
				.Get<Person>(g => g.Id(lewisId))
			);
			var objects = result.Documents;

			objects.Should().NotBeNull()
				.And.HaveCount(3);

			var missingPerson = result.Get<Person>(project);
			missingPerson.Should().NotBeNull();
			missingPerson.Found.Should().BeFalse();

			var missingPersonDirect = result.Source<Person>(frank);
			missingPersonDirect.Should().BeNull();

			var lewis = result.Source<Person>(lewisId);
			lewis.Should().NotBeNull();
			lewis.FirstName.Should().NotBeNullOrEmpty();
		}
		
		[Test]
		public void GetMultiWithMetaData()
		{
			var projectId = NestTestData.Data[14].Id;
			var authorId = NestTestData.People[11].Id;

			var result = this._client.MultiGet(a => a
				.Get<ElasticsearchProject>(g => g.Id(projectId).Fields(p=>p.Id, p=>p.Followers.First().FirstName))
				.Get<Person>(g => g.Id(authorId).Type("person").Index(ElasticsearchConfiguration.DefaultIndex).Fields(p => p.Id, p => p.FirstName))
			);

			var objects = result.Documents;
			objects.Should().NotBeNull()
				.And.HaveCount(2);

			var people = objects.OfType<MultiGetHit<Person>>();
			people.Should().HaveCount(1);

			var personHit = people.FirstOrDefault(p => p.Id == authorId.ToString());
			personHit.Should().NotBeNull();
			personHit.Found.Should().BeTrue();
			personHit.Version.Should().NotBeNullOrEmpty().And.Match("1");

			var fieldSelection = personHit.FieldSelection;
			fieldSelection.Should().NotBeNull();
			fieldSelection.FieldValues<Person, int>(p=>p.Id).Should().BeEquivalentTo(new []{authorId});
			fieldSelection.FieldValues<Person, string>(p => p.FirstName)
				.Should().NotBeEmpty();

		}

		[Test]
		public void GetMultiWithMetaDataUsingCleanApi()
		{
			var projectId = NestTestData.Data[8].Id;
			var authorId = NestTestData.People[5].Id;

			var result = this._client.MultiGet(a => a
				.Get<ElasticsearchProject>(g => g.Id(projectId).Fields(p => p.Id, p => p.Followers.First().FirstName))
				.Get<Person>(g => g
					.Id(authorId)
					.Type("person")
					.Index(ElasticsearchConfiguration.DefaultIndex)
					.Fields(p => p.Id, p => p.FirstName)
				)
			);

			var personHit = result.Get<Person>(authorId);
			personHit.Should().NotBeNull();
			personHit.Found.Should().BeTrue();
			personHit.Version.Should().NotBeNullOrEmpty().And.Match("1");

			//personHit.FieldSelection would work too
			var personFieldSelection = result.GetFieldSelection<Person>(authorId);
			personFieldSelection.Should().NotBeNull();
			personFieldSelection.FieldValues<Person, int>(p => p.Id).Should().BeEquivalentTo(new []{authorId});
			personFieldSelection.FieldValues<Person, string>(p => p.FirstName)
				.Should().NotBeEmpty();

			var projectFieldSelection = result.GetFieldSelection<ElasticsearchProject>(projectId);
			projectFieldSelection.Should().NotBeNull();
			projectFieldSelection.FieldValues<ElasticsearchProject, int>(p => p.Id)
				.Should().BeEquivalentTo(new []{projectId});
			projectFieldSelection.FieldValues<ElasticsearchProject, string>(p => p.Followers.First().FirstName)
				.Should().NotBeEmpty();

		}

	}
}
