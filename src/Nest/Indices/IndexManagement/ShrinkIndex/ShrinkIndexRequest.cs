using System;
using Newtonsoft.Json;

namespace Nest
{
	[MapsApi("indices.shrink.json")]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<ShrinkIndexRequest>))]
	public partial interface IShrinkIndexRequest
	{
		[JsonProperty("aliases")]
		IAliases Aliases { get; set; }

		[JsonProperty("settings")]
		IIndexSettings Settings { get; set; }
	}

	public partial class ShrinkIndexRequest
	{
		// For ReadAsType()
		internal ShrinkIndexRequest() { }

		public IAliases Aliases { get; set; }

		public IIndexSettings Settings { get; set; }
	}

	public partial class ShrinkIndexDescriptor
	{
		IAliases IShrinkIndexRequest.Aliases { get; set; }
		IIndexSettings IShrinkIndexRequest.Settings { get; set; }

		public ShrinkIndexDescriptor Settings(Func<IndexSettingsDescriptor, IPromise<IIndexSettings>> selector) =>
			Assign(a => a.Settings = selector?.Invoke(new IndexSettingsDescriptor())?.Value);

		public ShrinkIndexDescriptor Aliases(Func<AliasesDescriptor, IPromise<IAliases>> selector) =>
			Assign(a => a.Aliases = selector?.Invoke(new AliasesDescriptor())?.Value);
	}
}
