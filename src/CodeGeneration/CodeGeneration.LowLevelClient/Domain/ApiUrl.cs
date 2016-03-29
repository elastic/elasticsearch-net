using System.Collections.Generic;
using System.Linq;

namespace CodeGeneration.LowLevelClient.Domain
{
	// ReSharper disable once ClassNeverInstantiated.Global
	public class ApiUrl
	{
		//these are aliases we much rather pass along inside the querystring (or body)
		//allowing these will cause too many overloads being generated which helps noone
		public static readonly string[] BlackListRouteValues = { "{search_groups}", "{indexing_types}", "{body}", "{scroll_id}" };
		private IEnumerable<string> _paths;

		public string Path { get; set; }

		public IEnumerable<string> Paths
		{
			get
			{
				return _paths?.Where(p => !BlackListRouteValues.Any(p.Contains))
					.ToList() ?? _paths;
			}
			set { _paths = value; }
		}

		public IDictionary<string, ApiUrlPart> Parts { get; set; }
		public IDictionary<string, ApiQueryParameters> Params { get; set; }

	}
}