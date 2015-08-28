using System.Collections.Generic;

namespace Nest
{
	public interface IIndexState
	{
		IIndexSettings Settings { get; set; }

		IAliases Aliases { get; set; }

		IWarmers Warmers { get; set; }

		IMappings Mappings { get; set; }

		ISimilarities Similarity { get; set; }
	}
}