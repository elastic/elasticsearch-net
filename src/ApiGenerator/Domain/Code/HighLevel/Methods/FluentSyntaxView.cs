namespace ApiGenerator.Domain.Code.HighLevel.Methods 
{
	public class FluentSyntaxView
	{
		public FluentSyntaxView(FluentSyntaxBase syntax, bool async) => (Syntax , Async) = (syntax, async);

		public FluentSyntaxBase Syntax { get; }

		public bool Async { get; }
	}
}