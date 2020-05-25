// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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

		public string DispatchMethod => IsCatMethod ? "DoCat" : "DoRequest";

		/// <summary>
		/// Dispatch needs a class instance so if the response is an interface transform it to the concrete implementation
		/// when calling into DoRequest
		/// </summary>
		private string DispatchResponseName => InterfaceResponse ? ResponseName.Substring(1, ResponseName.Length - 1) : ResponseName;

		public string DispatchGenerics => IsCatMethod
			? $"<{ArgumentType},{CsharpNames.ParametersName},{CsharpNames.RequestName.Replace("Request", "Record")}>"
			: $"<{ArgumentType},{DispatchResponseName}>";

		public string DispatchParameters => IsCatMethod
			? "request"
			: CsharpNames.CustomResponseBuilderPerRequestOverride(out var builder)
				? $"request, ResponseBuilder(request.RequestParameters, {builder})"
				: "request, request.RequestParameters";
	}
}
