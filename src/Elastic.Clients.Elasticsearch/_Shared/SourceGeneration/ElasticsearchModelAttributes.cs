// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;

namespace Elastic.Clients.Elasticsearch.SourceGeneration;

internal enum ElasticsearchTypeKind
{
	Request,
	Response,
	Class,
	Enum,
	Union
}

/// <summary>
/// Internal source generator attribute that marks types derived from the Elasticsearch specification.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum, Inherited = false)]
internal sealed class ElasticsearchTypeAttribute :
	Attribute
{
	/// <summary>
	/// Internal source generator attribute that marks types derived from the Elasticsearch specification.
	/// </summary>
	/// <param name="kind">The kind of the Elasticsearch type.</param>
	/// <param name="name">The fully qualified name of the type according to the Elasticsearch specification.</param>
	public ElasticsearchTypeAttribute(ElasticsearchTypeKind kind, string name)
	{
		Kind = kind;
		Name = name;
	}

	/// <summary>
	/// Internal source generator attribute that marks types derived from the Elasticsearch specification.
	/// </summary>
	/// <param name="kind">The kind of the Elasticsearch type.</param>
	/// <remarks>
	/// Special constructor for artificial, handcrafted types that do have a representation in the Elasticsearch specification.
	/// </remarks>
	public ElasticsearchTypeAttribute(ElasticsearchTypeKind kind)
	{
		Kind = kind;
		Name = string.Empty;
	}

	/// <summary>
	/// The Elasticsearch type kind.
	/// </summary>
	public ElasticsearchTypeKind Kind { get; }

	/// <summary>
	/// The fully qualified name of the type according to the Elasticsearch specification.
	/// </summary>
	public string Name { get; }
}

/// <summary>
/// Internal source generator attribute that provides supplementary information for Elasticsearch request types.
/// </summary>
/// <param name="api">The Elasticsearch API identifier name.</param>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
internal sealed class ElasticsearchRequestAttribute(string api) :
	Attribute
{
	/// <summary>
	/// The Elasticsearch API identifier name.
	/// </summary>
	public string Api { get; } = api;

	/// <summary>
	/// Whether the request body (if any) exclusively uses `application/json` content type encoding.
	/// </summary>
	public bool IsJsonCompatible { get; init; } = true;
}

/// <summary>
/// Internal source generator attribute that provides supplementary information for Elasticsearch response types.
/// </summary>
/// <param name="api">The Elasticsearch API identifier name.</param>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
internal sealed class ElasticsearchResponseAttribute(string api) :
	Attribute
{
	/// <summary>
	/// The Elasticsearch API identifier name.
	/// </summary>
	public string Api { get; } = api;

	/// <summary>
	/// Whether the response body (if any) exclusively uses `application/json` content type encoding.
	/// </summary>
	public bool IsJsonCompatible { get; init; } = true;
}

/// <summary>
/// Internal source generator attribute that marks a property on a request/response as a path parameter property.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
internal sealed class ElasticsearchPathParamAttribute(string name) :
	Attribute
{
	/// <summary>
	/// The name of path parameter according to the Elasticsearch specification.
	/// </summary>
	public string Name { get; } = name;
}

/// <summary>
/// Internal source generator attribute that marks a property on a request/response as a query string parameter
/// property.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
internal sealed class ElasticsearchQueryParamAttribute(string name) :
	Attribute
{
	/// <summary>
	/// The name of query string parameter according to the Elasticsearch specification.
	/// </summary>
	public string Name { get; } = name;
}

/// <summary>
/// Internal source generator attribute that marks a property on a request/response as the property that contains
/// the request/response body value.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
internal sealed class ElasticsearchBodyAttribute :
	Attribute;

/// <summary>
/// Internal source generator attribute that marks a property as JSON serializable.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
internal sealed class ElasticsearchSerializableAttribute(params string[] names) :
	Attribute
{
	/// <summary>
	/// The JSON names used for (de-)serialization.
	/// <para>
	///		The first name is the primary/canonical representation that is always used when serializing the property.
	/// </para>
	/// <para>
	///		Any additional name is accepted as alias when deserializing.
	/// </para>
	/// </summary>
	public string[] Names { get; } = names;
}
