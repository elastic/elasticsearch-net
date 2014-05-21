namespace Nest
{
	public abstract class PlainQuery
	{
		public static bool operator false(PlainQuery a)
		{
			return false;
		}

		public static bool operator true(PlainQuery a)
		{
			return false;
		}
		public static PlainQuery operator &(PlainQuery leftQuery, PlainQuery rightQuery)
		{
			var lc = new QueryContainer();
			leftQuery.WrapInContainer(lc);
			var rc = new QueryContainer();
			rightQuery.WrapInContainer(rc);
			var query = ((lc && rc) as IQueryContainer).Bool;
			return new BoolQuery()
			{
				Must = query.Must,
				MustNot = query.MustNot,
				Should = query.Should
			};
		}
		//public static QueryContainer operator &(PlainQuery leftQuery, PlainQuery rightQuery)
		//{
		//	var lc = new QueryContainer();
		//	leftQuery.WrapInContainer(lc);
		//	var rc = new QueryContainer();
		//	leftQuery.WrapInContainer(rc);
		//	return (lc && rc);
		//}
		public static implicit operator QueryContainer(PlainQuery query)
		{
			if (query == null) return null;
			var c = new QueryContainer();
			query.WrapInContainer(c);
			return c;
		}
		protected abstract void WrapInContainer(IQueryContainer container);
	}
}