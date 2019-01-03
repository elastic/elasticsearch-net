using System;
using Newtonsoft.Json;

namespace Nest
{
	[MapsApi("ccr.follow.json")]
	[ContractJsonConverter(typeof(ReadAsTypeJsonConverter<CreateFollowIndexRequest>))]
	public partial interface ICreateFollowIndexRequest
	{
		/// <summary>
		///
		/// <para>
		/// </para>
		/// </summary>
		[JsonProperty("cursor")]
		string Cursor { get; set; }
	}

	public partial class CreateFollowIndexRequest
	{
		/// <inheritdoc cref="ICreateFollowIndexRequest.Cursor" />
		public string Cursor { get; set; }
	}

	public partial class CreateFollowIndexDescriptor
	{
		string ICreateFollowIndexRequest.Cursor { get; set; }

		/// <inheritdoc cref="ICreateFollowIndexRequest.Cursor" />
		public CreateFollowIndexDescriptor Cursor(string cursor) => Assign(a => a.Cursor = cursor);
	}
}
