using System;
using NUnit.Framework;
using FluentAssertions;

namespace Nest.Tests.Integration.Reproduce
{
	/// <summary>
	/// tests to reproduce reported errors
	/// </summary>
	[TestFixture]
	public class Reproduce437Tests : IntegrationTests
	{
		[ElasticType(IdProperty = "IndexId")]
		public class IndexableEvent: BaseElasticIndexablePage
		{
		}

		[ElasticType(IdProperty = "ContentGuid")]
		public class MyOtherIndexableEvent : BaseElasticIndexablePage
		{
			
		}

		public class BaseElasticIndexablePage : BaseElasticIndexableContent
		{
			public string IndexId
			{
				get { return ContentGUID.ToString("N"); }
			}

			public Guid ContentGUID { get; set; }
			public string ISOLanguage { get; set; }
			public string PageTypeName { get; set; }
		}

		public class BaseElasticIndexableContent
		{
			public string Id { get; set; }
		}

		/// <summary>
		/// https://github.com/Mpdreamz/NEST/issues/437
		/// </summary>
		[Test]
		public void IdIsProperlyInferrered()
		{
			var guid = Guid.NewGuid();
			var o = new IndexableEvent() {Id = "this-is-not-the-id", ContentGUID = guid};
			var id = _client.Infer.Id(o);
			id.Should().NotBeNullOrWhiteSpace().And.Be(o.IndexId);
		}

		[Test]
		public void IdIsProperlyInferreredWhenReferencingGuidDirectly()
		{
			//this is just to show nest can reference GUID as alternative ids albeit with the D formatting
			var guid = Guid.NewGuid();
			var o = new MyOtherIndexableEvent() {Id = "this-is-not-the-id", ContentGUID = guid};
			var id = _client.Infer.Id(o);
			id.Should().NotBeNullOrWhiteSpace().And.Be(o.ContentGUID.ToString("D"));
		}
	}

	
}
