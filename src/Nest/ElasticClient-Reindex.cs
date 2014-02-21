using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{

	public partial class ElasticClient
	{
		/// <summary>
		/// Index object to the default index and the inferred type name for T
		/// </summary>
		public IObservable<IReindexResponse<T>> Reindex<T>(Func<ReindexDescriptor<T>, ReindexDescriptor<T>> reindexSelector) where T : class
		{
			reindexSelector.ThrowIfNull("reindexSelector");
			var reindexDescriptor = reindexSelector(new ReindexDescriptor<T>());
			var observable = new ReindexObservable<T>(this, this._connectionSettings, reindexDescriptor);
			return observable;
		}
		
	}
}
