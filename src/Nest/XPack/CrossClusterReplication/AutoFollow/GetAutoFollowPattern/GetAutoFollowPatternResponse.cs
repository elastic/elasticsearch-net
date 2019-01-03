using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetAutoFollowPatternResponse : IResponse
	{
		/// <summary>
		///
		/// </summary>
		[JsonProperty("columns")]
		IReadOnlyCollection<SqlColumn> Columns { get; }
	}

	public class GetAutoFollowPatternResponse : ResponseBase, IGetAutoFollowPatternResponse
	{
		/// <inheritdoc cref="IGetAutoFollowPatternResponse.Columns" />
		public IReadOnlyCollection<SqlColumn> Columns { get; internal set; } = EmptyReadOnly<SqlColumn>.Collection;
	}
}
