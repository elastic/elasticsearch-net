using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using CsQuery.ExtensionMethods.Internal;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;

namespace ApiGenerator.Domain 
{
	public class CsharpNames
	{
		public CsharpNames(string name, string endpointMethodName, string endpointNamespace)
		{
			Namespace = CreateCSharpNamespace(endpointNamespace);
			if (CodeConfiguration.ApiNameMapping.TryGetValue(name, out var mapsApiMethodName))
				ApiName = mapsApiMethodName;
			else ApiName = endpointMethodName.ToPascalCase();

			//if the api name starts with the namespace do not repeat it in the method name
			MethodName = Regex.Replace(ApiName, $"^{Namespace}(\\w+)$", "$1");
		}

		/// <summary> Pascal cased version of the namespace from the specification </summary>
		public string Namespace { get; private set; }

		/// <summary>
		/// The pascal cased method name as loaded by <see cref="ApiEndpointFactory.FromFile"/>
		/// <pre>Uses <see cref="CodeConfiguration.ApiNameMapping"/> mapping of request implementations in the nest code base</pre>
		/// </summary>
		public string MethodName { get; private set; }
		
		public string ApiName { get; private set; }
		
		public string RequestName => $"{ApiName}Request";
		public string InterfaceName => $"I{RequestName}";
		public string ParametersName => $"{RequestName}Parameters";
		public string DescriptorName => $"{ApiName}Descriptor";

		public const string RootNamespace = "NoNamespace";
		private static string CreateCSharpNamespace(string endpointNamespace)
		{
			switch (endpointNamespace)
			{
				case null:
				case "": return RootNamespace;
				// SSL namespace most likely removed, no need to introduce it
				// https://github.com/elastic/elasticsearch/issues/41845
				case "ssl": return "Security"; 
				case "ilm": return "IndexLifecycleManagement";
				case "ccr": return "CrossClusterReplication";
				case "ml": return "MachineLearning";
				default: return endpointNamespace.ToPascalCase(); 
			}
		}
		
		public string PerPathMethodName(string path)
		{
			Func<string, bool> ms = s => Namespace != null && Namespace.StartsWith(s);
			Func<string, bool> pc = path.Contains;

			if (ms("Indices") && !pc("{index}"))
				return (MethodName + "ForAll").Replace("AsyncForAll", "ForAllAsync");

			if (ms("Nodes") && !pc("{node_id}"))
				return (MethodName + "ForAll").Replace("AsyncForAll", "ForAllAsync");

			return MethodName;
		}

		
		public string GenericsDeclaredOnRequest => 
			CodeConfiguration.RequestInterfaceGenericsLookup.TryGetValue(InterfaceName, out var requestGeneric) ? requestGeneric : null;

		public string GenericsDeclaredOnDescriptor =>
			CodeConfiguration.DescriptorGenericsLookup.TryGetValue(DescriptorName, out var generic) ? generic : null;

		public bool DescriptorNotFoundInCodebase => !CodeConfiguration.DescriptorGenericsLookup.TryGetValue(DescriptorName, out _);
		
		public string GenericDescriptorName => GenericsDeclaredOnDescriptor.IsNullOrEmpty() ? null : $"{DescriptorName}{GenericsDeclaredOnDescriptor}";
		public string GenericRequestName => GenericsDeclaredOnRequest.IsNullOrEmpty() ? null : $"{RequestName}{GenericsDeclaredOnRequest}";
		public string GenericInterfaceName => GenericsDeclaredOnRequest.IsNullOrEmpty() ? null : $"I{GenericRequestName}";
		
		public string GenericOrNonGenericDescriptorName => GenericDescriptorName ?? DescriptorName;
		public string GenericOrNonGenericInterfaceName => GenericInterfaceName  ?? InterfaceName;
	}
}