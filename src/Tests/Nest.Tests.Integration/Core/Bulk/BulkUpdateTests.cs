using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Integration.Core.Bulk
{
	[TestFixture]
	public class BulkUpdateTests : IntegrationTests
	{

		[Test]
		public void BulkUpdateObject()
		{
			//Lets first insert some documents with id range 5000-6000
			var descriptor = new BulkDescriptor();
			foreach (var i in Enumerable.Range(5000, 1000))
				descriptor.Index<ElasticsearchProject>(op => op.Document(new ElasticsearchProject { Id = i }));

			var result = this.Client.Bulk(d=>descriptor);
			result.Should().NotBeNull();
			result.IsValid.Should().BeTrue();

			//Now lets update all of them giving them a name
			descriptor = new BulkDescriptor().Refresh();
			foreach (var i in Enumerable.Range(5000, 1000))
			{
				int id = i;
				descriptor.Update<ElasticsearchProject, object>(op => op
					.IdFrom(new ElasticsearchProject { Id = id })
					.Doc(new { name = "SufixedName-" + id})
				);
			}

			result = this.Client.Bulk(d=>descriptor);
			result.Should().NotBeNull();
			result.IsValid.Should().BeTrue();
			result.Errors.Should().BeFalse();
			result.Items.Count().Should().Be(1000);
			result.Items.All(i => i != null).Should().BeTrue();

			var updatedObject = this.Client.Source<ElasticsearchProject>(i=>i.Id(5000));
			Assert.NotNull(updatedObject);
			Assert.AreEqual(updatedObject.Name, "SufixedName-5000");

		}
	}
}