using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace Tests.Mapping.Types.Core.Date
{
	public class DateTest
	{
		[Date(
			DocValues = true, 
			IndexName = "myindex", 
			Similarity = SimilarityOption.Default, 
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
					index_name = "myindex",
					similarity = "default",
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
				}
			}
		};

		protected override Func<PropertiesDescriptor<DateTest>, IPromise<IProperties>> FluentProperties => m => m
			.Date(d => d
				.Name(o => o.Full)
				.IndexName("myindex")
				.Similarity(SimilarityOption.Default)
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
			);
	}
}
