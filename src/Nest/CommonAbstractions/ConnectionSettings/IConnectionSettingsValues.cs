using System;
using System.Reflection;
using Elasticsearch.Net;

namespace Nest
{
	public interface IConnectionSettingsValues : IConnectionConfigurationValues
	{
		Func<string, string> DefaultFieldNameInferrer { get; }

		string DefaultIndex { get; }
		FluentDictionary<Type, string> DefaultIndices { get; }
		FluentDictionary<Type, string> DefaultRelationNames { get; }
		string DefaultTypeName { get; }

		Func<Type, string> DefaultTypeNameInferrer { get; }
		FluentDictionary<Type, string> DefaultTypeNames { get; }
		FluentDictionary<Type, string> IdProperties { get; }
		Inferrer Inferrer { get; }
		IPropertyMappingProvider PropertyMappingProvider { get; }
		FluentDictionary<MemberInfo, IPropertyMapping> PropertyMappings { get; }
		FluentDictionary<Type, string> RouteProperties { get; }

		IElasticsearchSerializer SourceSerializer { get; }
	}
}
