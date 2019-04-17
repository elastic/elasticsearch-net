namespace Nest
{
	public class TranslateSqlResponse : ResponseBase
	{
		public ISearchRequest Result { get; internal set; }
	}
}
