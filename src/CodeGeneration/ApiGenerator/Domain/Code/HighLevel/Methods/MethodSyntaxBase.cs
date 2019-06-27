namespace ApiGenerator.Domain.Code.HighLevel.Methods
{
	public abstract class MethodSyntaxBase
	{
		protected MethodSyntaxBase(CsharpNames names, string link, string summary) =>
			(CsharpNames, DocumentationLink, XmlDocSummary) = (names, link, summary);

		public string DocumentationLink { get;  }

		public string XmlDocSummary { get;  }

		protected CsharpNames CsharpNames { get; }

		public bool InterfaceResponse => ResponseName.StartsWith("ISearchResponse<");

		public string ResponseName => CsharpNames.GenericOrNonGenericResponseName;

		public string DocumentationCref => CsharpNames.GenericOrNonGenericInterfacePreference;

		public abstract string MethodGenerics { get; }

		public abstract string GenericWhereClause { get; }
	}
}
