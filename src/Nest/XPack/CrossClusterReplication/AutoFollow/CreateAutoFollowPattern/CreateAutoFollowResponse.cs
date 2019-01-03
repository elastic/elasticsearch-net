using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface ICreateAutoFollowPatternResponse : IResponse
	{
		/// <summary>
		///
		/// </summary>
		[JsonProperty("columns")]
		IReadOnlyCollection<SqlColumn> Columns { get; }
	}

	public class CreateAutoFollowPatternResponse : ResponseBase, ICreateAutoFollowPatternResponse
	{
		/// <inheritdoc cref="ICreateAutoFollowPatternResponse.Columns" />
		public IReadOnlyCollection<SqlColumn> Columns { get; internal set; } = EmptyReadOnly<SqlColumn>.Collection;
	}
}
