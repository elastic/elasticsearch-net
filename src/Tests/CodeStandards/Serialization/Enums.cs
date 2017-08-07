using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Nest;
using Newtonsoft.Json;
using Tests.Framework;

namespace Tests.CodeStandards.Serialization
{
	public class Enums
	{
		[U]
		public void AllNestEnumsHaveStringEnumConverterApplied()
		{
			var exceptions = new HashSet<Type>(new[]
			{
				typeof(GeoHashPrecision),
				typeof(VisitorScope),
				typeof(AggregationVisitorScope),
				typeof(CountFunction),
				typeof(NonZeroCountFunction),
				typeof(DistinctCountFunction),
				typeof(GeographicFunction),
				typeof(InfoContentFunction),
				typeof(MetricFunction),
				typeof(RareFunction),
				typeof(SumFunction),
				typeof(NonNullSumFunction),
				typeof(TimeFunction),
			});

			var enums =
				from type in typeof(ElasticClient).Assembly().GetExportedTypes()
				where type.IsEnumType()
				where !exceptions.Contains(type)
				where type.Namespace == "Nest"
#if DOTNETCORE
				let stringEnumConverterAttribute = type.GetTypeInfo().GetCustomAttribute<JsonConverterAttribute>()
#else
				let stringEnumConverterAttribute = type.GetCustomAttribute<JsonConverterAttribute>()
#endif
				where stringEnumConverterAttribute == null
				select type;

			enums.Should().BeEmpty();
		}
	}
}
