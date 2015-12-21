namespace Nest
{
	public enum VisitorScope
	{
		Unknown,
		Query,
		Must,
		MustNot,
		Should,
		PositiveQuery,
		NegativeQuery,
		NoMatchQuery,
		Span,

	}
}