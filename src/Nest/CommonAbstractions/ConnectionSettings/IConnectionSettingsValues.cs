/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Collections.Generic;
using System.Reflection;
using Elasticsearch.Net;

namespace Nest
{
	/// <summary>
	/// Provides the connection settings for NEST's high level <see cref="ElasticClient" />
	/// </summary>
	public interface IConnectionSettingsValues : IConnectionConfigurationValues
	{
		/// <summary>
		/// Specifies how field names are inferred from CLR property names.
		/// <para></para>
		/// By default, NEST camel cases property names.
		/// </summary>
		/// <example>
		/// CLR property EmailAddress will be inferred as "emailAddress" Elasticsearch document field name
		/// </example>
		Func<string, string> DefaultFieldNameInferrer { get; }

		/// <summary>
		/// The default index to use for a request when no index has been explicitly specified
		/// and no default indices are specified for the given CLR type specified for the request.
		/// </summary>
		string DefaultIndex { get; }

		/// <summary>
		/// The default index/indices to use for a request for a given CLR type specified in the request.
		/// </summary>
		FluentDictionary<Type, string> DefaultIndices { get; }

		/// <summary>
		/// The default relation name to use for a request for a given CLR type specified in the request.
		/// </summary>
		FluentDictionary<Type, string> DefaultRelationNames { get; }

		/// <summary>
		/// Specify a property for a CLR type to use to infer the _id of the document when indexed in Elasticsearch.
		/// </summary>
		FluentDictionary<Type, string> IdProperties { get; }

		/// <summary>
		/// Infers index, type, id, relation, routing and field names
		/// </summary>
		Inferrer Inferrer { get; }

		/// <summary>
		/// Provides mappings for CLR types
		/// </summary>
		IPropertyMappingProvider PropertyMappingProvider { get; }

		/// <summary>
		/// Provides mappings for CLR type members
		/// </summary>
		FluentDictionary<MemberInfo, IPropertyMapping> PropertyMappings { get; }

		/// <summary>
		/// Specify a property for a CLR type to use to infer the routing for of a document when indexed in Elasticsearch.
		/// </summary>
		FluentDictionary<Type, string> RouteProperties { get; }

		/// <summary>
		/// Disables automatic Id inference for given CLR types.
		/// <para></para>
		/// NEST by default will use the value of a property named Id on a CLR type as the _id to send to Elasticsearch. Adding a type
		/// will disable this behaviour for that CLR type. If Id inference should be disabled for all CLR types, use
		/// <see cref="DefaultDisableIdInference"/>
		/// </summary>
		HashSet<Type> DisableIdInference { get; }

		/// <summary>
		/// Disables automatic Id inference for all CLR types.
		/// <para></para>
		/// NEST by default will use the value of a property named Id on a CLR type as the _id to send to Elasticsearch. Setting this to <c>true</c>
		/// will disable this behaviour for all CLR types and cannot be overridden. If Id inference should be disabled only for specific types, use
		/// <see cref="DisableIdInference"/>
		/// </summary>
		bool DefaultDisableIdInference { get; }

		/// <summary>
		/// The serializer use to serialize CLR types representing documents and other types related to documents.
		/// </summary>
		IElasticsearchSerializer SourceSerializer { get; }
	}
}
