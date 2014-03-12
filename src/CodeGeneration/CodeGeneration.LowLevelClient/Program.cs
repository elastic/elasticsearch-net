using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeGeneration.LowLevelClient
{
	class Program
	{
		static void Main(string[] args)
		{
			var useCache = (args.Length > 0 && args[0] == "cache");

			var spec = ApiGenerator.GetRestSpec(useCache: useCache);

			ApiGenerator.GenerateClientInterface(spec);

			ApiGenerator.GenerateQueryStringParameters(spec);
			ApiGenerator.GenerateQueryStringParametersExtensions(spec);
			
			ApiGenerator.GenerateDescriptors(spec);

			ApiGenerator.GenerateEnums(spec);

			ApiGenerator.GenerateRawClient(spec);
			
			ApiGenerator.GenerateRawDispatch(spec);

			Console.WriteLine("Found {0} api documentation endpoints", spec.Endpoints.Count());

			var x  = GetT<string>();
		}


		static T GetT<T>()
		{
			return default(T);
		}
	}
}
