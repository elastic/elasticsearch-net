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
		/// Snapshot all indices
		/// </summary>
		public IndicesShardResponse Snapshot()
		{
			return this.Snapshot("_all");
		}
		/// <summary>
		/// Snapshot the default index
		/// </summary>
		public IndicesShardResponse Snapshot<T>() where T : class
		{
			var index = this.Settings.DefaultIndex;
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

			return Snapshot(index);
		}
		/// <summary>
		/// Snapshot the specified index
		/// </summary>
		public IndicesShardResponse Snapshot(string index)
		{
			index.ThrowIfNull("index");
			return this.Snapshot(new[] { index });
		}
		/// <summary>
		/// Snapshot the specified indices
		/// </summary>
		public IndicesShardResponse Snapshot(IEnumerable<string> indices)
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
