using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers;
using Nest.Resolvers.Converters;
using Nest.Resolvers.Converters.Filters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(CompositeJsonConverter<TermsFilterJsonReader, CustomJsonConverter>))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ITermsBaseFilter : IFilterBase
	{
		PropertyPathMarker Field { get; set; }
		
		[JsonProperty("execution")]
		TermsExecution? Execution { get; set; }
	}

	[JsonConverter(typeof(CompositeJsonConverter<TermsFilterJsonReader, CustomJsonConverter>))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ITermsFilter : ITermsBaseFilter, ICustomJson
	{
		IEnumerable<object> Terms { get; set; }
	}

	public class TermsFilter : FilterBase, ITermsFilter
	{
		bool IFilterBase.IsConditionless
		{
			get
			{
				return ((ITermsBaseFilter)this).Field.IsConditionless()
					   || !((ITermsFilter)this).Terms.HasAny()
					   || ((ITermsFilter)this).Terms.OfType<string>().All(s => s.IsNullOrEmpty())
					   || ((ITermsFilter)this).Terms.All(t => t == null);
			}
		}

		PropertyPathMarker ITermsBaseFilter.Field { get; set; }
		IEnumerable<object> ITermsFilter.Terms { get; set; }
		TermsExecution? ITermsBaseFilter.Execution { get; set; }

		object ICustomJson.GetCustomJson()
		{
			var tf = ((ITermsFilter)this);
			return this.FieldNameAsKeyFormat(tf.Field, tf.Terms, d => d
				.Add("execution", tf.Execution)
			);
		}
	}
}