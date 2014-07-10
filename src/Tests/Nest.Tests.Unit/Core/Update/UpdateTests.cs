using System.Reflection;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Core.Update
{
	[TestFixture]
	public class UpdateTests : BaseJsonTests
	{
		public class PartialElasticSearchProject
		{
			public string Name { get; set; }
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
			this.JsonEquals(s, MethodInfo.GetCurrentMethod()); 
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

			this.JsonEquals(s, MethodInfo.GetCurrentMethod());
		}

		[Test]
		public void UpdateUsingPartial()
		{
			var originalProject = new ElasticsearchProject { Id = 1, Name = "NeST", Country = "UK" };
			var partialUpdate = new PartialElasticSearchProject { Name = "NEST", Country = "Netherlands" };

			var s = new UpdateDescriptor<ElasticsearchProject, PartialElasticSearchProject>()
				.Object(originalProject) //only used to infer the id
				.Document(partialUpdate); //the actual partial update statement;

			this.JsonEquals(s, MethodInfo.GetCurrentMethod());
		}

	}
}
