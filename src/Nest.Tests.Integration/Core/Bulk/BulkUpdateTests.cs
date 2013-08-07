using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Integration.Core.Bulk
{
	public class BulkUpdateTests : BulkTests
	{

		[Test]
		public void BulkUpdateObject()
		{
			//Lets first insert some documents with id range 5000-6000
			var descriptor = new BulkDescriptor();
			foreach (var i in Enumerable.Range(5000, 1000))
				descriptor.Index<ElasticSearchProject>(op => op.Object(new ElasticSearchProject { Id = i }));

			var result = this._client.Bulk(descriptor);
			result.Should().NotBeNull();
			result.IsValid.Should().BeTrue();

			//Now lets update all of them giving them a name
			descriptor = new BulkDescriptor();
			foreach (var i in Enumerable.Range(5000, 1000))
			{
				int id = i;
				descriptor.Update<ElasticSearchProject, object>(op => op
					.Object(new ElasticSearchProject { Id = id })
					.Document(new { name = "SufixedName-" + id})
					.Refresh()
				);
			}

			result = this._client.Bulk(descriptor);
			result.Should().NotBeNull();
			result.IsValid.Should().BeTrue();
			result.Items.Count().Should().Be(1000);
			result.Items.All(i => i != null).Should().BeTrue();
			result.Items.All(i => i.OK).Should().BeTrue();

			var updatedObject = this._client.Get<ElasticSearchProject>(5000);
			Assert.NotNull(updatedObject);
			Assert.AreEqual(updatedObject.Name, "SufixedName-5000");

		}
	}
}