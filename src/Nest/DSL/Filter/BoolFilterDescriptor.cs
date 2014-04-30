using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Elasticsearch.Net;

namespace Nest
{
	internal static class BoolBaseFilterDescriptorExtensions
	{
		internal static bool CanMergeMustAndMustNots(this IBoolFilter bq)
		{
			return bq == null || !((IBoolFilter)bq).Should.HasAny();
		}

		internal static bool CanJoinShould(this IBoolFilter bq)
		{
			var bf = (IBoolFilter)bq;
			return bq == null 
				|| (
					(bf.Should.HasAny() && !bf.Must.HasAny() && !bf.MustNot.HasAny())
					|| !bf.Should.HasAny()
				);
		}
		
		internal static IEnumerable<IFilterDescriptor> MergeShouldFilters(this IFilterDescriptor lbq, IFilterDescriptor rbq)
		{
			var lBoolDescriptor = lbq.BoolFilterDescriptor;
			var lHasShouldFilters = lBoolDescriptor != null &&
			  ((IBoolFilter)lBoolDescriptor).Should.HasAny();

			var rBoolDescriptor = rbq.BoolFilterDescriptor;
			var rHasShouldFilters = rBoolDescriptor != null &&
			  ((IBoolFilter)rBoolDescriptor).Should.HasAny();


			var lq = lHasShouldFilters ? ((IBoolFilter)lBoolDescriptor).Should : new[] { lbq };
			var rq = rHasShouldFilters ? ((IBoolFilter)rBoolDescriptor).Should : new[] { rbq };

			return lq.Concat(rq);
		}
	}

	[JsonConverter(typeof(ReadAsTypeConverter<BoolBaseFilterDescriptor>))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IBoolFilter : IFilterBase
	{
		[JsonProperty("must")]
		IEnumerable<IFilterDescriptor> Must { get; set; }

		[JsonProperty("must_not")]
		IEnumerable<IFilterDescriptor> MustNot { get; set; }

		[JsonProperty("should")]
		IEnumerable<IFilterDescriptor> Should { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class BoolBaseFilterDescriptor : FilterBase , IBoolFilter
	{
		IEnumerable<IFilterDescriptor> IBoolFilter.Must { get; set; }

		IEnumerable<IFilterDescriptor> IBoolFilter.MustNot { get; set; }

		IEnumerable<IFilterDescriptor> IBoolFilter.Should { get; set; }

		bool IFilterBase.IsConditionless
		{
			get
			{
				var bf = (IBoolFilter)this;
				if (!bf.Must.HasAny() && !bf.Should.HasAny() && !bf.MustNot.HasAny())
					return true;
				return (bf.MustNot.HasAny() && bf.MustNot.All(q => q.IsConditionless))
					   || (bf.Should.HasAny() && bf.Should.All(q => q.IsConditionless))
					   || (bf.Must.HasAny() && bf.Must.All(q => q.IsConditionless));
			}
		}
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class BoolFilterDescriptor<T> : BoolBaseFilterDescriptor where T : class
	{
		public BoolFilterDescriptor<T> Must(params Func<FilterDescriptorDescriptor<T>, BaseFilterDescriptor>[] filters)
		{
			var descriptors = new List<BaseFilterDescriptor>();
			foreach (var selector in filters)
			{
				var filter = new FilterDescriptorDescriptor<T>();
				var f = selector(filter);
				if (f.IsConditionless)
					continue;
				descriptors.Add(f);
			}
			((IBoolFilter)this).Must = descriptors;
			return this;
		}

		public BoolFilterDescriptor<T> Must(params BaseFilterDescriptor[] filtersDescriptor)
		{
			var descriptors = new List<BaseFilterDescriptor>();
			foreach (var f in filtersDescriptor)
			{
				if (f.IsConditionless)
					continue;
				descriptors.Add(f);
			}
			((IBoolFilter)this).Must = descriptors;
			return this;
		}

		public BoolFilterDescriptor<T> MustNot(params Func<FilterDescriptorDescriptor<T>, BaseFilterDescriptor>[] filters)
		{
			var descriptors = new List<BaseFilterDescriptor>();
			foreach (var selector in filters)
			{
				var filter = new FilterDescriptorDescriptor<T>();
				var f = selector(filter);
				if (f.IsConditionless)
					continue;
				descriptors.Add(f);
			}
			((IBoolFilter)this).MustNot = descriptors;
			return this;
		}

		public BoolFilterDescriptor<T> MustNot(params BaseFilterDescriptor[] filtersDescriptor)
		{
			var descriptors = new List<BaseFilterDescriptor>();
			foreach (var f in filtersDescriptor)
			{
				if (f.IsConditionless)
					continue;
				descriptors.Add(f);
			}
			((IBoolFilter)this).MustNot = descriptors;
			return this;
		}
	
		public BoolFilterDescriptor<T> Should(params Func<FilterDescriptorDescriptor<T>, BaseFilterDescriptor>[] filters)
		{
			var descriptors = new List<BaseFilterDescriptor>();
			foreach (var selector in filters)
			{
				var filter = new FilterDescriptorDescriptor<T>();
				var f = selector(filter);
				if (f.IsConditionless)
					continue;
				descriptors.Add(f);
			}
			((IBoolFilter)this).Should = descriptors;
			return this;
		}

		public BoolFilterDescriptor<T> Should(params BaseFilterDescriptor[] filtersDescriptor)
		{
			var descriptors = new List<BaseFilterDescriptor>();
			foreach (var f in filtersDescriptor)
			{
				if (f.IsConditionless)
					continue;
				descriptors.Add(f);
			}
			((IBoolFilter)this).Should = descriptors;
			return this;
		}
	}
}
