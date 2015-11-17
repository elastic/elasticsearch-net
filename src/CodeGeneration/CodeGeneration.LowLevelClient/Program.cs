using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeGeneration.LowLevelClient
{
	public static class Program
	{
		static void Main(string[] args)
		{
			var useCache = args.Length > 0 && args[0] == "cache";

			if (!useCache)
				ApiGenerator.GenerateEndpointFiles();

			var spec = ApiGenerator.GetRestApiSpec();

			ApiGenerator.GenerateClientInterface(spec);

			ApiGenerator.GenerateRequestParameters(spec);

			ApiGenerator.GenerateRequestParametersExtensions(spec);

			ApiGenerator.GenerateDescriptors(spec);

			ApiGenerator.GenerateRequests(spec);

			ApiGenerator.GenerateEnums(spec);

			ApiGenerator.GenerateRawClient(spec);

			ApiGenerator.GenerateRawDispatch(spec);

			Console.WriteLine("Found {0} api documentation endpoints", spec.Endpoints.Count);
		}

	}
}
