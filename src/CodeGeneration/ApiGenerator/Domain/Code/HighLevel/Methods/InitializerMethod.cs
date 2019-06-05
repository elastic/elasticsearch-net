using System.Linq;
using ApiGenerator.Configuration;

namespace ApiGenerator.Domain.Code.HighLevel.Methods
{
	public class InitializerMethod : MethodSyntaxBase
	{
		public InitializerMethod(CsharpNames names, string link, string summary) : base(names, link, summary) { }

		public string MethodName => CsharpNames.MethodName;

		public string ArgumentType => CsharpNames.GenericOrNonGenericInterfacePreference;

		public override string MethodGenerics =>
			CodeConfiguration.GenericOnlyInterfaces.Contains(CsharpNames.RequestInterfaceName)
				? CsharpNames.GenericsDeclaredOnRequest
				: CsharpNames.GenericsDeclaredOnResponse;

		public override string GenericWhereClause =>
			string.Join(" ", CsharpNames.SplitGeneric(MethodGenerics)
				.Where(g=>g.Contains("Document"))
				.Select(g=>$"where {g} : class")
			);

		private bool IsCatMethod => CsharpNames.Namespace == "Cat";

		private bool IsCatHelpMethod => IsCatMethod && MethodName == "Help";

		private bool IsNodesHotThreadsMethod => CsharpNames.Namespace == "Nodes" && MethodName == "HotThreads";

		private bool IsMultiGetMethod => MethodName == "MultiGet";

		private bool IsMultiSearchMethod => MethodName == "MultiSearch";

		private bool IsMultiSearchTemplateMethod => MethodName == "MultiSearchTemplate";

		public string DispatchMethod => IsNodesHotThreadsMethod
										? "DoNodesHotThreads"
										:IsMultiSearchTemplateMethod
											? "DoMultiSearchTemplate"
											: IsMultiGetMethod
												? "DoMultiGet"
												: IsMultiSearchMethod
													? "DoMultiSearch"
													: IsCatHelpMethod
														? "DoCatHelp"
														: IsCatMethod
															? "DoCat"
															: "DoRequest";
		public string DispatchGenerics =>
			IsNodesHotThreadsMethod
			|| IsMultiGetMethod
			|| IsMultiSearchMethod
			|| IsMultiSearchTemplateMethod
				  ? ""
				  : IsCatMethod
						? $"<{ArgumentType},{CsharpNames.ParametersName},{CsharpNames.RequestName.Replace("Request", "Record")}>"
						: $"<{ArgumentType},{ResponseName}>";

		public string DispatchParameters =>
			IsCatMethod
			|| IsMultiGetMethod
			|| IsMultiSearchMethod
			|| IsMultiSearchTemplateMethod
			|| IsNodesHotThreadsMethod
					? "request"
					: "request, request.RequestParameters";
	}
}
