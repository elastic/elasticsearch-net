namespace Nest.QueryDsl.Visitor
{
	public enum VisitorScope
	{
		Unknown,
		Filter,
		Query,
		Must,
		MustNot,
		Should,
		PositiveQuery,
		NegativeQuery,
		NoMatchQuery,

	}
}