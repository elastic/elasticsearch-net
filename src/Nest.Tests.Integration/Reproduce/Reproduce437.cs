using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Diagnostics;
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

			public string Title { get; set; }

			public DateTime StartDate { get; set; }

			public DateTime EndDate { get; set; }

			public string EventTypeGuid { get; set; }

			public string Location { get; set; }

			public string LocationDetails { get; set; }

			public string Link { get; set; }

			public string Logo { get; set; }

			public string Notice { get; set; }

			public string Image { get; set; }

			public string EventCategoryGuid { get; set; }

			//public string EventCategoryName { get; set; }

			public string PageTagging { get; set; }
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
			id.Should().NotBeBlank().And.Be(o.IndexId);
		}

		[Test]
		public void IdIsProperlyInferreredWhenReferencingGuidDirectly()
		{
			//this is just to show nest can reference GUID as alternative ids albeit with the D formatting
			var guid = Guid.NewGuid();
			var o = new MyOtherIndexableEvent() {Id = "this-is-not-the-id", ContentGUID = guid};
			var id = _client.Infer.Id(o);
			id.Should().NotBeBlank().And.Be(o.ContentGUID.ToString("D"));
		}
	}

	
}
