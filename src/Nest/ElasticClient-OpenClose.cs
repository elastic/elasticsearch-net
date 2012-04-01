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
		/// Open index
		/// </summary>
		public IndicesOperationResponse OpenIndex(string index)
		{
			string path = this.CreatePath(index) + "_open";
			return this._OpenClose(path);
		}
		/// <summary>
		/// Close index
		/// </summary>
		public IndicesOperationResponse CloseIndex(string index)
		{
			string path = this.CreatePath(index) + "_close";
			return this._OpenClose(path);
		}
		/// <summary>
		/// Open the default index
		/// </summary>
		public IndicesOperationResponse OpenIndex<T>() where T : class
		{
			var index = this.Settings.DefaultIndex;
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

			return OpenIndex(index);
		}
		/// <summary>
		/// Close the default index
		/// </summary>
		public IndicesOperationResponse CloseIndex<T>() where T : class
		{
			var index = this.Settings.DefaultIndex;
			index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

			return CloseIndex(index);
		}
		private IndicesOperationResponse _OpenClose(string path)
		{
			var status = this.Connection.PostSync(path, "");
			var r = this.ToParsedResponse<IndicesOperationResponse>(status);
			return r;
		}

	}
}
