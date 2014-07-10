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

	}

	public class RegexpFilter : PlainFilter, IRegexpFilter
	{
		protected internal override void WrapInContainer(IFilterContainer container)
		{
			container.Regexp = this;
		}

		public string Value { get; set; }
		public string Flags { get; set; }
		public PropertyPathMarker Field { get; set; }
	}

	public class RegexpFilterDescriptor<T> : FilterBase, IRegexpFilter where T : class
	{
		string IRegexpFilter.Value { get; set; }

		string IRegexpFilter.Flags { get; set; }

		PropertyPathMarker IFieldNameFilter.Field { get; set; }

		bool IFilter.IsConditionless
		{
			get
			{
				return ((IRegexpFilter)this).Field.IsConditionless() || ((IRegexpFilter)this).Value.IsNullOrEmpty();
			}
		}

		public RegexpFilterDescriptor<T> Value(string regex)
		{
			((IRegexpFilter)this).Value = regex;
			return this;
		}
		public RegexpFilterDescriptor<T> Flags(string flags)
		{
			((IRegexpFilter)this).Flags = flags;
			return this;
		}
		public RegexpFilterDescriptor<T> OnField(string path)
		{
			((IRegexpFilter)this).Field = path;
			return this;
		}
		public RegexpFilterDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			((IRegexpFilter)this).Field = objectPath;
			return this;
		}

	}
}
