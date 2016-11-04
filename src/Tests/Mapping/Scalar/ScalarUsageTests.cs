using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Framework;
using Tests.Framework.MockData;

namespace Tests.Mapping.Scalar
{
	public class ScalarUsageTests : UsageTestBase<ITypeMapping, TypeMappingDescriptor<ScalarUsageTests.ScalarPoco>, TypeMapping>
	{
		protected override bool SupportsDeserialization => false;
		protected override bool TestObjectInitializer => false;

		public class ScalarPoco
		{
			public int Int { get; set; }
			public int? IntNullable { get; set; }

			public float Float { get; set; }
			public float? FloatNullable { get; set; }

			public double Double { get; set; }
			public double? DoubleNullable { get; set; }

			public sbyte SByte { get; set; }
			public sbyte? SByteNullable { get; set; }

			public short Short { get; set; }
			public short? ShortNullable { get; set; }

			public byte Byte { get; set; }
			public byte? ByteNullable { get; set; }

			public long Long { get; set; }
			public long? LongNullable { get; set; }

			public uint Uint { get; set; }
			public uint? UintNullable { get; set; }

			public TimeSpan TimeSpan { get; set; }
			public TimeSpan? TimeSpanNullable { get; set; }

			public decimal Decimal { get; set; }
			public decimal? DecimalNullable { get; set; }

			public ulong Ulong { get; set; }
			public ulong? UlongNullable { get; set; }

			public DateTime DateTime { get; set; }
			public DateTime? DateTimeNullable { get; set; }

			public DateTimeOffset DateTimeOffset { get; set; }
			public DateTimeOffset? DateTimeOffsetNullable { get; set; }

			public bool Bool { get; set; }
			public bool? BoolNullable { get; set; }

			public char Char { get; set; }
			public char? CharNullable { get; set; }

			public Guid Guid { get; set; }
			public Guid? GuidNullable { get; set; }

			public string String { get; set; }

		}

		protected override object ExpectJson => new
		{
			properties = new
			{
				@bool = new { type = "boolean" },
				boolNullable = new { type = "boolean" },
				@byte = new { type = "short" },
				byteNullable = new { type = "short" },
				@char = new { type = "keyword" },
				charNullable = new { type = "keyword" },
				dateTime = new { type = "date" },
				dateTimeNullable = new { type = "date" },
				dateTimeOffset = new { type = "date" },
				dateTimeOffsetNullable = new { type = "date" },
				@decimal = new { type = "double" },
				decimalNullable = new { type = "double" },
				@double = new { type = "double" },
				doubleNullable = new { type = "double" },
				@float = new { type = "float" },
				floatNullable = new { type = "float" },
				guid = new { type = "keyword" },
				guidNullable = new { type = "keyword" },
				@int = new { type = "integer" },
				intNullable = new { type = "integer" },
				@long = new { type = "long" },
				longNullable = new { type = "long" },
				sByte = new { type = "byte" },
				sByteNullable = new { type = "byte" },
				@short = new { type = "short" },
				shortNullable = new { type = "short" },
				@string = new { type = "text" },
				timeSpan = new { type = "long" },
				timeSpanNullable = new { type = "long" },
				@uint = new { type = "long" },
				uintNullable = new { type = "long" },
				@ulong = new { type = "double" },
				ulongNullable = new { type = "double" }
			}
		};

		protected override Func<TypeMappingDescriptor<ScalarPoco>, ITypeMapping> Fluent => f => f
			.Properties(ps => ps
				.Scalar(p => p.Int, m => m)
				.Scalar(p => p.IntNullable, m => m)
				.Scalar(p => p.Float, m => m)
				.Scalar(p => p.FloatNullable, m => m)
				.Scalar(p => p.Double, m => m)
				.Scalar(p => p.DoubleNullable, m => m)
				.Scalar(p => p.SByte, m => m)
				.Scalar(p => p.SByteNullable, m => m)
				.Scalar(p => p.Short, m => m)
				.Scalar(p => p.ShortNullable, m => m)
				.Scalar(p => p.Byte, m => m)
				.Scalar(p => p.ByteNullable, m => m)
				.Scalar(p => p.Long, m => m)
				.Scalar(p => p.LongNullable, m => m)
				.Scalar(p => p.Uint, m => m)
				.Scalar(p => p.UintNullable, m => m)
				.Scalar(p => p.TimeSpan, m => m)
				.Scalar(p => p.TimeSpanNullable, m => m)
				.Scalar(p => p.Decimal, m => m)
				.Scalar(p => p.DecimalNullable, m => m)
				.Scalar(p => p.Ulong, m => m)
				.Scalar(p => p.UlongNullable, m => m)
				.Scalar(p => p.DateTime, m => m)
				.Scalar(p => p.DateTimeNullable, m => m)
				.Scalar(p => p.DateTimeOffset, m => m)
				.Scalar(p => p.DateTimeOffsetNullable, m => m)
				.Scalar(p => p.Bool, m => m)
				.Scalar(p => p.BoolNullable, m => m)
				.Scalar(p => p.Char, m => m)
				.Scalar(p => p.CharNullable, m => m)
				.Scalar(p => p.Guid, m => m)
				.Scalar(p => p.GuidNullable, m => m)
				.Scalar(p => p.String, m => m)
			);

		protected override TypeMapping Initializer => null;
	}
}
