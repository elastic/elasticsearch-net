using System;
using CsQuery.ExtensionMethods.Internal;

namespace ApiGenerator.Domain 
{
	public class CsharpNames
	{
		public CsharpNames(string name, string endpointMethodName, string endpointNamespace)
		{
			Namespace = endpointNamespace.ToPascalCase();
			if (CodeConfiguration.ApiNameMapping.TryGetValue(name, out var mapsApiMethodName))
				MethodName = mapsApiMethodName;
			else MethodName = $"{Namespace}{endpointMethodName.ToPascalCase()}";
		}

		/// <summary> Pascal cased version of the namespace from the specification </summary>
		public string Namespace { get; set; }

		/// <summary>
		/// The pascal cased method name as loaded by <see cref="ApiEndpointFactory.FromFile"/>
		/// <pre>Uses <see cref="CodeConfiguration.ApiNameMapping"/> mapping of request implementations in the nest code base</pre>
		/// </summary>
		public string MethodName { get; set; }
		
		public string PerPathMethodName(string path)
		{
			Func<string, bool> ms = s => Namespace != null && Namespace.StartsWith(s);
			Func<string, bool> pc = path.Contains;

			if (ms("Indices") && !pc("{index}"))
				return (MethodName + "ForAll").Replace("AsyncForAll", "ForAllAsync");

			if (ms("Nodes") && !pc("{node_id}"))
				return (MethodName + "ForAll").Replace("AsyncForAll", "ForAllAsync");

			//temporary to maintain old behavior before we introduce namespaces
			if (!string.IsNullOrWhiteSpace(Namespace) && !MethodName.StartsWith(Namespace))
				return Namespace + MethodName;

			return MethodName;
		}
		
		public string RequestName => $"{MethodName}Request";
		public string InterfaceName => $"I{RequestName}";
		public string ParametersName => $"{RequestName}Parameters";
		public string DescriptorName => $"{MethodName}Descriptor";
		
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