using System.Collections.Generic;

namespace Nest
{
	public interface IIndexState
	{
		dynamic AsExpando { get; }

		Dictionary<string, ICreateAliasOperation> Aliases { get; set; }
		AnalysisSettings Analysis { get; set; }
		IList<TypeMapping> Mappings { get; set; }
		int? NumberOfReplicas { get; set; }
		int? NumberOfShards { get; set; }
		IDictionary<string, object> Settings { get; set; }
		SimilaritySettings Similarity { get; }
		Dictionary<string, WarmerMapping> Warmers { get; set; }
	}
}