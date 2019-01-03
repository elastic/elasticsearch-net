using System;
using Newtonsoft.Json;

namespace Nest
{
	[MapsApi("ccr.put_auto_follow_pattern.json")]
	[ContractJsonConverter(typeof(ReadAsTypeJsonConverter<CreateAutoFollowPatternRequest>))]
	public partial interface ICreateAutoFollowPatternRequest
	{
		/// <summary>
		///
		/// <para>
		/// </para>
		/// </summary>
		[JsonProperty("cursor")]
		string Cursor { get; set; }
	}

	public partial class CreateAutoFollowPatternRequest
	{
		/// <inheritdoc cref="ICreateAutoFollowPatternRequest.Cursor" />
		public string Cursor { get; set; }
	}

	public partial class CreateAutoFollowPatternDescriptor
	{
		string ICreateAutoFollowPatternRequest.Cursor { get; set; }

		/// <inheritdoc cref="ICreateAutoFollowPatternRequest.Cursor" />
		public CreateAutoFollowPatternDescriptor Cursor(string cursor) => Assign(a => a.Cursor = cursor);
	}
}
