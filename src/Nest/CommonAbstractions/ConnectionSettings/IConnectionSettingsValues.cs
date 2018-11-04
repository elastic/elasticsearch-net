using System;
using System.Reflection;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public interface IConnectionSettingsValues : IConnectionConfigurationValues
	{
		Func<string, string> DefaultFieldNameInferrer { get; }
		string DefaultIndex { get; }
		FluentDictionary<Type, string> DefaultIndices { get; }
		Func<Type, string> DefaultTypeNameInferrer { get; }
		FluentDictionary<Type, string> DefaultTypeNames { get; }
		FluentDictionary<Type, string> IdProperties { get; }
		Inferrer Inferrer { get; }
		FluentDictionary<MemberInfo, IPropertyMapping> PropertyMappings { get; }

		ISerializerFactory SerializerFactory { get; }

		IElasticsearchSerializer StatefulSerializer(JsonConverter converter);
	}
}
