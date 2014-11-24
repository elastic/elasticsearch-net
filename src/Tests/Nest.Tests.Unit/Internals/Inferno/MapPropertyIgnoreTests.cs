using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using FluentAssertions;
using NUnit.Framework;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest.Tests.Unit.Internals.Inferno
{
	[TestFixture]
	public class MapPropertyIgnoreForTests : BaseJsonTests
	{
		[ElasticType(IdProperty = "Guid")]
		internal class SomeClass
		{
			public MyCustomClass MyCustomClass { get; set; }
			[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
			public Dictionary<string, SomeOtherClass> StringDict { get; set; }
			public Dictionary<int, MyCustomClass> IntDict { get; set; }
			public IList<MyCustomClass> ListOfCustomClasses { get; set; }
			public B BInstance { get; set; }
			public C CInstance { get; set; }
		}

		internal class B
		{
			internal int X { get; set; }
		}

		internal class C : B
		{
		}

		[ElasticType(IdProperty = "Guid")]
		internal class SomeOtherClass
		{
			[ElasticProperty(Name = "CreateDate")]
			public DateTime CreateDate { get; set; }

			[ElasticProperty(Name = "custom")]
			public MyCustomOtherClass MyCustomOtherClass { get; set; }
		}
		internal class MyCustomClass
		{
			[ElasticProperty(Name = "MID")]
			public string MyProperty { get; set; }

			public override string ToString()
			{
				return "static id ftw";
			}
		}
		[ElasticType(IdProperty = "Guid", Name = "mycustomother")]
		internal class MyCustomOtherClass
		{
			[ElasticProperty(Name = "MID")]
			public string MyProperty { get; set; }

			public override string ToString()
			{
				return "static id ftw";
			}
		}

		internal class UserItemData
		{
			public string Id { get; set; }
			public string UserId { get; set; }
			public string Title { get; set; }
			public string Hidden { get; set; }
			public string UserLabels { get; set; }
		}

		public MapPropertyIgnoreForTests()
		{
			
		}

		public ElasticClient ConfigureClient(Action<ConnectionSettings> settingsSelector)
		{
			var settings = new ConnectionSettings(UnitTestDefaults.Uri, UnitTestDefaults.DefaultIndex);
			settingsSelector(settings);
			var client = new ElasticClient(settings, connection: new InMemoryConnection());
			return client;
		}

		public IElasticClient ClientWithPropertiesFor<T>(Action<FluentDictionary<Expression<Func<T, object>>, PropertyMapping>> propertiesSelector)
		{
			return this.ConfigureClient(c=>c.MapPropertiesFor<T>(propertiesSelector));
		}

		[Test]
		public void Global_Ignore_Should_Be_Adhered()
		{
			var client = this.ClientWithPropertiesFor<MyCustomClass>(props => props
				.Add(p=>p.MyProperty, PropertyMapping.Ignored)
			);
			var json = client.Serializer.Serialize(new MyCustomClass
			{
				MyProperty = "should not be serialized"
			});
			json.Utf8String().Should().Be("{}");
		}
		
		[Test]
		public void CanIgnoreTwice_ExceptionMessageMakesSense()
		{
			var e = Assert.Throws<ArgumentException>(() =>
			{
				this.ClientWithPropertiesFor<MyCustomClass>(props => props
					.Add(p => p.MyProperty, PropertyMapping.Ignored)
					.Add(p => p.MyProperty, PropertyMapping.Ignored)
				);
			});
			e.Message.Should()
				.Contain("is already ignored")
				.And.Contain("p => p.MyProperty");
		}
		
		[Test]
		public void CanNotMapTwiceDifferently_ExceptionMessageMakesSense()
		{
			var e = Assert.Throws<ArgumentException>(() =>
			{
				this.ClientWithPropertiesFor<MyCustomClass>(props => props
					.Add(p => p.MyProperty, PropertyMapping.Ignored)
					.Add(p => p.MyProperty, "mahProperty4")
				);
			});
			e.Message.Should()
				.Contain("can not be mapped to 'mahProperty4'")
				.And.Contain("already has an ignore mapping");
		}
		
		[Test]
		public void CanNotMapTwiceDifferently_ExceptionMessageMakesSense_AlternativeOrder()
		{
			var e = Assert.Throws<ArgumentException>(() =>
			{
				this.ClientWithPropertiesFor<MyCustomClass>(props => props
					.Add(p => p.MyProperty, "mahProperty4")
					.Add(p => p.MyProperty, PropertyMapping.Ignored)
				);
			});
			e.Message.Should()
				.Contain("already has a mapping to 'mahProperty4'")
				.And.Contain("can not be ignored");
		}
	}
}
