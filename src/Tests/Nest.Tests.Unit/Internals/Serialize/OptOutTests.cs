using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using Newtonsoft.Json;

namespace Nest.Tests.Unit.Internals.Serialize
{
	[TestFixture]
	public class OptOutTests : BaseJsonTests
	{
		public class SimpleOptOutClass
		{
			public int Id { get; set; }
			public string Name { get; set; }
			[ElasticProperty(OptOut = true)]
			public string Description { get; set; }

			[JsonIgnore]
			public string IgnoreThisAsWell { get; set; }
		}

		[Test]
		public void OptOutDuringMapping()
		{
			var result = this._client.Map<SimpleOptOutClass>(m=>m.MapFromAttributes());
			var call = result.ConnectionStatus.Request;

			this.JsonEquals(call, MethodInfo.GetCurrentMethod());


		}

		[Test]
		public void OptOutDuringIndexing()
		{
			var obj = new SimpleOptOutClass
			{
				Id = 1,
				Name = "ABC",
				Description = "efghijklmnop",
				IgnoreThisAsWell = "qrstuvwxyz"
			};

			var result = this._client.Index(obj);
			var call = result.ConnectionStatus.Request;

			this.JsonEquals(call, MethodInfo.GetCurrentMethod());


		}
	}
}
