using System;
using System.CodeDom;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	/// <summary>
	/// A request to Reindex API
	/// </summary>
	public partial interface IReindexOnServerRequest
	{
		/// <summary>
		/// The source for the reindex operation
		/// </summary>
		[JsonProperty("source")]
		IReindexSource Source { get; set; }

		/// <summary>
		/// The destination for the reindex operation.
		/// </summary>
		/// <remarks>
		/// Reindex does not attempt to set up the destination index. It does not copy the settings
		/// of the source index. You should set up the destination index beforehand
		/// </remarks>
		[JsonProperty("dest")]
		IReindexDestination Destination { get; set; }

		/// <summary>
		/// A script that can modify documents from source, including metadata, before reindexing
		/// </summary>
		[JsonProperty("script")]
		IScript Script { get; set; }

		/// <summary>
		/// Limit the number of processed documents
		/// </summary>
		[JsonProperty("size")]
		long? Size { get; set; }

		/// <summary>
		/// Determine what to do in the event of version conflicts.
		/// Defaults to <see cref="Elasticsearch.Net.Conflicts.Abort"/>
		/// </summary>
        [JsonProperty("conflicts")]
        [JsonConverter(typeof(StringEnumConverter))]
        Conflicts?  Conflicts { get; set; }
    }

	/// <inheritdoc cref="IReindexOnServerRequest"/>
	public partial class ReindexOnServerRequest
	{
		/// <inheritdoc cref="IReindexOnServerRequest.Source"/>
		public IReindexSource Source { get; set; }
		/// <inheritdoc cref="IReindexOnServerRequest.Destination"/>
		public IReindexDestination Destination { get; set; }
		/// <inheritdoc cref="IReindexOnServerRequest.Script"/>
		public IScript Script { get; set; }
		/// <inheritdoc cref="IReindexOnServerRequest.Size"/>
		public long? Size { get; set; }
		/// <inheritdoc cref="IReindexOnServerRequest.Conflicts"/>
        public Conflicts? Conflicts { get; set; }
    }

	[DescriptorFor("Reindex")]
	public partial class ReindexOnServerDescriptor
	{
		IReindexSource IReindexOnServerRequest.Source { get; set; }
		IReindexDestination IReindexOnServerRequest.Destination { get; set; }
		IScript IReindexOnServerRequest.Script { get; set; }
		long? IReindexOnServerRequest.Size { get; set; }
        Conflicts? IReindexOnServerRequest.Conflicts { get; set; }

		/// <inheritdoc cref="IReindexOnServerRequest.Source"/>
		public ReindexOnServerDescriptor Source(Func<ReindexSourceDescriptor, IReindexSource> selector = null) =>
			Assign(a => a.Source = selector.InvokeOrDefault(new ReindexSourceDescriptor()));

		/// <inheritdoc cref="IReindexOnServerRequest.Destination"/>
		public ReindexOnServerDescriptor Destination(Func<ReindexDestinationDescriptor, IReindexDestination> selector) =>
			Assign(a => a.Destination = selector?.Invoke(new ReindexDestinationDescriptor()));

		/// <inheritdoc cref="IReindexOnServerRequest.Script"/>
		public ReindexOnServerDescriptor Script(string script) => Assign(a => a.Script = (InlineScript)script);

		/// <inheritdoc cref="IReindexOnServerRequest.Script"/>
		public ReindexOnServerDescriptor Script(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(a => a.Script = scriptSelector?.Invoke(new ScriptDescriptor()));

		/// <inheritdoc cref="IReindexOnServerRequest.Size"/>
		public ReindexOnServerDescriptor Size(long? size) => Assign(a => a.Size = size);

		/// <inheritdoc cref="IReindexOnServerRequest.Conflicts"/>
        public ReindexOnServerDescriptor Conflicts(Conflicts? conflicts) => Assign(a => a.Conflicts = conflicts);
	}
}
