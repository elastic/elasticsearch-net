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
            public decimal DecimalField { get; set; }
            public SByte SignedByteField { get; set; }
            public UInt64 UnsignedLongField { get; set; }
            public UInt16 UnsignedShortField { get; set; }
            public UInt32 UnsignedIntegerField { get; set; }
			public DateTime DateTimeField { get; set; }
            public DateTimeOffset DateTimeOffsetField { get; set; }
            public bool BoolField { get; set; }
            public string StringField { get; set; }
            public Guid GuidField { get; set; }
            public char CharField { get; set; }
		}

		[Test]
		public void TestAllTypes()
		{
			var json = this.CreateMapFor<AllTypes>();
			this.JsonEquals(json, System.Reflection.MethodInfo.GetCurrentMethod());
		}
	}
}
