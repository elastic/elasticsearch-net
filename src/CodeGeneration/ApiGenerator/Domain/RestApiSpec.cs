using System.Collections.Generic;
using System.Linq;

namespace ApiGenerator.Domain
{
	public class EnumDescription
	{
		public string Name { get; set; }
		public IEnumerable<string> Options { get; set; }
	}

	public class RestApiSpec
	{
		private IEnumerable<EnumDescription> _enumDescriptions;

		public string Commit { get; set; }

		public static SortedDictionary<string, QueryParameters> CommonApiQueryParameters { get; set; }
		
		public IReadOnlyCollection<Request> Requests { get; internal set; }

		public IEnumerable<CsharpMethod> CsharpMethodsWithQueryStringInfo => Endpoints.Values
			.SelectMany(v => v.CsharpMethods)
			.Select(u => u)
			.GroupBy(m => m.QueryStringParamName)
			.Select(g => g.First());

		public IDictionary<string, ApiEndpoint> Endpoints { get; set; }

		public IEnumerable<EnumDescription> EnumsInTheSpec
		{
			get
			{
				if (_enumDescriptions != null) return _enumDescriptions;

				var queryParamEnums = from m in CsharpMethodsWithQueryStringInfo.SelectMany(m => m.Url.Params)
					where m.Value.Type == "enum"
					select new EnumDescription
					{
						Name = m.Value.ClsName,
						Options = m.Value.Options
					};

				var urlParamEnums = from data in Endpoints.Values
						.SelectMany(v => v.CsharpMethods.Select(m => new { m, n = v.MethodName }))
						.SelectMany(m => m.m.Parts.Select(part => new { m = m.n, p = part }))
					let p = data.p
					let m = data.m
					where p.Options != null && p.Options.Any()
					let name = p.Name.Contains("metric") && p.Name != "watcher_stats_metric"
						? m + p.Name.ToPascalCase()
						: p.Name.ToPascalCase()
					select new EnumDescription
					{
						Name = name,
						Options = p.Options
					};

				_enumDescriptions = queryParamEnums.Concat(urlParamEnums).DistinctBy(e => e.Name);

				return _enumDescriptions;
			}
		}
	}
}
