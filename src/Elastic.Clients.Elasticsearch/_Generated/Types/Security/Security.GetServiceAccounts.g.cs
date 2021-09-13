// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.
//
// ███╗   ██╗ ██████╗ ████████╗██╗ ██████╗███████╗
// ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝██╔════╝
// ██╔██╗ ██║██║   ██║   ██║   ██║██║     █████╗
// ██║╚██╗██║██║   ██║   ██║   ██║██║     ██╔══╝
// ██║ ╚████║╚██████╔╝   ██║   ██║╚██████╗███████╗
// ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝ ╚═════╝╚══════╝
// ------------------------------------------------
//
// This file is automatically generated.
// Please do not edit these files manually.
//
// ------------------------------------------------

using Elastic.Transport.Products.Elasticsearch.Failures;
using OneOf;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.Security.GetServiceAccounts
{
	public partial class RoleDescriptor
	{
		[JsonInclude]
		[JsonPropertyName("applications")]
		public IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.ApplicationPrivileges>? Applications
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("cluster")]
		public IReadOnlyCollection<string> Cluster
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("global")]
		public IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.GlobalPrivilege>? Global
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("indices")]
		public IReadOnlyCollection<Elastic.Clients.Elasticsearch.Security.IndicesPrivileges> Indices
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("metadata")]
		public Elastic.Clients.Elasticsearch.Metadata? Metadata
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("run_as")]
		public IReadOnlyCollection<string>? RunAs
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("transient_metadata")]
		public Dictionary<string, object>? TransientMetadata
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}
	}

	public partial class RoleDescriptorWrapper
	{
		[JsonInclude]
		[JsonPropertyName("role_descriptor")]
		public Elastic.Clients.Elasticsearch.Security.GetServiceAccounts.RoleDescriptor RoleDescriptor
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}
	}
}