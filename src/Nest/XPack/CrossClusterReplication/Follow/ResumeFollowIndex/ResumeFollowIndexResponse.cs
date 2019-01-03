using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IResumeFollowIndexResponse : IResponse
	{
		/// <summary>
		///
		/// </summary>
		[JsonProperty("columns")]
		IReadOnlyCollection<SqlColumn> Columns { get; }
	}

	public class ResumeFollowIndexResponse : ResponseBase, IResumeFollowIndexResponse
	{
		/// <inheritdoc cref="IResumeFollowIndexResponse.Columns" />
		public IReadOnlyCollection<SqlColumn> Columns { get; internal set; } = EmptyReadOnly<SqlColumn>.Collection;
	}
}
