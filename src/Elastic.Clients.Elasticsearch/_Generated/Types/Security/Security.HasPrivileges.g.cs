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
namespace Elastic.Clients.Elasticsearch.Security.HasPrivileges
{
	[ConvertAs(typeof(ApplicationPrivilegesCheck))]
	public partial interface IApplicationPrivilegesCheck
	{
		string Application { get; set; }

		IEnumerable<string> Privileges { get; set; }

		IEnumerable<string> Resources { get; set; }
	}

	public partial class ApplicationPrivilegesCheckDescriptor : DescriptorBase<ApplicationPrivilegesCheckDescriptor, IApplicationPrivilegesCheck>, IApplicationPrivilegesCheck
	{
		string IApplicationPrivilegesCheck.Application { get; set; }

		IEnumerable<string> IApplicationPrivilegesCheck.Privileges { get; set; }

		IEnumerable<string> IApplicationPrivilegesCheck.Resources { get; set; }
	}

	public partial class ApplicationPrivilegesCheck : IApplicationPrivilegesCheck
	{
		[JsonInclude]
		[JsonPropertyName("application")]
		public string Application { get; set; }

		[JsonInclude]
		[JsonPropertyName("privileges")]
		public IEnumerable<string> Privileges { get; set; }

		[JsonInclude]
		[JsonPropertyName("resources")]
		public IEnumerable<string> Resources { get; set; }
	}

	[ConvertAs(typeof(IndexPrivilegesCheck))]
	public partial interface IIndexPrivilegesCheck
	{
		Elastic.Clients.Elasticsearch.Indices Names { get; set; }

		IEnumerable<Elastic.Clients.Elasticsearch.Security.IndexPrivilege> Privileges { get; set; }
	}

	public partial class IndexPrivilegesCheckDescriptor : DescriptorBase<IndexPrivilegesCheckDescriptor, IIndexPrivilegesCheck>, IIndexPrivilegesCheck
	{
		Elastic.Clients.Elasticsearch.Indices IIndexPrivilegesCheck.Names { get; set; }

		IEnumerable<Elastic.Clients.Elasticsearch.Security.IndexPrivilege> IIndexPrivilegesCheck.Privileges { get; set; }
	}

	public partial class IndexPrivilegesCheck : IIndexPrivilegesCheck
	{
		[JsonInclude]
		[JsonPropertyName("names")]
		public Elastic.Clients.Elasticsearch.Indices Names { get; set; }

		[JsonInclude]
		[JsonPropertyName("privileges")]
		public IEnumerable<Elastic.Clients.Elasticsearch.Security.IndexPrivilege> Privileges { get; set; }
	}
}