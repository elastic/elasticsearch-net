using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Thrift;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Fasterflect;
using Newtonsoft.Json.Converters;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Reflection;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <summary>
		/// Optimize the default index
		/// </summary>
		public IndicesOperationResponse Optimize<T>() where T : class
		{
			var index = this.Settings.DefaultIndex;
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");
			return Optimize(index);
		}
		/// <summary>
		/// Optimize the default index using the specified optimize params
		/// </summary>
		public IndicesOperationResponse Optimize<T>(OptimizeParams optimizeParameters) where T : class
		{
			var index = this.Settings.DefaultIndex;
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");
			return Optimize(index, optimizeParameters);
		}
		/// <summary>
		/// Optimize the specified index
		/// </summary>
		public IndicesOperationResponse Optimize(string index)
		{
			index.ThrowIfNull("index");
			return this.Optimize(new[] { index });
		}
		/// <summary>
		/// Optimize the specified index using the specified optimize params
		/// </summary>
		public IndicesOperationResponse Optimize(string index, OptimizeParams optimizeParameters)
		{
			index.ThrowIfNull("index");
			return this.Optimize(new[] { index }, optimizeParameters);
		}
		/// <summary>
		/// Optimize all indices
		/// </summary>
		public IndicesOperationResponse Optimize()
		{
			return this.Optimize("_all");
		}
		/// <summary>
		/// Optimize all indices using the specified optimize params
		/// </summary>
		public IndicesOperationResponse Optimize(OptimizeParams optimizeParameters)
		{
			return this.Optimize("_all", optimizeParameters);
		}
		/// <summary>
		/// Optimize the specified indices
		/// </summary>
		public IndicesOperationResponse Optimize(IEnumerable<string> indices)
		{
			indices.ThrowIfNull("index");
			string path = this.CreatePath(string.Join(",", indices)) + "_optimize";
			return this._Optimize(path, null);
		}
		/// <summary>
		/// Optimize the specified indices using the specified optimize params
		/// </summary>
		public IndicesOperationResponse Optimize(IEnumerable<string> indices, OptimizeParams optimizeParameters)
		{
			indices.ThrowIfNull("index");
			string path = this.CreatePath(string.Join(",", indices)) + "_optimize";
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
			var r = this.ToParsedResponse<IndicesOperationResponse>(status);
			return r;
		}

	}
}
