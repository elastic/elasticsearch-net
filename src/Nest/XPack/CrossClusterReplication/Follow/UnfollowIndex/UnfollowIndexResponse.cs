using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IUnfollowIndexResponse : IResponse
	{
		/// <summary>
		///
		/// </summary>
		[JsonProperty("columns")]
		IReadOnlyCollection<SqlColumn> Columns { get; }
	}

	public class UnfollowIndexResponse : ResponseBase, IUnfollowIndexResponse
	{
		/// <inheritdoc cref="IUnfollowIndexResponse.Columns" />
		public IReadOnlyCollection<SqlColumn> Columns { get; internal set; } = EmptyReadOnly<SqlColumn>.Collection;
	}
}
