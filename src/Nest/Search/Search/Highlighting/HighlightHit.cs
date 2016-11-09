using System.Collections.Generic;

namespace Nest
{
	public class HighlightHit
	{
		public string DocumentId { get; internal set; }
		public string Field { get; internal set; }
		public IReadOnlyCollection<string> Highlights { get; internal set; } =
			EmptyReadOnly<string>.Collection;
	}
}
