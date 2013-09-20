namespace Nest
{
	public partial class ElasticClient
	{
		/// <summary>
		/// Open index
		/// </summary>
		public IIndicesOperationResponse OpenIndex(string index)
		{
			string path = this.PathResolver.CreateIndexPath(index, "_open");
			return this._OpenClose(path);
		}
		/// <summary>
		/// Close index
		/// </summary>
		public IIndicesOperationResponse CloseIndex(string index)
		{
			string path = this.PathResolver.CreateIndexPath(index, "_close");
			return this._OpenClose(path);
		}
		/// <summary>
		/// Open the default index
		/// </summary>
		public IIndicesOperationResponse OpenIndex<T>() where T : class
		{
			var index = this.IndexNameResolver.GetIndexForType<T>();
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

			return OpenIndex(index);
		}
		/// <summary>
		/// Close the default index
		/// </summary>
		public IIndicesOperationResponse CloseIndex<T>() where T : class
		{
			var index = this.IndexNameResolver.GetIndexForType<T>();
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

			return CloseIndex(index);
		}
		private IndicesOperationResponse _OpenClose(string path)
		{
			var status = this.Connection.PostSync(path, "");
			var r = this.Deserialize<IndicesOperationResponse>(status);
			return r;
		}

	}
}
