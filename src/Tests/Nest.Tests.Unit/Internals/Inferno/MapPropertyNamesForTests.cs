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
	public class MapPropertyNamesForTests : BaseJsonTests
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

		public MapPropertyNamesForTests()
		{
			
		}

		public ElasticClient ConfigureClient(Action<ConnectionSettings> settingsSelector)
		{
			var settings = new ConnectionSettings(UnitTestDefaults.Uri, UnitTestDefaults.DefaultIndex);
			settingsSelector(settings);
			var client = new ElasticClient(settings, connection: new InMemoryConnection());
			return client;
		}

		public IElasticClient ClientWithPropertiesFor<T>(Action<PropertyMappingDescriptor<T>> propertiesSelector)
		{
			return this.ConfigureClient(c=>c.MapPropertiesFor<T>(propertiesSelector));
		}

		[Test]
		public void SettingsTakePrecedenceOverAttributes()
		{
			var client = this.ClientWithPropertiesFor<MyCustomClass>(props => props
				.Rename(p=>p.MyProperty, "mahPropertah")
			);
			Expression<Func<SomeClass, object>> exp = (m) => m.MyCustomClass.MyProperty;
			client.Infer.PropertyPath(exp).Should().Be("myCustomClass.mahPropertah");
		}

		[Test]
		public void CanNotMapAnyDepth()
		{
			var e = Assert.Throws<ArgumentException>(()=>
				this.ClientWithPropertiesFor<SomeClass>(props => props
					.Rename(p=>p.MyCustomClass.MyProperty, "mahPropertah")
				)
			);
			e.Message.Should().Contain("can only map direct properties");
		}

		[Test]
		public void CanNotMapSamePropertyTwice()
		{
			var e = Assert.Throws<ArgumentException>(()=>
				this.ClientWithPropertiesFor<MyCustomClass>(props => props
					.Rename(p=>p.MyProperty, "mahPropertah")
					.Rename(p=>p.MyProperty, "mahPropertah2")
				)
			);
			e.Message.Should()
				.Contain("on type MyCustomClass")
				.And.Contain("can not be mapped to 'mahPropertah2'")
				.And.Contain("already mapped as 'mahPropertah'");
		}

		[Test]
		public void CanNotMapSamePropertyTwice_SubClasses()
		{
			var e = Assert.Throws<ArgumentException>(()=>
				this.ConfigureClient(c=>c
					.MapPropertiesFor<B>(props => props
						.Rename(p=>p.X, "bX")
					)
					.MapPropertiesFor<C>(props => props
						.Rename(p=>p.X, "cX")
					)
				)
			);
			e.Message.Should()
				.Contain("on type C")
				.And.Contain("can not be mapped to 'cX'")
				.And.Contain("already mapped as 'bX'");
		}
		
		[Test]
		public void ResolverShouldResolveAllNestedMembers()
		{
			var client = this.ConfigureClient(c=>c
				.MapPropertiesFor<SomeClass>(props => props
					.Rename(p=>p.MyCustomClass, "customClazz")
				)
				.MapPropertiesFor<MyCustomClass>(props => props
					.Rename(p=>p.MyProperty, "mahPropertah")
				)
			);
			Expression<Func<SomeClass, object>> exp = (m) => m.MyCustomClass.MyProperty;
			client.Infer.PropertyPath(exp).Should().Be("customClazz.mahPropertah");
		}
		
		[Test]
		public void ResolverShouldResolveAllNestedMembers_Dictionary()
		{
			var client = this.ConfigureClient(c=>c
				.MapPropertiesFor<SomeClass>(props => props
					.Rename(p=>p.StringDict, "map")
				)
				.MapPropertiesFor<SomeOtherClass>(props => props
					.Rename(p=>p.CreateDate, "d0b")
				)
			);
			Expression<Func<SomeClass, object>> exp = (m) => m.StringDict["path"].CreateDate;
			client.Infer.PropertyPath(exp).Should().Be("map.path.d0b");
		}
		
		[Test]
		public void PropertiesOn_CInstanceTakeNamesFrom_B()
		{
			var client = this.ConfigureClient(c=>c
				.MapPropertiesFor<B>(props => props
					.Rename(p=>p.X, "Xavier")
				)
			);
			Expression<Func<SomeClass, object>> exp = (m) => m.CInstance.X;
			client.Infer.PropertyPath(exp).Should().Be("cInstance.Xavier");
		}
		
		[Test]
		public void PropertiesOn_BInstanceTakeNamesFrom_C()
		{
			var client = this.ConfigureClient(c=>c
				.MapPropertiesFor<C>(props => props
					.Rename(p=>p.X, "Xavier")
				)
			);
			Expression<Func<SomeClass, object>> exp = (m) => m.BInstance.X;
			client.Infer.PropertyPath(exp).Should().Be("bInstance.Xavier");
		}
		
		[Test]
		public void PropertiesOnCollectionExpressionsResolve()
		{
			var client = this.ConfigureClient(c=>c
				.MapPropertiesFor<MyCustomClass>(props => props
					.Rename(p=>p.MyProperty, "myProp")
				)
			);
			Expression<Func<SomeClass, object>> exp = (m) => m.ListOfCustomClasses.First().MyProperty;
			client.Infer.PropertyPath(exp).Should().Be("listOfCustomClasses.myProp");
		}
	}
}
