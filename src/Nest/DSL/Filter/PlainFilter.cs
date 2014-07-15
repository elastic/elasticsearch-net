namespace Nest
{
	public abstract class PlainFilter : IFilter
	{
		bool IFilter.IsConditionless { get { return false; } }
		bool IFilter.IsVerbatim { get ; set; }
		bool IFilter.IsStrict { get; set; }
		public bool? Cache { get; set; }
		public string FilterName { get; set; }
		public string CacheKey { get; set; }

		public static bool operator false(PlainFilter a)
		{
			return false;
		}

		public static bool operator true(PlainFilter a)
		{
			return false;
		}
		public static PlainFilter operator &(PlainFilter leftQuery, PlainFilter rightQuery)
		{
			var lc = new FilterContainer();
			leftQuery.WrapInContainer(lc);
			var rc = new FilterContainer();
			rightQuery.WrapInContainer(rc);
			var query = ((lc && rc) as IFilterContainer).Bool;
			return new BoolFilter()
			{
				Must = query.Must,
				MustNot = query.MustNot,
				Should = query.Should
			};
		}
		public static implicit operator FilterContainer(PlainFilter filter)
		{
			return ToContainer(filter);
		}
		public FilterContainer ToContainer()
		{
			return PlainFilter.ToContainer(this);
		}

		public static FilterContainer ToContainer(PlainFilter filter, FilterContainer filterContainer = null)
		{
			if (filter == null) return null;
			var c = filterContainer ?? new FilterContainer();
			IFilterContainer fc = c;
			filter.WrapInContainer(c);
			fc.Cache = filter.Cache;
			fc.CacheKey = filter.CacheKey;
			fc.FilterName = filter.FilterName;
			return c;
		}

		protected internal abstract void WrapInContainer(IFilterContainer container);
	}
}