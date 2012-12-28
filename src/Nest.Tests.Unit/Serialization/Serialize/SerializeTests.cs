using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Nest;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Serialization.Serialize
{
	[TestFixture]
	public class SerializeTests : BaseJsonTests
	{
		public class SimpleClass
		{
			public int Id { get; set; }
			public string Name { get; set; }
		}


		[Test]
		public void SimpleClassUsesCamelCase()
		{
			var simpleClass = new SimpleClass { Id = 2, Name = "X" };
			var json = this._client.SerializeCamelCase(simpleClass);
			this.JsonEquals(json, MethodInfo.GetCurrentMethod());
		}
	}
}
