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
namespace Elastic.Clients.Elasticsearch.Global.Reindex
{
	[ConvertAs(typeof(Destination))]
	public partial interface IDestination
	{
		Elastic.Clients.Elasticsearch.IndexName Index { get; set; }

		Elastic.Clients.Elasticsearch.OpType? OpType { get; set; }

		string? Pipeline { get; set; }

		Elastic.Clients.Elasticsearch.Routing? Routing { get; set; }

		Elastic.Clients.Elasticsearch.VersionType? VersionType { get; set; }
	}

	public partial class DestinationDescriptor : DescriptorBase<DestinationDescriptor, IDestination>, IDestination
	{
		Elastic.Clients.Elasticsearch.IndexName IDestination.Index { get; set; }

		Elastic.Clients.Elasticsearch.OpType? IDestination.OpType { get; set; }

		string? IDestination.Pipeline { get; set; }

		Elastic.Clients.Elasticsearch.Routing? IDestination.Routing { get; set; }

		Elastic.Clients.Elasticsearch.VersionType? IDestination.VersionType { get; set; }
	}

	public partial class Destination : IDestination
	{
		[JsonInclude]
		[JsonPropertyName("index")]
		public Elastic.Clients.Elasticsearch.IndexName Index { get; set; }

		[JsonInclude]
		[JsonPropertyName("op_type")]
		public Elastic.Clients.Elasticsearch.OpType? OpType { get; set; }

		[JsonInclude]
		[JsonPropertyName("pipeline")]
		public string? Pipeline { get; set; }

		[JsonInclude]
		[JsonPropertyName("routing")]
		public Elastic.Clients.Elasticsearch.Routing? Routing { get; set; }

		[JsonInclude]
		[JsonPropertyName("version_type")]
		public Elastic.Clients.Elasticsearch.VersionType? VersionType { get; set; }
	}

	[ConvertAs(typeof(RemoteSource))]
	public partial interface IRemoteSource
	{
		Elastic.Clients.Elasticsearch.Time ConnectTimeout { get; set; }

		Elastic.Clients.Elasticsearch.Host Host { get; set; }

		Elastic.Clients.Elasticsearch.Password Password { get; set; }

		Elastic.Clients.Elasticsearch.Time SocketTimeout { get; set; }

		Elastic.Clients.Elasticsearch.Username Username { get; set; }
	}

	public partial class RemoteSourceDescriptor : DescriptorBase<RemoteSourceDescriptor, IRemoteSource>, IRemoteSource
	{
		Elastic.Clients.Elasticsearch.Time IRemoteSource.ConnectTimeout { get; set; }

		Elastic.Clients.Elasticsearch.Host IRemoteSource.Host { get; set; }

		Elastic.Clients.Elasticsearch.Username IRemoteSource.Username { get; set; }

		Elastic.Clients.Elasticsearch.Password IRemoteSource.Password { get; set; }

		Elastic.Clients.Elasticsearch.Time IRemoteSource.SocketTimeout { get; set; }
	}

	public partial class RemoteSource : IRemoteSource
	{
		[JsonInclude]
		[JsonPropertyName("connect_timeout")]
		public Elastic.Clients.Elasticsearch.Time ConnectTimeout { get; set; }

		[JsonInclude]
		[JsonPropertyName("host")]
		public Elastic.Clients.Elasticsearch.Host Host { get; set; }

		[JsonInclude]
		[JsonPropertyName("password")]
		public Elastic.Clients.Elasticsearch.Password Password { get; set; }

		[JsonInclude]
		[JsonPropertyName("socket_timeout")]
		public Elastic.Clients.Elasticsearch.Time SocketTimeout { get; set; }

		[JsonInclude]
		[JsonPropertyName("username")]
		public Elastic.Clients.Elasticsearch.Username Username { get; set; }
	}

	[ConvertAs(typeof(Source))]
	public partial interface ISource
	{
		Elastic.Clients.Elasticsearch.Indices Index { get; set; }

		Elastic.Clients.Elasticsearch.QueryDsl.IQueryContainer? Query { get; set; }

		Elastic.Clients.Elasticsearch.Global.Reindex.IRemoteSource? Remote { get; set; }

		int? Size { get; set; }

		Elastic.Clients.Elasticsearch.ISlicedScroll? Slice { get; set; }

		Elastic.Clients.Elasticsearch.Global.Search.Sort? Sort { get; set; }

		Elastic.Clients.Elasticsearch.Fields? source_fields { get; set; }
	}

	public partial class SourceDescriptor : DescriptorBase<SourceDescriptor, ISource>, ISource
	{
		Elastic.Clients.Elasticsearch.Indices ISource.Index { get; set; }

		Elastic.Clients.Elasticsearch.QueryDsl.IQueryContainer? ISource.Query { get; set; }

		Elastic.Clients.Elasticsearch.Global.Reindex.IRemoteSource? ISource.Remote { get; set; }

		int? ISource.Size { get; set; }

		Elastic.Clients.Elasticsearch.ISlicedScroll? ISource.Slice { get; set; }

		Elastic.Clients.Elasticsearch.Global.Search.Sort? ISource.Sort { get; set; }

		Elastic.Clients.Elasticsearch.Fields? ISource.source_fields { get; set; }
	}

	public partial class Source : ISource
	{
		[JsonInclude]
		[JsonPropertyName("index")]
		public Elastic.Clients.Elasticsearch.Indices Index { get; set; }

		[JsonInclude]
		[JsonPropertyName("query")]
		public Elastic.Clients.Elasticsearch.QueryDsl.IQueryContainer? Query { get; set; }

		[JsonInclude]
		[JsonPropertyName("remote")]
		public Elastic.Clients.Elasticsearch.Global.Reindex.IRemoteSource? Remote { get; set; }

		[JsonInclude]
		[JsonPropertyName("size")]
		public int? Size { get; set; }

		[JsonInclude]
		[JsonPropertyName("slice")]
		public Elastic.Clients.Elasticsearch.ISlicedScroll? Slice { get; set; }

		[JsonInclude]
		[JsonPropertyName("sort")]
		public Elastic.Clients.Elasticsearch.Global.Search.Sort? Sort { get; set; }

		[JsonInclude]
		[JsonPropertyName("_source")]
		public Elastic.Clients.Elasticsearch.Fields? source_fields { get; set; }
	}
}