using System.Collections.Generic;

namespace Nest
{
	public interface IIndexState
	{
		Dictionary<string, ICreateAliasOperation> Aliases { get; set; }
		SimilaritySettings Similarity { get; }

		IAnalysisSettings Analysis { get; set; }
		IWarmers Warmers { get; set; }
		IMappings Mappings { get; set; }
		IIndexSettings Settings { get; set; }
	}
}