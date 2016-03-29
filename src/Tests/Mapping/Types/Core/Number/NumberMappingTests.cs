using System;
using Nest;

namespace Tests.Mapping.Types.Core.Number
{
	public class NumberTest
	{
		[Number(
			DocValues = true,
			IndexName = "myindex",
			Similarity = SimilarityOption.Classic,
			Store = true,
			Index = NonStringIndexOption.No,
			Boost = 1.5,
			NullValue = 0.0,
			IncludeInAll = false,
			PrecisionStep = 10,
			IgnoreMalformed = true,
			Coerce = true)]
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

	public class NumberMappingTests
		: TypeMappingTestBase<NumberTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				full = new
				{
					type = "double",
					doc_values = true,
					index_name = "myindex",
					similarity = "classic",
					store = true,
					index = "no",
					boost = 1.5,
					null_value = 0.0,
					include_in_all = false,
					precision_step = 10,
					ignore_malformed = true,
					coerce = true
				},
				minimal = new
				{
					type = "double"
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

	    protected override Func<PropertiesDescriptor<NumberTest>, IPromise<IProperties>> FluentProperties => m => m
	        .Number(d => d
	            .Name(o => o.Full)
	            .DocValues()
	            .IndexName("myindex")
	            .Similarity(SimilarityOption.Classic)
	            .Store()
	            .Index(NonStringIndexOption.No)
	            .Boost(1.5)
	            .NullValue(0.0)
	            .IncludeInAll(false)
	            .PrecisionStep(10)
	            .IgnoreMalformed()
	            .Coerce()
	        )
	        .Number(d => d
	            .Name(o => o.Minimal)
	        )
	        .Number(d => d
	            .Name(o => o.Byte)
                .Type(NumberType.Short)
	        )
	        .Number(d => d
	            .Name(o => o.Short)
                .Type(NumberType.Short)
            )
	        .Number(d => d
	            .Name(o => o.Integer)
                .Type(NumberType.Integer)
	        )
	        .Number(d => d
	            .Name(o => o.Long)
                .Type(NumberType.Long)
            )
	        .Number(d => d
	            .Name(o => o.SignedByte)
                .Type(NumberType.Byte)
            )
	        .Number(d => d
	            .Name(o => o.UnsignedShort)
                .Type(NumberType.Integer)
	        )
	        .Number(d => d
	            .Name(o => o.UnsignedInteger)
                .Type(NumberType.Long)
            )
	        .Number(d => d
	            .Name(o => o.UnsignedLong)
                .Type(NumberType.Double)
            )
	        .Number(d => d
	            .Name(o => o.Float)
                .Type(NumberType.Float)
            )
	        .Number(d => d
	            .Name(o => o.Double)
	        )
			.Number(d => d
	            .Name(o => o.Decimal)
	        )
			.Number(d => d
	            .Name(o => o.TimeSpan)
				.Type(NumberType.Long)
			);
	}
}
