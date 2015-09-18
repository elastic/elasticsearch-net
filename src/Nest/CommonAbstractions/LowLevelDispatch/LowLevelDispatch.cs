using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Elasticsearch.Net.Serialization;

namespace Nest
{
	internal partial class LowLevelDispatch
	{
		protected IElasticsearchClient _lowLevel;

		public LowLevelDispatch(IElasticsearchClient rawElasticClient)
		{
			this._lowLevel = rawElasticClient;
		}

		internal bool AllSet(params string[] pathVariables) => pathVariables.All(p => !p.IsNullOrEmpty());

        internal bool AllSet(params IUrlParameter[] pathVariables) => pathVariables.All(p => p != null);
	}
}
