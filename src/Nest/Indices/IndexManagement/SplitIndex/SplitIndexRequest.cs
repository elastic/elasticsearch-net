using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	/// <summary>
	/// A request to split an existing index into a new index, where each original primary
	/// shard is split into two or more primary shards in the new index.
	/// </summary>
	[JsonConverter(typeof(ReadAsTypeJsonConverter<SplitIndexRequest>))]
	public partial interface ISplitIndexRequest
	{
		/// <summary>
		/// The settings for the target index
		/// </summary>
		[JsonProperty("settings")]
		IIndexSettings Settings { get; set; }

		/// <summary>
		/// The aliases for the target index
		/// </summary>
		[JsonProperty("aliases")]
		IAliases Aliases { get; set; }
	}

	/// <inheritdoc cref="ISplitIndexRequest"/>
	public partial class SplitIndexRequest
	{
		// For ReadAsType()
		internal SplitIndexRequest() { }

		/// <inheritdoc />
		public IIndexSettings Settings { get; set; }

		/// <inheritdoc />
		public IAliases Aliases { get; set; }
	}

	/// <inheritdoc cref="ISplitIndexRequest"/>
	[DescriptorFor("IndicesSplit")]
	public partial class SplitIndexDescriptor
	{
		IIndexSettings ISplitIndexRequest.Settings { get; set; }
		IAliases ISplitIndexRequest.Aliases { get; set; }

		/// <inheritdoc cref="ISplitIndexRequest.Settings"/>
		public SplitIndexDescriptor Settings(Func<IndexSettingsDescriptor, IPromise<IIndexSettings>> selector) =>
			Assign(a => a.Settings = selector?.Invoke(new IndexSettingsDescriptor())?.Value);

		/// <inheritdoc cref="ISplitIndexRequest.Aliases"/>
		public SplitIndexDescriptor Aliases(Func<AliasesDescriptor, IPromise<IAliases>> selector) =>
			Assign(a => a.Aliases = selector?.Invoke(new AliasesDescriptor())?.Value);
	}
}
