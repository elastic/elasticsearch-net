using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Framework;

namespace Tests.Mapping.Scalar
{
	public class ScalarUsageTests : UsageTestBase<ITypeMapping, TypeMappingDescriptor<ScalarUsageTests.ScalarPoco>, TypeMapping>
	{
		protected override bool SupportsDeserialization => false;
		protected override bool TestObjectInitializer => false;

		public enum ScalarEnum { X }

		public class ScalarPoco
		{
			public int Int { get; set; }
			public IEnumerable<int> Ints { get; set; }
			public int? IntNullable { get; set; }
			public IEnumerable<int?> IntNullables { get; set; }

			public float Float { get; set; }
			public IEnumerable<float> Floats { get; set; }
			public float? FloatNullable { get; set; }
			public IEnumerable<float?> FloatNullables { get; set; }

			public double Double { get; set; }
			public IEnumerable<double> Doubles { get; set; }
			public double? DoubleNullable { get; set; }
			public IEnumerable<double?> DoubleNullables { get; set; }

			public sbyte SByte { get; set; }
			public IEnumerable<sbyte> SBytes { get; set; }
			public sbyte? SByteNullable { get; set; }
			public IEnumerable<sbyte?> SByteNullables { get; set; }

			public short Short { get; set; }
			public IEnumerable<short> Shorts { get; set; }
			public short? ShortNullable { get; set; }
			public IEnumerable<short?> ShortNullables { get; set; }

			public byte Byte { get; set; }
			public IEnumerable<byte> Bytes { get; set; }
			public byte? ByteNullable { get; set; }
			public IEnumerable<byte?> ByteNullables { get; set; }

			public long Long { get; set; }
			public IEnumerable<long> Longs { get; set; }
			public long? LongNullable { get; set; }
			public IEnumerable<long?> LongNullables { get; set; }

			public uint Uint { get; set; }
			public IEnumerable<uint> Uints { get; set; }
			public uint? UintNullable { get; set; }
			public IEnumerable<uint?> UintNullables { get; set; }

			public TimeSpan TimeSpan { get; set; }
			public IEnumerable<TimeSpan> TimeSpans { get; set; }
			public TimeSpan? TimeSpanNullable { get; set; }
			public IEnumerable<TimeSpan?> TimeSpanNullables { get; set; }

			public decimal Decimal { get; set; }
			public IEnumerable<decimal> Decimals {get;set; }
			public decimal? DecimalNullable { get; set; }
			public IEnumerable<decimal?> DecimalNullables { get; set; }

			public ulong Ulong { get; set; }
			public IEnumerable<ulong> Ulongs { get; set; }
			public ulong? UlongNullable { get; set; }
			public IEnumerable<ulong?> UlongNullables { get; set; }

			public DateTime DateTime { get; set; }
			public IEnumerable<DateTime> DateTimes { get; set; }
			public DateTime? DateTimeNullable { get; set; }
			public IEnumerable<DateTime?> DateTimeNullables { get; set; }

			public DateTimeOffset DateTimeOffset { get; set; }
			public IEnumerable<DateTimeOffset> DateTimeOffsets { get; set; }
			public DateTimeOffset? DateTimeOffsetNullable { get; set; }
			public IEnumerable<DateTimeOffset?> DateTimeOffsetNullables { get; set; }

			public bool Bool { get; set; }
			public IEnumerable<bool> Bools { get; set; }
			public bool? BoolNullable { get; set; }
			public IEnumerable<bool?> BoolNullables { get; set; }

			public char Char { get; set; }
			public IEnumerable<char> Chars { get; set; }
			public char? CharNullable { get; set; }
			public IEnumerable<char?> CharNullables { get; set; }

			public Guid Guid { get; set; }
			public IEnumerable<Guid> Guids { get; set; }
			public Guid? GuidNullable { get; set; }
			public IEnumerable<Guid?> GuidNullables { get; set; }

			public string String { get; set; }
			public IEnumerable<string> Strings { get; set; }

			public ScalarEnum Enum { get; set; }

			public DateRange DateRange { get; set; }
			public DoubleRange DoubleRange { get; set; }
			public IntegerRange IntegerRange { get; set; }
			public FloatRange FloatRange { get; set; }
			public LongRange LongRange { get; set; }
		}

