using System;
using Elasticsearch.Net;
using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

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
		[DataMember(Name ="size")]
		long? Size { get; set; }

		/// <summary>
		/// The source for the reindex operation
		/// </summary>
		[DataMember(Name ="source")]
		IReindexSource Source { get; set; }
	}

	/// <inheritdoc cref="IReindexOnServerRequest" />
	public partial class ReindexOnServerRequest
	{
		/// <inheritdoc cref="IReindexOnServerRequest.Conflicts" />
		public Conflicts? Conflicts { get; set; }

		/// <inheritdoc cref="IReindexOnServerRequest.Destination" />
		public IReindexDestination Destination { get; set; }

		/// <inheritdoc cref="IReindexOnServerRequest.Script" />
		public IScript Script { get; set; }

		/// <inheritdoc cref="IReindexOnServerRequest.Size" />
		public long? Size { get; set; }

		/// <inheritdoc cref="IReindexOnServerRequest.Source" />
		public IReindexSource Source { get; set; }
	}

	public partial class ReindexOnServerDescriptor
	{
		Conflicts? IReindexOnServerRequest.Conflicts { get; set; }
		IReindexDestination IReindexOnServerRequest.Destination { get; set; }
		IScript IReindexOnServerRequest.Script { get; set; }
		long? IReindexOnServerRequest.Size { get; set; }
		IReindexSource IReindexOnServerRequest.Source { get; set; }

		/// <inheritdoc cref="IReindexOnServerRequest.Source" />
		public ReindexOnServerDescriptor Source(Func<ReindexSourceDescriptor, IReindexSource> selector = null) =>
			Assign(a => a.Source = selector.InvokeOrDefault(new ReindexSourceDescriptor()));

		/// <inheritdoc cref="IReindexOnServerRequest.Destination" />
		public ReindexOnServerDescriptor Destination(Func<ReindexDestinationDescriptor, IReindexDestination> selector) =>
			Assign(a => a.Destination = selector?.Invoke(new ReindexDestinationDescriptor()));

		/// <inheritdoc cref="IReindexOnServerRequest.Script" />
		public ReindexOnServerDescriptor Script(string script) => Assign(a => a.Script = (InlineScript)script);

		/// <inheritdoc cref="IReindexOnServerRequest.Script" />
		public ReindexOnServerDescriptor Script(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(a => a.Script = scriptSelector?.Invoke(new ScriptDescriptor()));

		/// <inheritdoc cref="IReindexOnServerRequest.Size" />
		public ReindexOnServerDescriptor Size(long? size) => Assign(a => a.Size = size);

		/// <inheritdoc cref="IReindexOnServerRequest.Conflicts" />
		public ReindexOnServerDescriptor Conflicts(Conflicts? conflicts) => Assign(a => a.Conflicts = conflicts);
	}
}
