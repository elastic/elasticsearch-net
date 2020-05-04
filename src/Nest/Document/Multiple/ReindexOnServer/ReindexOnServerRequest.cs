// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using Elasticsearch.Net;
using System.Runtime.Serialization;


namespace Nest
{
	/// <summary>
	/// A request to Reindex API
	/// </summary>
	[MapsApi("reindex.json")]
	public partial interface IReindexOnServerRequest
	{
		/// <summary>
		/// Determine what to do in the event of version conflicts.
		/// Defaults to <see cref="Elasticsearch.Net.Conflicts.Abort" />
		/// </summary>
		[DataMember(Name ="conflicts")]

		Conflicts? Conflicts { get; set; }

		/// <summary>
		/// The destination for the reindex operation.
		/// </summary>
		/// <remarks>
		/// Reindex does not attempt to set up the destination index. It does not copy the settings
		/// of the source index. You should set up the destination index beforehand
		/// </remarks>
		[DataMember(Name ="dest")]
		IReindexDestination Destination { get; set; }

		/// <summary>
		/// A script that can modify documents from source, including metadata, before reindexing
		/// </summary>
		[DataMember(Name ="script")]
		IScript Script { get; set; }

		/// <summary>
		/// Limit the number of processed documents
		/// </summary>
		// TODO: Remove in 8.0
		[DataMember(Name ="size")]
		[Obsolete("Deprecated. Use MaximumDocuments")]
		long? Size { get; set; }

		/// <summary>
		/// Limit the number of processed documents
		/// </summary>
		[DataMember(Name ="max_docs")]
		long? MaximumDocuments { get; set; }

		/// <summary>
		/// The source for the reindex operation
		/// </summary>
		[DataMember(Name ="source")]
		IReindexSource Source { get; set; }
	}

	/// <inheritdoc cref="IReindexOnServerRequest" />
	public partial class ReindexOnServerRequest
	{
		/// <inheritdoc />
		public Conflicts? Conflicts { get; set; }

		/// <inheritdoc />
		public IReindexDestination Destination { get; set; }

		/// <inheritdoc />
		public IScript Script { get; set; }

		/// <inheritdoc />
		[Obsolete("Deprecated. Use MaximumDocuments")]
		public long? Size { get; set; }

		/// <inheritdoc />
		public long? MaximumDocuments { get; set; }

		/// <inheritdoc />
		public IReindexSource Source { get; set; }
	}

	public partial class ReindexOnServerDescriptor
	{
		Conflicts? IReindexOnServerRequest.Conflicts { get; set; }
		IReindexDestination IReindexOnServerRequest.Destination { get; set; }
		IScript IReindexOnServerRequest.Script { get; set; }
		[Obsolete("Deprecated. Use MaximumDocuments")]
		long? IReindexOnServerRequest.Size { get; set; }
		IReindexSource IReindexOnServerRequest.Source { get; set; }
		long? IReindexOnServerRequest.MaximumDocuments { get; set; }

		/// <inheritdoc cref="IReindexOnServerRequest.Source" />
		public ReindexOnServerDescriptor Source(Func<ReindexSourceDescriptor, IReindexSource> selector = null) =>
			Assign(selector.InvokeOrDefault(new ReindexSourceDescriptor()), (a, v) => a.Source = v);

		/// <inheritdoc cref="IReindexOnServerRequest.Destination" />
		public ReindexOnServerDescriptor Destination(Func<ReindexDestinationDescriptor, IReindexDestination> selector) =>
			Assign(selector, (a, v) => a.Destination = v?.Invoke(new ReindexDestinationDescriptor()));

		/// <inheritdoc cref="IReindexOnServerRequest.Script" />
		public ReindexOnServerDescriptor Script(string script) => Assign((InlineScript)script, (a, v) => a.Script = v);

		/// <inheritdoc cref="IReindexOnServerRequest.Script" />
		public ReindexOnServerDescriptor Script(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(scriptSelector, (a, v) => a.Script = v?.Invoke(new ScriptDescriptor()));

		[Obsolete("Deprecated. Use MaximumDocuments")]
		/// <inheritdoc cref="IReindexOnServerRequest.Size" />
		public ReindexOnServerDescriptor Size(long? size) => Assign(size, (a, v) => a.Size = v);

		/// <inheritdoc cref="IReindexOnServerRequest.Conflicts" />
		public ReindexOnServerDescriptor Conflicts(Conflicts? conflicts) => Assign(conflicts, (a, v) => a.Conflicts = v);

		/// <inheritdoc cref="IReindexOnServerRequest.MaximumDocuments"/>
		public ReindexOnServerDescriptor MaximumDocuments(long? maximumDocuments) =>
			Assign(maximumDocuments, (a, v) => a.MaximumDocuments = v);
	}
}
