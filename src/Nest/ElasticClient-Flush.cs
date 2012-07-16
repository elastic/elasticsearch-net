using System.Collections.Generic;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <summary>
		/// <para>Flushes the infered typename for T under the default index </para>
		/// <para>The flush process of an index basically frees memory from the index by flushing data to the index storage and clearing the internal transaction log. By default, ElasticSearch uses memory heuristics in order to automatically trigger flush operations as required in order to clear memory.</para>
		/// </summary>
		/// <param name="refresh">optional, wait for the flush operation to complete</param>
		public IIndicesOperationResponse Flush<T>(bool refresh = false) where T : class
		{
            var index = this.Settings.GetIndexForType<T>();
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

			return Flush(index, refresh);
		}

		/// <summary>
		/// <para>Flushes all indices</para>
		/// <para>The flush process of an index basically frees memory from the index by flushing data to the index storage and clearing the internal transaction log. By default, ElasticSearch uses memory heuristics in order to automatically trigger flush operations as required in order to clear memory.</para>
		/// </summary>
		/// <param name="refresh">optional, wait for the flush operation to complete</param>
		public IIndicesOperationResponse Flush(bool refresh = false)
		{
			return this.Flush("_all", refresh);
		}
		/// <summary>
		/// <para>Flushes the specified index</para>
		/// <para>The flush process of an index basically frees memory from the index by flushing data to the index storage and clearing the internal transaction log. By default, ElasticSearch uses memory heuristics in order to automatically trigger flush operations as required in order to clear memory.</para>
		/// </summary>
		/// <param name="refresh">optional, wait for the flush operation to complete</param>
		public IIndicesOperationResponse Flush(string index, bool refresh = false)
		{
			index.ThrowIfNull("index");
			return this.Flush(new[] { index }, refresh);
		}
		/// <summary>
		/// <para>Flushes the specified indices</para>
		/// <para>The flush process of an index basically frees memory from the index by flushing data to the index storage and clearing the internal transaction log. By default, ElasticSearch uses memory heuristics in order to automatically trigger flush operations as required in order to clear memory.</para>
		/// </summary>
		/// <param name="refresh">optional, wait for the flush operation to complete</param>
		public IIndicesOperationResponse Flush(IEnumerable<string> indices, bool refresh = false)
		{
			indices.ThrowIfNull("index");
			string path = this.CreatePath(string.Join(",", indices)) + "_flush?refresh=" + refresh.ToString().ToLower();
			return this._Flush(path);
		}
        
		private IndicesOperationResponse _Flush(string path)
		{
			var status = this.Connection.PostSync(path, "");
			var r = this.ToParsedResponse<IndicesOperationResponse>(status);
			return r;
		}
	}
}
