using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Elasticsearch.Net;

namespace Nest
{
	internal static class BoolBaseFilterDescriptorExtensions
	{
		internal static bool CanMergeMustAndMustNots(this BoolBaseFilterDescriptor bq)
		{
			return bq == null || !bq._ShouldFilters.HasAny();
		}

		internal static bool CanJoinShould(this BoolBaseFilterDescriptor bq)
		{
			return bq == null 
				|| (
					(bq._ShouldFilters.HasAny() && !bq._MustFilters.HasAny() && !bq._MustNotFilters.HasAny())
					|| !bq._ShouldFilters.HasAny()
				);
		}
		
		internal static IEnumerable<BaseFilter> MergeShouldFilters(this BaseFilter lbq, BaseFilter rbq)
		{
			var lBoolDescriptor = lbq.BoolFilterDescriptor;
			var lHasShouldFilters = lBoolDescriptor != null &&
			  lBoolDescriptor._ShouldFilters.HasAny();

			var rBoolDescriptor = rbq.BoolFilterDescriptor;
			var rHasShouldFilters = rBoolDescriptor != null &&
			  rBoolDescriptor._ShouldFilters.HasAny();


			var lq = lHasShouldFilters ? lBoolDescriptor._ShouldFilters : new[] { lbq };
			var rq = rHasShouldFilters ? rBoolDescriptor._ShouldFilters : new[] { rbq };

			return lq.Concat(rq);
		}
	}


	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class BoolBaseFilterDescriptor : FilterBase
	{
		[JsonProperty("must")]
		internal IEnumerable<BaseFilter> _MustFilters { get; set; }

		[JsonProperty("must_not")]
		internal IEnumerable<BaseFilter> _MustNotFilters { get; set; }

		[JsonProperty("should")]
		internal IEnumerable<BaseFilter> _ShouldFilters { get; set; }

		internal override bool IsConditionless
		{
			get
			{
				if (!this._MustFilters.HasAny() && !this._ShouldFilters.HasAny() && !this._MustNotFilters.HasAny())
					return true;
				return (this._MustNotFilters.HasAny() && this._MustNotFilters.All(q => q.IsConditionless))
					   || (this._ShouldFilters.HasAny() && this._ShouldFilters.All(q => q.IsConditionless))
					   || (this._MustFilters.HasAny() && this._MustFilters.All(q => q.IsConditionless));
			}
		}
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class BoolFilterDescriptor<T> : BoolBaseFilterDescriptor where T : class
	{
		public BoolFilterDescriptor<T> Must(params Func<FilterDescriptor<T>, BaseFilter>[] filters)
		{
			var descriptors = new List<BaseFilter>();
			foreach (var selector in filters)
			{
				var filter = new FilterDescriptor<T>();
				var f = selector(filter);
				if (f.IsConditionless)
					continue;
				descriptors.Add(f);
			}
			this._MustFilters = descriptors;
			return this;
		}

		public BoolFilterDescriptor<T> Must(params BaseFilter[] filters)
		{
			var descriptors = new List<BaseFilter>();
			foreach (var f in filters)
			{
				if (f.IsConditionless)
					continue;
				descriptors.Add(f);
			}
			this._MustFilters = descriptors;
			return this;
		}

		public BoolFilterDescriptor<T> MustNot(params Func<FilterDescriptor<T>, BaseFilter>[] filters)
		{
			var descriptors = new List<BaseFilter>();
			foreach (var selector in filters)
			{
				var filter = new FilterDescriptor<T>();
				var f = selector(filter);
				if (f.IsConditionless)
					continue;
				descriptors.Add(f);
			}
			this._MustNotFilters = descriptors;
			return this;
		}

		public BoolFilterDescriptor<T> MustNot(params BaseFilter[] filters)
		{
			var descriptors = new List<BaseFilter>();
			foreach (var f in filters)
			{
				if (f.IsConditionless)
					continue;
				descriptors.Add(f);
			}
			this._MustNotFilters = descriptors;
			return this;
		}
	
		public BoolFilterDescriptor<T> Should(params Func<FilterDescriptor<T>, BaseFilter>[] filters)
		{
			var descriptors = new List<BaseFilter>();
			foreach (var selector in filters)
			{
				var filter = new FilterDescriptor<T>();
				var f = selector(filter);
				if (f.IsConditionless)
					continue;
				descriptors.Add(f);
			}
			this._ShouldFilters = descriptors;
			return this;
		}

		public BoolFilterDescriptor<T> Should(params BaseFilter[] filters)
		{
			var descriptors = new List<BaseFilter>();
			foreach (var f in filters)
			{
				if (f.IsConditionless)
					continue;
				descriptors.Add(f);
			}
			this._ShouldFilters = descriptors;
			return this;
		}
	}
}
