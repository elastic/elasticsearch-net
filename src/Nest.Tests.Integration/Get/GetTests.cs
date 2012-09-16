using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Get
{
	[TestFixture]
	public class GetTests : BaseElasticSearchTests
	{
		[Test]
		public void SimpleGet()
		{
			var elasticSearchProject = this.ConnectedClient.Get<ElasticSearchProject>(4);
			
			Assert.NotNull(elasticSearchProject);
			Assert.IsNotNullOrEmpty(elasticSearchProject.Name);
		}
		[Test]
		public void SimpleMultiGet()
		{
			var elasticSearchProjects = this.ConnectedClient.Get<ElasticSearchProject>(new [] { 4, 5 });

			Assert.NotNull(elasticSearchProjects);
			Assert.IsNotEmpty(elasticSearchProjects);
			foreach (var e in elasticSearchProjects)
			{
				Assert.IsNotNullOrEmpty(e.Name);
			}
		}
		[Test]
		public void GetWithFields()
		{
			var elasticSearchProject = this.ConnectedClient.Get<ElasticSearchProject>(g=>g
				.Id(4)
				.Fields(f=>f.Name)
			);

			Assert.NotNull(elasticSearchProject);
			Assert.IsNotNullOrEmpty(elasticSearchProject.Name);
		}
		[Test]
		public void GetWithFieldsDeep()
		{
			var fieldSelection = this.ConnectedClient.GetFieldSelection<ElasticSearchProject>(g => g
				.Id(4)
				.Fields(f => f.Name, f => f.Followers.First().FirstName)
			);

			Assert.NotNull(fieldSelection);
			Assert.IsNotNullOrEmpty(fieldSelection.Document.Name);
			var name = fieldSelection.FieldValue<string>(f => f.Name);
			Assert.IsNotNullOrEmpty(name);
			var list = fieldSelection.FieldValue<List<string>>(f=>f.Followers.First().FirstName);
			Assert.NotNull(list);
			Assert.IsNotEmpty(list);
			var array = fieldSelection.FieldValue<string[]>(f => f.Followers.First().FirstName);
			Assert.NotNull(array);
			Assert.IsNotEmpty(array);
			var enumerable = fieldSelection.FieldValue<IEnumerable<string>>(f => f.Followers.First().FirstName);
			Assert.NotNull(enumerable);
			Assert.IsNotEmpty(enumerable);
		}
	}
}
