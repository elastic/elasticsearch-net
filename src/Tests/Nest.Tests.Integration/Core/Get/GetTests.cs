using System;
using System.Collections.Generic;
using System.Linq;
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
			var elasticSearchProject = this.Client.Source<ElasticsearchProject>(4);
			
			Assert.NotNull(elasticSearchProject);
			Assert.IsNotNullOrEmpty(elasticSearchProject.Name);
		}
		[Test]
		public void SimpleMultiGet()
		{
			var elasticSearchProjects = this.Client.SourceMany<ElasticsearchProject>(new long[] { 4, 5 });

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
			var fieldSelection = this.Client.Get<ElasticsearchProject>(g => g
				.Id(4)
				.Fields(f => f.Name, f => f.Followers.First().FirstName)
			).Fields;

			Assert.NotNull(fieldSelection);
			var name = fieldSelection.FieldValues<ElasticsearchProject, string>(f => f.Name);
			Assert.IsNotEmpty(name);
			var list = fieldSelection.FieldValues<ElasticsearchProject, string>(f=>f.Followers.First().FirstName);
			Assert.NotNull(list);
			Assert.IsNotEmpty(list);
			
		}

		[Test]
		public void GetDocumentThatDoesntExist()
		{
			var result = this.Client.Get<ElasticsearchProject>("this-doc-id-doesnt-exist");
			Assert.IsTrue(result.IsValid);
			Assert.IsFalse(result.Found);
		}
	}
}
