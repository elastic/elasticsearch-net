using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Core.Get
{
	[TestFixture]
	public class GetTests : IntegrationTests
	{
		[Test]
		public void SimpleGet()
		{
			var elasticSearchProject = this._client.Source<ElasticsearchProject>(4);
			
			Assert.NotNull(elasticSearchProject);
			Assert.IsNotNullOrEmpty(elasticSearchProject.Name);
		}
		[Test]
		public void SimpleMultiGet()
		{
			var elasticSearchProjects = this._client.SourceMany<ElasticsearchProject>(new [] { 4, 5 });

			Assert.NotNull(elasticSearchProjects);
			Assert.IsNotEmpty(elasticSearchProjects);
			foreach (var e in elasticSearchProjects)
			{
				Assert.IsNotNullOrEmpty(e.Name);
			}
		}
		
		[Test]
		public void GetWithFieldsDeep()
		{
			var fieldSelection = this._client.Get<ElasticsearchProject>(g => g
				.Id(4)
				.Fields(f => f.Name, f => f.Followers.First().FirstName)
			).Fields;

			Assert.NotNull(fieldSelection);
			var name = fieldSelection.FieldValue(f => f.Name);
			Assert.IsNotEmpty(name);
			var list = fieldSelection.FieldValue(f=>f.Followers.First().FirstName);
			Assert.NotNull(list);
			Assert.IsNotEmpty(list);
			
		}
	}
}
