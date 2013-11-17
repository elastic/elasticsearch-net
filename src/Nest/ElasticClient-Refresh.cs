using System.Collections.Generic;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <summary>
		///  refreshes all
		/// </summary>
		/// <returns></returns>
		public IIndicesShardResponse Refresh()
		{
			return this.Refresh("_all");
		}
		/// <summary>
		/// Refresh an index
		/// </summary>
		public IIndicesShardResponse Refresh(string index)
		{
			index.ThrowIfNull("index");
			return this.Refresh(new []{ index });
		}
		/// <summary>
		/// Refresh multiple indices at once.
		/// </summary>
		public IIndicesShardResponse Refresh(IEnumerable<string> indices)
		{
			indices.ThrowIfNull("indices");
			string path = this.PathResolver.CreateIndexPath(indices, "_refresh");
			return this._Refresh(path);
		}
		/// <summary>
		/// refresh the connection settings default index for type T
		/// </summary>
		public IIndicesShardResponse Refresh<T>() where T : class
		{
			var index = this.Infer.IndexName<T>();
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

			return Refresh(index);
		}
		private IndicesShardResponse _Refresh(string path)
		{
			var status = this.Connection.PostSync(path, null);
			var r = this.Deserialize<IndicesShardResponse>(status);
			return r;
		}

	}
}
