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
		/// Get segment information for all the indices
		/// </summary>
		public SegmentsResponse Segments()
		{
			return _Segments("_segments");
		}
		/// <summary>
		/// Get the segment information for the specified index
		/// </summary>
		public SegmentsResponse Segments(string index)
		{
			return this.Segments(new [] { index });
		}
		/// <summary>
		/// Get the segment information for the specified indices
		/// </summary>
		public SegmentsResponse Segments(IEnumerable<string> indices)
		{
			var path = this.CreatePath(string.Join(",", indices)) + "_segments";
			return this._Segments(path);
		}
		private SegmentsResponse _Segments(string path)
		{
			var status = this.Connection.GetSync(path);
			var r = this.ToParsedResponse<SegmentsResponse>(status);
			return r;
		}
	}
}
