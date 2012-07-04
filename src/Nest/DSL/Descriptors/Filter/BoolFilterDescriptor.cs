using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Nest
{
	[JsonObject(MemberSerialization=MemberSerialization.OptIn)]
	internal class BoolBaseFilterDescriptor : FilterBase
	{
		[JsonProperty("must")]
		internal IEnumerable<BaseFilter> _MustFilters { get; set; }

		[JsonProperty("should")]
		internal IEnumerable<BaseFilter> _ShouldFilters { get; set; }
	}

	[JsonObject(MemberSerialization=MemberSerialization.OptIn)]
	public class BoolFilterDescriptor<T> : FilterBase where T : class
	{
		[JsonProperty("must")]
		internal IEnumerable<FilterDescriptor<T>> _MustFilters { get; set; }

		[JsonProperty("must_not")]
		internal IEnumerable<FilterDescriptor<T>> _MustNotFilters { get; set; }

		[JsonProperty("should")]
		internal IEnumerable<FilterDescriptor<T>> _ShouldFilters { get; set; }

		public BoolFilterDescriptor<T> Must(params Action<FilterDescriptor<T>>[] filters)
		{
			var descriptors = new List<FilterDescriptor<T>>();
			foreach (var selector in filters)
			{
				var filter = new FilterDescriptor<T>();
				selector(filter);
				descriptors.Add(filter);
			}
			this._MustFilters = descriptors;
			return this;
		}
		public BoolFilterDescriptor<T> MustNot(params Action<FilterDescriptor<T>>[] filters)
		{
			var descriptors = new List<FilterDescriptor<T>>();
			foreach (var selector in filters)
			{
				var filter = new FilterDescriptor<T>();
				selector(filter);
				descriptors.Add(filter);
			}
			this._MustNotFilters = descriptors;
			return this;
		}
		public BoolFilterDescriptor<T> Should(params Action<FilterDescriptor<T>>[] filters)
		{
			var descriptors = new List<FilterDescriptor<T>>();
			foreach (var selector in filters)
			{
				var filter = new FilterDescriptor<T>();
				selector(filter);
				descriptors.Add(filter);
			}
			this._ShouldFilters = descriptors;
			return this;
		}
	}
}
