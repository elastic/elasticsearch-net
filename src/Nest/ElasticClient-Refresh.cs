using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using Newtonsoft.Json.Converters;

using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Reflection;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <summary>
		///  refreshes all
		/// </summary>
		/// <returns></returns>
		public IndicesShardResponse Refresh()
		{
			return this.Refresh("_all");
		}
		/// <summary>
		/// Refresh an index
		/// </summary>
		public IndicesShardResponse Refresh(string index)
		{
			index.ThrowIfNull("index");
			return this.Refresh(new []{ index });
		}
		/// <summary>
		/// Refresh multiple indices at once.
		/// </summary>
		public IndicesShardResponse Refresh(IEnumerable<string> indices)
		{
			indices.ThrowIfNull("indices");
			string path = this.CreatePath(string.Join(",", indices)) + "_refresh";
			return this._Refresh(path);
		}
		/// <summary>
		/// refresh the connection settings default index for type T
		/// </summary>
		public IndicesShardResponse Refresh<T>() where T : class
		{
      var index = this.Settings.GetIndexForType<T>();
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

			return Refresh(index);
		}
		private IndicesShardResponse _Refresh(string path)
		{
			var status = this.Connection.GetSync(path);
			var r = this.ToParsedResponse<IndicesShardResponse>(status);
			return r;
		}

	}
}
