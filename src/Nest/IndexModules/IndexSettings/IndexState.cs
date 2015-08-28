using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using Elasticsearch.Net;

namespace Nest
{
	public class IndexState : IIndexState
	{
		public IIndexSettings Settings { get; set; }
		
		public IMappings Mappings { get; set; }

		public IAliases Aliases { get; set; }
			
		public IWarmers Warmers { get; set; }

		public ISimilarities Similarity { get; set; }
	}





}