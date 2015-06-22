using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IRegexpFilter : IFieldNameFilter
	{
		[JsonProperty("value")]
		string Value { get; set; }

		[JsonProperty("flags")]
		string Flags { get; set; }

		[JsonProperty(PropertyName = "max_determinized_states")]
		int? MaximumDeterminizedStates { get; set; }

	}

	public class RegexpFilter : PlainFilter, IRegexpFilter
	{
		protected internal override void WrapInContainer(IFilterContainer container)
		{
			container.Regexp = this;
		}

		public string Value { get; set; }
		public string Flags { get; set; }
		public int? MaximumDeterminizedStates { get; set; }
		public PropertyPathMarker Field { get; set; }
	}

	public class RegexpFilterDescriptor<T> : FilterBase, IRegexpFilter where T : class
	{
		private IRegexpFilter Self { get { return this; }}

		string IRegexpFilter.Value { get; set; }

		string IRegexpFilter.Flags { get; set; }

		int? IRegexpFilter.MaximumDeterminizedStates { get; set; }

		PropertyPathMarker IFieldNameFilter.Field { get; set; }

		bool IFilter.IsConditionless
		{
			get
			{
				return Self.Field.IsConditionless() || Self.Value.IsNullOrEmpty();
			}
		}

		public RegexpFilterDescriptor<T> Value(string regex)
		{
			Self.Value = regex;
			return this;
		}
		public RegexpFilterDescriptor<T> Flags(string flags)
		{
			Self.Flags = flags;
			return this;
		}
		public RegexpFilterDescriptor<T> MaximumDeterminizedStates(int maxDeterminizedStates)
		{
			Self.MaximumDeterminizedStates = maxDeterminizedStates;
			return this;
		}
		public RegexpFilterDescriptor<T> OnField(string path)
		{
			Self.Field = path;
			return this;
		}
		public RegexpFilterDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			Self.Field = objectPath;
			return this;
		}

	}
}
