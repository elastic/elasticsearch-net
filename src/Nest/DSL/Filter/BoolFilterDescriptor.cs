using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeConverter<BoolBaseFilterDescriptor>))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IBoolFilter : IFilter
	{
		[JsonProperty("must",
			ItemConverterType = typeof(CompositeJsonConverter<ReadAsTypeConverter<FilterContainer>, CustomJsonConverter>))]
		IEnumerable<IFilterContainer> Must { get; set; }

		[JsonProperty("must_not",
			ItemConverterType = typeof(CompositeJsonConverter<ReadAsTypeConverter<FilterContainer>, CustomJsonConverter>))]
		IEnumerable<IFilterContainer> MustNot { get; set; }

		[JsonProperty("should",
			ItemConverterType = typeof(CompositeJsonConverter<ReadAsTypeConverter<FilterContainer>, CustomJsonConverter>))]
		IEnumerable<IFilterContainer> Should { get; set; }
	}

	public class BoolFilter : PlainFilter, IBoolFilter
	{
		protected internal override void WrapInContainer(IFilterContainer container)
		{
			container.Bool = this;
		}

		public IEnumerable<IFilterContainer> Must { get; set; }
		public IEnumerable<IFilterContainer> MustNot { get; set; }
		public IEnumerable<IFilterContainer> Should { get; set; }
	}

	public class BoolBaseFilterDescriptor : FilterBase , IBoolFilter
	{
		IEnumerable<IFilterContainer> IBoolFilter.Must { get; set; }

		IEnumerable<IFilterContainer> IBoolFilter.MustNot { get; set; }

		IEnumerable<IFilterContainer> IBoolFilter.Should { get; set; }

		bool IFilter.IsConditionless
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

	public class BoolFilterDescriptor<T> : BoolBaseFilterDescriptor where T : class
	{
		public BoolFilterDescriptor<T> Must(params Func<FilterDescriptor<T>, FilterContainer>[] filters)
		{
			var descriptors = new List<FilterContainer>();
			foreach (var selector in filters)
			{
				var filter = new FilterDescriptor<T>();
				var f = selector(filter);
				if (f.IsConditionless)
					continue;
				descriptors.Add(f);
			}
			((IBoolFilter)this).Must = descriptors;
			return this;
		}

		public BoolFilterDescriptor<T> Must(params FilterContainer[] filtersDescriptor)
		{
			var descriptors = new List<FilterContainer>();
			foreach (var f in filtersDescriptor)
			{
				if (f.IsConditionless)
					continue;
				descriptors.Add(f);
			}
			((IBoolFilter)this).Must = descriptors;
			return this;
		}

		public BoolFilterDescriptor<T> MustNot(params Func<FilterDescriptor<T>, FilterContainer>[] filters)
		{
			var descriptors = new List<FilterContainer>();
			foreach (var selector in filters)
			{
				var filter = new FilterDescriptor<T>();
				var f = selector(filter);
				if (f.IsConditionless)
					continue;
				descriptors.Add(f);
			}
			((IBoolFilter)this).MustNot = descriptors;
			return this;
		}

		public BoolFilterDescriptor<T> MustNot(params FilterContainer[] filtersDescriptor)
		{
			var descriptors = new List<FilterContainer>();
			foreach (var f in filtersDescriptor)
			{
				if (f.IsConditionless)
					continue;
				descriptors.Add(f);
			}
			((IBoolFilter)this).MustNot = descriptors;
			return this;
		}
	
		public BoolFilterDescriptor<T> Should(params Func<FilterDescriptor<T>, FilterContainer>[] filters)
		{
			var descriptors = new List<FilterContainer>();
			foreach (var selector in filters)
			{
				var filter = new FilterDescriptor<T>();
				var f = selector(filter);
				if (f.IsConditionless)
					continue;
				descriptors.Add(f);
			}
			((IBoolFilter)this).Should = descriptors;
			return this;
		}

		public BoolFilterDescriptor<T> Should(params FilterContainer[] filtersDescriptor)
		{
			var descriptors = new List<FilterContainer>();
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
