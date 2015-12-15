namespace Nest
{
	public class TokenCountAttribute : NumberAttribute, ITokenCountProperty
	{
		ITokenCountProperty Self => this;

		string ITokenCountProperty.Analyzer { get; set; }

		public string Analyzer { get { return Self.Analyzer; } set { Self.Analyzer = value; } }

		public TokenCountAttribute() : base("token_count") { }
	}	
}
