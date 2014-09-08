using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Tests.Unit.Reproduce
{
	class SomeClass
	{
		public SomeClass()
		{
			this.Data = new Dictionary<long, SomeOtherClass>();
		}

		public long ID { get; set; }

		[ElasticProperty(Type = FieldType.Object)]
		public Dictionary<long, SomeOtherClass> Data { get; set; }
	}

	class SomeOtherClass
	{
		public string Value1 { get; set; }
		public string Value2 { get; set; }
	}

	[TestFixture]
	public class Reproduce926Tests : BaseJsonTests
	{
		[Test]
		public void ObjectMappingOnDictionaryCausesArgumentException()
		{
			var mapResult = this._client.Map<SomeClass>(m => m
				.MapFromAttributes());
		}
	}
}
