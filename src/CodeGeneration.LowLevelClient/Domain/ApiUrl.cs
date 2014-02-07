using System.Collections.Generic;
using System.Linq;

namespace CodeGeneration.LowLevelClient.Domain
{
	public class ApiUrl
	{
		//these are aliases we much rather pass along inside the querystring
		//allowing these will cause too many overloads being generated which helps noone
		private static readonly string[] _blackList = {"{metric_family}", "{metric}", "{fields}", "{search_groups}", "{indexing_types}"};
		private IEnumerable<string> _paths;

		public string Path { get; set; }

		public IEnumerable<string> Paths
		{
			get
			{
				return _paths == null ? _paths : _paths
					.Where(p => !_blackList.Any(p.Contains))
					.ToList();
			}
			set { _paths = value; }
		}

		public IDictionary<string, ApiUrlPart> Parts { get; set; }
		public IDictionary<string, ApiQueryParameters> Params { get; set; }
	}
}