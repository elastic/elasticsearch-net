using System;
using Newtonsoft.Json;

namespace Nest
{
	[MapsApi("ccr.stats.json")]
	[ContractJsonConverter(typeof(ReadAsTypeJsonConverter<CcrStatsRequest>))]
	public partial interface ICcrStatsRequest
	{
		/// <summary>
		///
		/// <para>
		/// </para>
		/// </summary>
		[JsonProperty("cursor")]
		string Cursor { get; set; }
	}

	public partial class CcrStatsRequest
	{
		/// <inheritdoc cref="ICcrStatsRequest.Cursor" />
		public string Cursor { get; set; }
	}

	public partial class CcrStatsDescriptor
	{
		string ICcrStatsRequest.Cursor { get; set; }

		/// <inheritdoc cref="ICcrStatsRequest.Cursor" />
		public CcrStatsDescriptor Cursor(string cursor) => Assign(a => a.Cursor = cursor);
	}
}
