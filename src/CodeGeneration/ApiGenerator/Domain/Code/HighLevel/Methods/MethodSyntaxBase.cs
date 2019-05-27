namespace ApiGenerator.Domain 
{
	public abstract class MethodSyntaxBase
	{
		protected MethodSyntaxBase(CsharpNames names) => (CsharpNames) = (names);
		
		protected CsharpNames CsharpNames { get; }
		
		public string ResponseName => CsharpNames.GenericOrNonGenericResponseName;

		public string DocumentationCref => CsharpNames.RequestInterfaceName;
		public abstract string MethodGenerics { get; }
		
		public abstract string GenericWhereClause { get; }
	}
}