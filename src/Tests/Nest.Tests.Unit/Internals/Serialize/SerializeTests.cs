using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Threading;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;
using Elasticsearch.Net;
using FluentAssertions;

namespace Nest.Tests.Unit.Internals.Serialize
{
	[TestFixture]
	public class SerializeTests : BaseJsonTests
	{
		public class SimpleClass
		{
			public int Id { get; set; }
			public string Name { get; set; }
		}

		public class ClassWithCollections
		{
			public int Id { get; set; }
			private ICollection<SimpleClass> _productVariants;
			public virtual ICollection<SimpleClass> ProductVariants
			{
				get { return _productVariants ?? (_productVariants = new List<SimpleClass>()); }
				set { _productVariants = value; }
			}
		}

		// <summary>
		/// https://github.com/Mpdreamz/NEST/issues/204
		/// 
		/// Reported problems with ICollections
		/// </summary>
		[Test]
		public void ClassWithCollectionSerializes()
		{
			var col = new ClassWithCollections
			{
				Id = 2,
				ProductVariants = new List<SimpleClass>
				{
					new SimpleClass {Id = 1, Name = "class 1"},
					new SimpleClass {Id = 1, Name = "class 1"},
				}
			};
			var json = this._client.Serializer.Serialize(col).Utf8String();
			this.JsonEquals(json, MethodInfo.GetCurrentMethod());
		}

		[Test]
		public void DateTimeIgnoresCurrentCulture()
		{
			var t = new Thread(() =>
			{
				var cultureInfo = new CultureInfo("IT-it");
				cultureInfo.DateTimeFormat.DateSeparator = ".";
				cultureInfo.DateTimeFormat.TimeSeparator = ".";
				Thread.CurrentThread.CurrentCulture = cultureInfo;
				Thread.CurrentThread.CurrentUICulture = cultureInfo;

				var serialized = _client.Serializer.Serialize(new DateTime(2015, 01, 30, 08, 52, 32, 443)).Utf8String();
				serialized.Should().EndWith(":32.443\"");
			});
			t.Start();
			t.Join(TimeSpan.FromSeconds(5));
		}

		[Test]
		public void SerializingSearchIsFastEnough()
		{
			//seraialize once to build cache
			var randomDeserialize = this._client.Serializer.Serialize(new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Fields(f => f.Id, f => f.Country)
				.SortAscending(f => f.LOC)
				.SortDescending(f => f.Country)
				.Query(q => q
					.MatchAll()
				)
			);

			var sw = Stopwatch.StartNew();
			var data = this._client.Serializer.Serialize(new SearchDescriptor<ElasticsearchProject>()
				   .From(0)
				   .Size(10)
				   .Fields(f => f.Id, f => f.Country)
				   .SortAscending(f => f.LOC)
				   .SortDescending(f => f.Country)
				   .Query(q => q
					   .MatchAll()
				   )
			   );
			Assert.NotNull(data);

			data = this._client.Serializer.Serialize(new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Fields(f => f.Id, f => f.Country)
				.SortAscending(f => f.LOC)
				.SortDescending(f => f.Country)
				.Query(q => q
					.MatchAll()
				)
			);
			Assert.LessOrEqual(sw.ElapsedMilliseconds, 10);
			Assert.NotNull(data);

		}
	}
}
