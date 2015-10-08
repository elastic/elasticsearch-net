using System.Reflection;
using Elasticsearch.Net;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;
using Newtonsoft.Json;

namespace Nest.Tests.Unit.Core.Update
{
	[TestFixture]
	public class UpdateTests : BaseJsonTests
	{
		public class PartialElasticsearchProject
		{
			public string Name { get; set; }
			public string Country { get; set; }
		}
		
		/// <summary>
		/// This POCO models an ElasticsearchProject that allows country to serialize to null explicitly
		/// So that we can use it to clear contents in the Update API
		/// </summary>
		public class PartialElasticsearchProjectWithNull
		{
			[JsonProperty(NullValueHandling = NullValueHandling.Include)]
			public string Country { get; set; }
		}

		public class UpsertCount
		{
			public int Count { get; set; }
		}

		[Test]
		public void UpsertUsingScript()
		{
			var s = new UpdateDescriptor<UpsertCount, UpsertCount>()
			  .Script("ctx._source.counter += count")
			  .Params(p => p
				  .Add("count", 4)
			  )
			  .Upsert(new UpsertCount { Count = 1 }); 
			this.JsonEquals(s, MethodBase.GetCurrentMethod());
		}

		[Test]
		public void UpsertUsingScriptAndPartialObject()
		{
			var s = new UpdateDescriptor<object, object>()
			  .Script("ctx._source.counter += count")
			  .Params(p => p
				  .Add("count", 4)
			  )
			  .Upsert(new { count = 4});

			this.JsonEquals(s, MethodBase.GetCurrentMethod());
		}

		[Test]
		public void UpdateUsingPartial()
		{
			var originalProject = new ElasticsearchProject { Id = 1, Name = "NeST", Country = "UK" };
			var partialUpdate = new PartialElasticsearchProject { Name = "NEST", Country = "Netherlands" };

			var s = new UpdateDescriptor<ElasticsearchProject, PartialElasticsearchProject>()
				.IdFrom(originalProject) //only used to infer the id
				.Doc(partialUpdate); //the actual partial update statement;

			this.JsonEquals(s, MethodBase.GetCurrentMethod());
		}

		[Test]
		public void UpdateUsingPartialWithNull()
		{
			var originalProject = new ElasticsearchProject { Id = 1, Name = "NEST", Country = "UK" };
			var partialUpdate = new PartialElasticsearchProjectWithNull { Country = null };

			var s = new UpdateDescriptor<ElasticsearchProject, PartialElasticsearchProjectWithNull>()
				.IdFrom(originalProject) //only used to infer the id
				.Doc(partialUpdate); //the actual partial update statement;

			this.JsonEquals(s, MethodBase.GetCurrentMethod());
		}
	}
}
