using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Elasticsearch.Net;
using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.Reproduce
{
	/// <summary>
	/// tests to reproduce reported errors
	/// </summary>
	[TestFixture]
	public class Reproduce1320Tests : BaseJsonTests
	{
		[Test]
		public void IdShouldBeInferred()
		{
			var products = Enumerable.Range(0, 100)
				.Select(i => new ProductES { Id = i });
			var descriptor = new BulkDescriptor();
			descriptor.FixedPath("products", "product");
			foreach (var item in products)
			{
				descriptor.Index<ProductES>(op => op
					.Document(item)
					.Index("products")
					.Type("product")
				);
			}

			var result = this._client.Bulk(d => descriptor);
			var index = result.ConnectionStatus.Request.Utf8String();
			foreach (var id in Enumerable.Range(0, 100))
				index.Should().Contain(string.Format(@"""_id"":""{0}""}}", id));
		}
	}

	[ElasticType(IdProperty = "Id")]
	public class ProductES
	{
		public ProductES()
		{
			ProductTag = 1;
			Created = DateTime.UtcNow;
			Modified = DateTime.UtcNow;
			Origin = "*";
		}

		/// <summary>
		/// The unique product id, will be assigned by the database
		/// </summary>
		public long Id { get; set; }

		/// <summary>
		/// If the product was importned from an external system this is the ID to map against
		/// </summary>
		public string ExternalId { get; set; }

		/// <summary>
		/// The origin (owner of the object)...
		/// * = global
		/// <![CDATA[<instance> - Instance record]]>
		/// <![CDATA[<instance>_<site> - Site record]]>
		/// <![CDATA[<instance>_<site>_<customerGroupNo> - Customer group record]]>
		/// <![CDATA[<instance>_<site>_<customerGroupNo>_<customerNo> - Customer record]]>
		/// </summary>
		public string Origin { get; set; }

		/// <summary>
		/// Unique URI name, to browse to the product
		/// </summary>
		public string URIName { get; set; }

		/// <summary>
		/// The name of the product, etc Apple iPhone 6, Samsung Galaxy 4S
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The brand: Apple, Samsung
		/// </summary>
		public string Brand { get; set; }

		//public ProductType ProductType { get; set; }
		public int ProductTag { get; set; }

		/// <summary>
		/// If active we'll export the record to the web shops
		/// </summary>
		public bool Active { get; set; }

		/// <summary>
		///     Defines the image attached to the product, can be inhiert from parent
		/// </summary>
		public string Image { get; set; }

		/// <summary>
		/// The UTC date when the record was created
		/// </summary>
		public System.DateTime Created { get; set; }

		/// <summary>
		/// The UTC date when the record was modified
		/// </summary>
		public System.DateTime Modified { get; set; }
	}
}
