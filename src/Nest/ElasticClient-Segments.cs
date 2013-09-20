using System.Collections.Generic;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <summary>
		/// Get segment information for all the indices
		/// </summary>
		public ISegmentsResponse Segments()
		{
			return _Segments("_segments");
		}
		/// <summary>
		/// Get the segment information for the specified index
		/// </summary>
		public ISegmentsResponse Segments(string index)
		{
			return this.Segments(new [] { index });
		}
		/// <summary>
		/// Get the segment information for the specified indices
		/// </summary>
		public ISegmentsResponse Segments(IEnumerable<string> indices)
		{
			var path = this.PathResolver.CreateIndexPath(indices, "_segments");
			return this._Segments(path);
		}
		private SegmentsResponse _Segments(string path)
		{
			var status = this.Connection.GetSync(path);
			var r = this.Deserialize<SegmentsResponse>(status);
			return r;
		}
	}
}