		protected override object ExpectJson => new
		{
			properties = new
			{
				@bool = new { type = "boolean" },
				bools = new { type = "boolean" },
				boolNullable = new { type = "boolean" },
				boolNullables = new { type = "boolean" },
				@byte = new { type = "short" },
				bytes = new { type = "short" },
				byteNullable = new { type = "short" },
				byteNullables = new { type = "short" },
				@char = new { type = "keyword" },
				chars = new { type = "keyword" },
				charNullable = new { type = "keyword" },
				charNullables = new { type = "keyword" },
				dateTime = new { type = "date" },
				dateTimes = new { type = "date" },
				dateTimeNullable = new { type = "date" },
				dateTimeNullables = new { type = "date" },
				dateTimeOffset = new { type = "date" },
				dateTimeOffsets = new { type = "date" },
				dateTimeOffsetNullable = new { type = "date" },
				dateTimeOffsetNullables = new { type = "date" },
				@decimal = new { type = "double" },
				decimals = new { type = "double" },
				decimalNullable = new { type = "double" },
				decimalNullables = new { type = "double" },
				@double = new { type = "double" },
				doubles = new { type = "double" },
				doubleNullable = new { type = "double" },
				doubleNullables = new { type = "double" },
				@float = new { type = "float" },
				floats = new { type = "float" },
				floatNullable = new { type = "float" },
				floatNullables = new { type = "float" },
				guid = new { type = "keyword" },
				guids = new { type = "keyword" },
				guidNullable = new { type = "keyword" },
				guidNullables = new { type = "keyword" },
				@int = new { type = "integer" },
				ints = new { type = "integer" },
				intNullable = new { type = "integer" },
				intNullables = new { type = "integer" },
				@long = new { type = "long" },
				longs = new { type = "long" },
				longNullable = new { type = "long" },
				longNullables = new { type = "long" },
				sByte = new { type = "byte" },
				sBytes = new { type = "byte" },
				sByteNullable = new { type = "byte" },
				sByteNullables = new { type = "byte" },
				@short = new { type = "short" },
				shorts = new { type = "short" },
				shortNullable = new { type = "short" },
				shortNullables = new { type = "short" },
				timeSpan = new { type = "long" },
				timeSpans = new { type = "long" },
				timeSpanNullable = new { type = "long" },
				timeSpanNullables = new { type = "long" },
				@uint = new { type = "long" },
				uints = new { type = "long" },
				uintNullable = new { type = "long" },
				uintNullables = new { type = "long" },
				@ulong = new { type = "double" },
				ulongs = new { type = "double" },
				ulongNullable = new { type = "double" },
				ulongNullables = new { type = "double" },
				@string = new { type = "text" },
				strings = new { type = "text" },
				@enum = new { type = "integer" },
				dateRange = new { type = "date_range" },
				integerRange = new { type = "integer_range" },
				doubleRange = new { type = "double_range" },
				longRange = new { type = "long_range" },
				floatRange = new { type = "float_range" }
			}
		};

		protected override Func<TypeMappingDescriptor<ScalarPoco>, ITypeMapping> Fluent => f => f
			.Properties(ps => ps
				.Scalar(p => p.Int, m => m)
				.Scalar(p => p.Ints, m => m)
				.Scalar(p => p.IntNullable, m => m)
				.Scalar(p => p.IntNullables, m => m)
				.Scalar(p => p.Float, m => m)
				.Scalar(p => p.Floats, m => m)
				.Scalar(p => p.FloatNullable, m => m)
				.Scalar(p => p.FloatNullables, m => m)
				.Scalar(p => p.Double, m => m)
				.Scalar(p => p.Doubles, m => m)
				.Scalar(p => p.DoubleNullable, m => m)
				.Scalar(p => p.DoubleNullables, m => m)
				.Scalar(p => p.SByte, m => m)
				.Scalar(p => p.SBytes, m => m)
				.Scalar(p => p.SByteNullable, m => m)
				.Scalar(p => p.SByteNullables, m => m)
				.Scalar(p => p.Short, m => m)
				.Scalar(p => p.Shorts, m => m)
				.Scalar(p => p.ShortNullable, m => m)
				.Scalar(p => p.ShortNullables, m => m)
				.Scalar(p => p.Byte, m => m)
				.Scalar(p => p.Bytes, m => m)
				.Scalar(p => p.ByteNullable, m => m)
				.Scalar(p => p.ByteNullables, m => m)
				.Scalar(p => p.Long, m => m)
				.Scalar(p => p.Longs, m => m)
				.Scalar(p => p.LongNullable, m => m)
				.Scalar(p => p.LongNullables, m => m)
				.Scalar(p => p.Uint, m => m)
				.Scalar(p => p.Uints, m => m)
				.Scalar(p => p.UintNullable, m => m)
				.Scalar(p => p.UintNullables, m => m)
				.Scalar(p => p.TimeSpan, m => m)
				.Scalar(p => p.TimeSpans, m => m)
				.Scalar(p => p.TimeSpanNullable, m => m)
				.Scalar(p => p.TimeSpanNullables, m => m)
				.Scalar(p => p.Decimal, m => m)
				.Scalar(p => p.Decimals, m => m)
				.Scalar(p => p.DecimalNullable, m => m)
				.Scalar(p => p.DecimalNullables, m => m)
				.Scalar(p => p.Ulong, m => m)
				.Scalar(p => p.Ulongs, m => m)
				.Scalar(p => p.UlongNullable, m => m)
				.Scalar(p => p.UlongNullables, m => m)
				.Scalar(p => p.DateTime, m => m)
				.Scalar(p => p.DateTimes, m => m)
				.Scalar(p => p.DateTimeNullable, m => m)
				.Scalar(p => p.DateTimeNullables, m => m)
				.Scalar(p => p.DateTimeOffset, m => m)
				.Scalar(p => p.DateTimeOffsets, m => m)
				.Scalar(p => p.DateTimeOffsetNullable, m => m)
				.Scalar(p => p.DateTimeOffsetNullables, m => m)
				.Scalar(p => p.Bool, m => m)
				.Scalar(p => p.Bools, m => m)
				.Scalar(p => p.BoolNullable, m => m)
				.Scalar(p => p.BoolNullables, m => m)
				.Scalar(p => p.Char, m => m)
				.Scalar(p => p.Chars, m => m)
				.Scalar(p => p.CharNullable, m => m)
				.Scalar(p => p.CharNullables, m => m)
				.Scalar(p => p.Guid, m => m)
				.Scalar(p => p.Guids, m => m)
				.Scalar(p => p.GuidNullable, m => m)
				.Scalar(p => p.GuidNullables, m => m)
				.Scalar(p => p.String, m => m)
				.Scalar(p => p.Strings, m => m)
				.Scalar(p => p.Enum, m => m)
				.Scalar(p => p.DateRange, m => m)
				.Scalar(p => p.IntegerRange, m => m)
				.Scalar(p => p.FloatRange, m => m)
				.Scalar(p => p.LongRange, m => m)
				.Scalar(p => p.DoubleRange, m => m)
			);

		protected override TypeMapping Initializer => null;
	}
}
