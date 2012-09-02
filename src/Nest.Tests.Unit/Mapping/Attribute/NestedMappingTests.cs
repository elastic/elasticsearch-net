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
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Mapping.Attribute
{
	[TestFixture]
	public class NestedMappingTests : BaseAttributeMappingTests
	{
		private class NestedMapParent
		{
			public string Name { get; set; }
			[ElasticProperty(Type=FieldType.nested)]
			public NestedMapChild Child { get; set; }
		}
		private class NestedMapChild
		{
			public string Name { get; set; }
		}
		[Test]
		public void TestNestedWriter()
		{ 
			var json = this.CreateMapFor<NestedMapParent>();
			this.JsonEquals(json, System.Reflection.MethodInfo.GetCurrentMethod());
		}
	}
}
