using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters.Filters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(TermsFilterConverter))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ITermsBaseFilter : IFilter
	{
		PropertyPathMarker Field { get; set; }
		
		[JsonProperty("execution")]
		TermsExecution? Execution { get; set; }
	}

	public interface ITermsFilter : ITermsBaseFilter
	{
		IEnumerable<object> Terms { get; set; }
	}

	public class TermsFilter : PlainFilter, ITermsFilter
	{
		protected internal override void WrapInContainer(IFilterContainer container)
		{
			container.Terms = this;
		}

		public PropertyPathMarker Field { get; set; }
		public TermsExecution? Execution { get; set; }
		

		public IEnumerable<object> Terms { get; set; }
	}

	public class TermsFilterDescriptor : FilterBase, ITermsFilter
	{
		bool IFilter.IsConditionless
		{
			get
			{
				return ((ITermsBaseFilter)this).Field.IsConditionless()
					   || !((ITermsFilter)this).Terms.HasAny()
					   || ((ITermsFilter)this).Terms.All(t => t is string && ((string)t).IsNullOrEmpty())
					   || ((ITermsFilter)this).Terms.All(t => t == null);
			}
		}

		PropertyPathMarker ITermsBaseFilter.Field { get; set; }
		IEnumerable<object> ITermsFilter.Terms { get; set; }
		TermsExecution? ITermsBaseFilter.Execution { get; set; }

	}
}