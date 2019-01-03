using System;
using Newtonsoft.Json;

namespace Nest
{
	[MapsApi("ccr.resume_follow.json")]
	[ContractJsonConverter(typeof(ReadAsTypeJsonConverter<ResumeFollowIndexRequest>))]
	public partial interface IResumeFollowIndexRequest
	{
		/// <summary>
		///
		/// <para>
		/// </para>
		/// </summary>
		[JsonProperty("cursor")]
		string Cursor { get; set; }
	}

	public partial class ResumeFollowIndexRequest
	{
		/// <inheritdoc cref="IResumeFollowIndexRequest.Cursor" />
		public string Cursor { get; set; }
	}

	public partial class ResumeFollowIndexDescriptor
	{
		string IResumeFollowIndexRequest.Cursor { get; set; }

		/// <inheritdoc cref="IResumeFollowIndexRequest.Cursor" />
		public ResumeFollowIndexDescriptor Cursor(string cursor) => Assign(a => a.Cursor = cursor);
	}
}
