using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Nest.Tests.Unit.Core.AttributeBasedMap
{
	[TestFixture]
	public class AllTypesTest : BaseAttributeMappingTests
	{
		public class AllTypes
		{
			public int IntegerField { get; set; }
			public short ShortField { get; set; }
			public byte ByteField { get; set; }
			public long LongField { get; set; }
			public float FloatField { get; set; }
			public double DoubleField { get; set; }
			public DateTime DateField { get; set; }
			public bool BoolField { get; set; }
		}

		[Test]
		public void TestAllTypes()
		{
			var json = this.CreateMapFor<AllTypes>();
			this.JsonEquals(json, System.Reflection.MethodInfo.GetCurrentMethod());
		}
	}
}
