namespace ApiGenerator.Domain.Code.HighLevel.Methods
{
	public class InitializerSyntaxView
	{
		public InitializerSyntaxView(InitializerMethod  syntax, bool async) => (Syntax , Async) = (syntax, async);

		public InitializerMethod Syntax { get; }

		public bool Async { get; }
	}

}
