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
		public string Commit { get; set; }

		public static SortedDictionary<string, QueryParameters> CommonApiQueryParameters { get; set; }
		
		public IDictionary<string, ApiEndpoint> Endpoints { get; set; }

		private IEnumerable<EnumDescription> _enumDescriptions;
		public IEnumerable<EnumDescription> EnumsInTheSpec
		{
			get
			{
				if (_enumDescriptions != null) return _enumDescriptions;

				string CreateName(string name, string methodName)
				{
					if (
						name.ToLowerInvariant().Contains("metric")
						 ||(name.ToLowerInvariant() == "status")
					) 
						return methodName + name;

					return name;
				}

				var urlParameterEnums = (
					from e in Endpoints.Values 
					from para in e.Url.Params.Values 
					where para.Options != null && para.Options.Any() 
					select new EnumDescription
					{
						Name = CreateName(para.ClsName, e.CsharpNames.MethodName),
						Options = para.Options
					}).ToList();
				
				var urlPartEnums = (
					from e in Endpoints.Values 
					from part in e.Url.Parts 
					where part.Options != null && part.Options.Any() 
					select new EnumDescription
					{
						Name = CreateName(part.Name.ToPascalCase(), e.CsharpNames.MethodName),
						Options = part.Options
					}).ToList();

				_enumDescriptions = urlPartEnums.Concat(urlParameterEnums).DistinctBy(e => e.Name).ToList();
				return _enumDescriptions;
			}
		}
	}
}
