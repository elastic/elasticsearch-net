using System;
using Nest;

namespace Tests.Mapping.Types.Core.Date
{
	public class DateTest
	{
		[Date(
			DocValues = true,
			IndexName = "myindex",
			Similarity = SimilarityOption.Classic,
			Store = true,
			Index = NonStringIndexOption.No,
			Boost = 1.2,
			IncludeInAll = false,
			PrecisionStep = 5,
			IgnoreMalformed = true,
			Format = "MM/dd/yyyy",
			NumericResolution = NumericResolutionUnit.Milliseconds)]
		public DateTime Full { get; set; }

		[Date]
		public DateTime Minimal { get; set; }

        public DateTime Inferred { get; set; }

        public DateTimeOffset InferredOffset { get; set; }
	}

	public class DateMappingTests : TypeMappingTestBase<DateTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				full = new
				{
					type = "date",
					doc_values = true,
					index_name = "myindex",
					similarity = "classic",
					store = true,
					index = "no",
					boost = 1.2,
					include_in_all = false,
					precision_step = 5,
					ignore_malformed = true,
					format = "MM/dd/yyyy",
					numeric_resolution = "milliseconds"
				},
				minimal = new
				{
					type = "date"
				},
                inferred = new
                {
                    type = "date"
                },
                inferredOffset = new
                {
                    type = "date"
                }
			}
		};

		protected override Func<PropertiesDescriptor<DateTest>, IPromise<IProperties>> FluentProperties => m => m
			.Date(d => d
				.Name(o => o.Full)
				.DocValues()
				.IndexName("myindex")
				.Similarity(SimilarityOption.Classic)
				.Store()
				.Index(NonStringIndexOption.No)
				.Boost(1.2)
				.IncludeInAll(false)
				.PrecisionStep(5)
				.IgnoreMalformed()
				.Format("MM/dd/yyyy")
				.NumericResolution(NumericResolutionUnit.Milliseconds)
			)
			.Date(d => d
				.Name(o => o.Minimal)
			)
            .Date(d => d
				.Name(o => o.Inferred)
			)
            .Date(d => d
                .Name(o => o.InferredOffset)
            );
	}
}
