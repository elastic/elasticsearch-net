using System.Collections.Generic;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <summary>
		/// Optimize the default index
		/// </summary>
		public IIndicesOperationResponse Optimize<T>() where T : class
		{
			var index = this.IndexNameResolver.GetIndexForType<T>();
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");
			return Optimize(index);
		}
		/// <summary>
		/// Optimize the default index using the specified optimize params
		/// </summary>
		public IIndicesOperationResponse Optimize<T>(OptimizeParams optimizeParameters) where T : class
		{
			var index = this.IndexNameResolver.GetIndexForType<T>();
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");
			return Optimize(index, optimizeParameters);
		}
		/// <summary>
		/// Optimize the specified index
		/// </summary>
		public IIndicesOperationResponse Optimize(string index)
		{
			index.ThrowIfNull("index");
			return this.Optimize(new[] { index });
		}
		/// <summary>
		/// Optimize the specified index using the specified optimize params
		/// </summary>
		public IIndicesOperationResponse Optimize(string index, OptimizeParams optimizeParameters)
		{
			index.ThrowIfNull("index");
			return this.Optimize(new[] { index }, optimizeParameters);
		}
		/// <summary>
		/// Optimize all indices
		/// </summary>
		public IIndicesOperationResponse Optimize()
		{
			return this.Optimize("_all");
		}
		/// <summary>
		/// Optimize all indices using the specified optimize params
		/// </summary>
		public IIndicesOperationResponse Optimize(OptimizeParams optimizeParameters)
		{
			return this.Optimize("_all", optimizeParameters);
		}
		/// <summary>
		/// Optimize the specified indices
		/// </summary>
		public IIndicesOperationResponse Optimize(IEnumerable<string> indices)
		{
			indices.ThrowIfNull("index");
			string path = this.PathResolver.CreateIndexPath(indices, "_optimize");
			return this._Optimize(path, null);
		}
		/// <summary>
		/// Optimize the specified indices using the specified optimize params
		/// </summary>
		public IIndicesOperationResponse Optimize(IEnumerable<string> indices, OptimizeParams optimizeParameters)
		{
			indices.ThrowIfNull("index");
			string path = this.PathResolver.CreateIndexPath(indices, "_optimize");
			return this._Optimize(path, optimizeParameters);
		}

		private IndicesOperationResponse _Optimize(string path, OptimizeParams optimizeParameters)
		{
			if (optimizeParameters != null)
			{
				path += "?max_num_segments=" + optimizeParameters.MaximumSegments;
				path += "&only_expunge_deletes=" + optimizeParameters.OnlyExpungeDeletes.ToString().ToLower();
				path += "&refresh=" + optimizeParameters.Refresh.ToString().ToLower();
				path += "&flush=" + optimizeParameters.Flush.ToString().ToLower();
				path += "&wait_for_merge=" + optimizeParameters.WaitForMerge.ToString().ToLower();
			}
			var status = this.Connection.PostSync(path, "");
			var r = this.Deserialize<IndicesOperationResponse>(status);
			return r;
		}

	}
}
