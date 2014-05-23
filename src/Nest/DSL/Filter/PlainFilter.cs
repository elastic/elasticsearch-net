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
			if (filter == null) return null;
			var c = new FilterContainer();
			filter.WrapInContainer(c);
			return c;
		}
		protected abstract void WrapInContainer(IFilterContainer container);
	}
}