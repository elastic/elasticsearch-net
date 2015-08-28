using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using Elasticsearch.Net;

namespace Nest
{
	public class IndexState : IIndexState
	{
		public IIndexSettings Settings { get; set; }
		
		public IAnalysisSettings Analysis { get; set; }

		public IMappings Mappings { get; set; }

		[JsonConverter(typeof(DictionaryKeysAreNotFieldNamesJsonConverter))]
		public Dictionary<string, IAlias> Aliases { get; set; }
			
		[JsonConverter(typeof(DictionaryKeysAreNotFieldNamesJsonConverter))]
		public IWarmers Warmers { get; set; }

		public SimilaritySettings Similarity { get; internal set; }
	}





}