using System;
using System.Collections.Generic;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IObservable<IReindexResponse<T>> Reindex<T>(Func<ReindexDescriptor<T>, ReindexDescriptor<T>> reindexSelector)
			where T : class
		{
			reindexSelector.ThrowIfNull("reindexSelector"); 
			var reindexDescriptor = reindexSelector(new ReindexDescriptor<T>());
			var observable = new ReindexObservable<T>(this, _connectionSettings, reindexDescriptor);
			return observable;
		}

		/// <inheritdoc />
		public IObservable<IReindexResponse<IDocument>> Reindex(Func<ReindexDescriptor<IDocument>, ReindexDescriptor<IDocument>> reindexSelector)
		{
			reindexSelector.ThrowIfNull("reindexSelector");
			var reindexDescriptor = reindexSelector(new ReindexDescriptor<IDocument>());

			reindexDescriptor._allTypes = true;

			var observable = new ReindexObservable<IDocument>(this, _connectionSettings, reindexDescriptor);
			return observable;
		}
	}
}