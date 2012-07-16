using System.Collections.Generic;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <summary>
		/// Snapshot all indices
		/// </summary>
		public IIndicesShardResponse Snapshot()
		{
			return this.Snapshot("_all");
		}
		/// <summary>
		/// Snapshot the default index
		/// </summary>
		public IIndicesShardResponse Snapshot<T>() where T : class
		{
            var index = this.Settings.GetIndexForType<T>();
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

			return Snapshot(index);
		}
		/// <summary>
		/// Snapshot the specified index
		/// </summary>
		public IIndicesShardResponse Snapshot(string index)
		{
			index.ThrowIfNull("index");
			return this.Snapshot(new[] { index });
		}
		/// <summary>
		/// Snapshot the specified indices
		/// </summary>
		public IIndicesShardResponse Snapshot(IEnumerable<string> indices)
		{
			indices.ThrowIfNull("indices");
			string path = this.CreatePath(string.Join(",", indices)) + "_gateway/snapshot";
			return this._Snapshot(path);
		}
		private IndicesShardResponse _Snapshot(string path)
		{
			var status = this.Connection.PostSync(path, "");
			var r = this.ToParsedResponse<IndicesShardResponse>(status);
			return r;
		}

	}
}
