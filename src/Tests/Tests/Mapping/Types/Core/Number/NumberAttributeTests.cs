using System;
using Nest;

namespace Tests.Mapping.Types.Core.Number
{
	public class NumberTest
	{
		[Number(
			DocValues = true,
			Similarity = "classic",
			Store = true,
			Index = false,
			Boost = 1.5,
			NullValue = 0.0,
			IgnoreMalformed = true,
			Coerce = true,
			ScalingFactor = 10)]
		public double Full { get; set; }

		[Number]
		public double Minimal { get; set; }

		public byte Byte { get; set; }

		public short Short { get; set; }

		public int Integer { get; set; }

		public long Long { get; set; }

		public sbyte SignedByte { get; set; }

		public ushort UnsignedShort { get; set; }

		public uint UnsignedInteger { get; set; }

		public ulong UnsignedLong { get; set; }

		public float Float { get; set; }

		public double Double { get; set; }

		public decimal Decimal { get; set; }

		public TimeSpan TimeSpan { get; set; }
	}

	public class NumberAttributeTests
		: AttributeTestsBase<NumberTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				full = new
				{
					type = "float",
					doc_values = true,
					similarity = "classic",
					store = true,
					index = false,
					boost = 1.5,
					null_value = 0.0,
					ignore_malformed = true,
					coerce = true,
					scaling_factor = 10.0
				},
				minimal = new
				{
					type = "float"
				},
				@byte = new
				{
					type = "short"
				},
				@short = new
				{
					type = "short"
				},
				integer = new
				{
					type = "integer"
				},
				@long = new
				{
					type = "long"
				},
				signedByte = new
				{
					type = "byte"
				},
				unsignedShort = new
				{
					type = "integer"
				},
				unsignedInteger = new
				{
					type = "long"
				},
				unsignedLong = new
				{
					type = "double"
				},
				@float = new
				{
					type = "float"
				},
				@double = new
				{
					type = "double"
				},
				@decimal = new
				{
					type = "double"
				},
				timeSpan = new
				{
					type = "long"
				}
			}
		};
	}
}
