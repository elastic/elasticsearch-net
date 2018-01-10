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
		public IDictionary<string, ApiEndpoint> Endpoints { get; set; }

		public static Dictionary<string, ApiQueryParameters> CommonApiQueryParameters { get; set; }

		public IEnumerable<EnumDescription> EnumsInTheSpec
		{
			get
			{
				if (_enumDescriptions != null) return _enumDescriptions;
				var queryParamEnums = from m in this.CsharpMethodsWithQueryStringInfo.SelectMany(m => m.Url.Params)
					where m.Value.Type == "enum"
					select new EnumDescription
					{
						Name = m.Value.ClsName,
						Options = m.Value.Options
					};

				var urlParamEnums = from data in this.Endpoints.Values
						.SelectMany(v => v.CsharpMethods.Select(m => new { m, n = v.CsharpMethodName }))
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


		public IEnumerable<CsharpMethod> CsharpMethodsWithQueryStringInfo =>
			(from u in this.Endpoints.Values.SelectMany(v => v.CsharpMethods)
			where u.QueryStringParamName != "FluentQueryString"
			select u).GroupBy(m => m.QueryStringParamName).Select(g =>
		{
			if (g.Count() == 1) return g.First();
			return g.OrderBy(v =>
			{
				switch (v.HttpMethod.ToUpper())
				{
					case "GET": return 1;
					default: return 0;
				}
			}).First();
		});
	}
}
