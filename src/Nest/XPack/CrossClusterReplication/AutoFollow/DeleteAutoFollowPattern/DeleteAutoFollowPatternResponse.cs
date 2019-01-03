using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IDeleteAutoFollowPatternResponse : IResponse
	{
		/// <summary>
		///
		/// </summary>
		[JsonProperty("columns")]
		IReadOnlyCollection<SqlColumn> Columns { get; }
	}

	public class DeleteAutoFollowPatternResponse : ResponseBase, IDeleteAutoFollowPatternResponse
	{
		/// <inheritdoc cref="IDeleteAutoFollowPatternResponse.Columns" />
		public IReadOnlyCollection<SqlColumn> Columns { get; internal set; } = EmptyReadOnly<SqlColumn>.Collection;
	}
}
