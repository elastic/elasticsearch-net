using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeGeneration.LowLevelClient.Domain
{
	public class EnumDescription
	{
		public string Name { get; set; }
		public IEnumerable<string> Options { get; set; }
	}

	public class RestApiSpec
	{
		public string Commit { get; set; }
		public IDictionary<string, ApiEndpoint> Endpoints { get; set; }

		public IList<ApiQueryParameters> ApiQueryParameters { get; set; }


		public IEnumerable<EnumDescription> EnumsInTheSpec
		{
			get
			{
				var queryParamEnums = from m in this.CsharpMethodsWithQueryStringInfo.SelectMany(m => m.Url.Params)
					where m.Value.CsharpType(m.Key).EndsWith("Options")
					select new EnumDescription
					{
						Name = m.Value.CsharpType(m.Key),
						Options = m.Value.Options

					};

				var urlParamEnums = from p in this.Endpoints.Values.SelectMany(v => v.CsharpMethods)
					.SelectMany(m => m.Parts)
					where p.Options != null && p.Options.Any()
					select new EnumDescription
					{
						Name = p.Name.ToPascalCase() + "Options",
						Options = p.Options
					};

				return queryParamEnums.Concat(urlParamEnums).DistinctBy(e=>e.Name);

			}

		}


		public IEnumerable<CsharpMethod> CsharpMethodsWithQueryStringInfo
		{
			get
			{
				return (from u in this.Endpoints.Values.SelectMany(v => v.CsharpMethods)
						where u.QueryStringParamName != "FluentQueryString"
						select u).DistinctBy(m=>m.QueryStringParamName);
					
			}
		}
	}


	//extensions methods dont work because scriptcs wraps everything
	//in its own class
}
