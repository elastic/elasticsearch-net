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
using Nest.DSL;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Reflection;
using Nest.Domain;

namespace Nest
{
	public partial class ElasticClient
	{
		public IndicesOperationResponse Optimize<T>() where T : class
		{
			var index = this.Settings.DefaultIndex;
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");
			return Optimize(index);
		}
		public IndicesOperationResponse Optimize<T>(OptimizeParams optimizeParameters) where T : class
		{
			var index = this.Settings.DefaultIndex;
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");
			return Optimize(index, optimizeParameters);
		}
		public IndicesOperationResponse Optimize(string index)
		{
			index.ThrowIfNull("index");
			return this.Optimize(new[] { index });
		}
		public IndicesOperationResponse Optimize(string index, OptimizeParams optimizeParameters)
		{
			index.ThrowIfNull("index");
			return this.Optimize(new[] { index }, optimizeParameters);
		}
		public IndicesOperationResponse Optimize()
		{
			return this.Optimize("_all");
		}
		public IndicesOperationResponse Optimize(OptimizeParams optimizeParameters)
		{
			return this.Optimize("_all", optimizeParameters);
		}
		public IndicesOperationResponse Optimize(IEnumerable<string> indices)
		{
			indices.ThrowIfNull("index");
			string path = this.CreatePath(string.Join(",", indices)) + "_optimize";
			return this._Optimize(path, null);
		}
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
