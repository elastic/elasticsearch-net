using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface ICreateFollowIndexResponse : IResponse
	{
		/// <summary>
		///
		/// </summary>
		[JsonProperty("columns")]
		IReadOnlyCollection<SqlColumn> Columns { get; }
	}

	public class CreateFollowIndexResponse : ResponseBase, ICreateFollowIndexResponse
	{
		/// <inheritdoc cref="ICreateFollowIndexResponse.Columns" />
		public IReadOnlyCollection<SqlColumn> Columns { get; internal set; } = EmptyReadOnly<SqlColumn>.Collection;
	}
}
