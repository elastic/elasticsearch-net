using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace Tests.Mapping.Types.Core.Number
{
	public class NumberTest
	{
		[Number(
			DocValues = true,
			IndexName = "myindex",
			Similarity = SimilarityOption.Default,
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
					similarity = "default",
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
				}
			}
		};

		protected override Func<PropertiesDescriptor<NumberTest>, IPromise<IProperties>> FluentProperties => m => m
			.Number(d => d
				.Name(o => o.Full)
				.DocValues()
				.IndexName("myindex")
				.Similarity(SimilarityOption.Default)
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
			);
	}
}
