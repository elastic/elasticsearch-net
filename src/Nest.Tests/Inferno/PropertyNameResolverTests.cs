using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Nest;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using Nest.TestData.Domain;
using System.Linq.Expressions;

namespace Nest.Tests.Inferno
{
	[TestFixture]
  public class PropertyNameResolverTests
	{
		[ElasticType(IdProperty = "Guid")]
		internal class SomeClass
		{
      public MyCustomClass MyCustomClass { get; set; }
		}
    [ElasticType(IdProperty = "Guid")]
    internal class SomeOtherClass
    {
      [ElasticProperty(Name = "custom")]
      public MyCustomClass MyCustomClass { get; set; }

      public MyCustomOtherClass MyCustomOtherClass { get; set; }
    }
		internal class MyCustomClass
		{
      [ElasticProperty(Name="MID")]
      public string MyProperty { get; set; }

			public override string ToString()
			{
				return "static id ftw";
			}
		}
    [ElasticType(IdProperty = "Guid", Name= "mycustomother")]
    internal class MyCustomOtherClass
    {
      [ElasticProperty(Name = "MID")]
      public string MyProperty { get; set; }

      public override string ToString()
      {
        return "static id ftw";
      }
    }
		[Test]
		public void TestUsesElasticProperty()
		{
      Expression<Func<SomeClass, object>> exp = (m) => m.MyCustomClass.MyProperty;
			var propertyName = ElasticClient.PropertyNameResolver.Resolve(exp);
			var expected = "myCustomClass.MID";
			StringAssert.AreEqualIgnoringCase(expected, propertyName);
		}
    [Test]
    public void TestUsesOtherElasticProperty()
    {
      Expression<Func<SomeOtherClass, object>> exp = (m) => m.MyCustomClass.MyProperty;
      var propertyName = ElasticClient.PropertyNameResolver.Resolve(exp);
      var expected = "custom.MID";
      StringAssert.AreEqualIgnoringCase(expected, propertyName);
    }
    [Test]
    public void TestUsesOtherElasticTypePropertyIsIgnored()
    {
      Expression<Func<SomeOtherClass, object>> exp = (m) => m.MyCustomOtherClass.MyProperty;
      var propertyName = ElasticClient.PropertyNameResolver.Resolve(exp);
      var expected = "myCustomOtherClass.MID";
      StringAssert.AreEqualIgnoringCase(expected, propertyName);
    }
	}
}
