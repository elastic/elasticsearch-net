namespace Nest
{
	public enum VisitorScope
	{
		Unknown,
		Query,
		Filter,
		Must,
		MustNot,
		Should,
		PositiveQuery,
		NegativeQuery,
		NoMatchQuery,
		Span,

	}
}
