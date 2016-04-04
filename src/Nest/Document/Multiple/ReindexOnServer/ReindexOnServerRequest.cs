using System;
using System.CodeDom;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IReindexOnServerRequest
	{
		[JsonProperty("source")]
		IReindexSource Source { get; set; }

		[JsonProperty("dest")]
		IReindexDestination Destination { get; set; }

		[JsonProperty("script")]
		IScript Script { get; set; }

		[JsonProperty("size")]
		long? Size { get; set; }
	}

	public partial class ReindexOnServerRequest
	{
		public IReindexSource Source { get; set; }
		public IReindexDestination Destination { get; set; }
		public IScript Script { get; set; }
		public long? Size { get; set; }
	}

	[DescriptorFor("Reindex")]
	public partial class ReindexOnServerDescriptor
	{
		IReindexSource IReindexOnServerRequest.Source { get; set; }
		IReindexDestination IReindexOnServerRequest.Destination { get; set; }
		IScript IReindexOnServerRequest.Script { get; set; }
		long? IReindexOnServerRequest.Size { get; set; }

		public ReindexOnServerDescriptor Source(Func<ReindexSourceDescriptor, IReindexSource> selector = null) =>
			Assign(a => a.Source = selector.InvokeOrDefault(new ReindexSourceDescriptor()));

		public ReindexOnServerDescriptor Destination(Func<ReindexDestinationDescriptor, IReindexDestination> selector) =>
			Assign(a => a.Destination = selector?.Invoke(new ReindexDestinationDescriptor()));

		public ReindexOnServerDescriptor Script(string script) => Assign(a => a.Script = (InlineScript)script);

		public ReindexOnServerDescriptor Script(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(a => a.Script = scriptSelector?.Invoke(new ScriptDescriptor()));

		public ReindexOnServerDescriptor Size(long? size) => Assign(a => a.Size = size);
	}
}
