using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CodeGeneration.LowLevelClient
{
	public static class Program
	{
		static void Main(string[] args)
		{
			var useCache = args.Length > 0 && args[0] == "cache";
			var directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());

			var generator = new ApiGenerator();

			if (!useCache)
				generator.GenerateEndpointFiles();

			var spec = generator.GetRestApiSpec();

			generator.GenerateClientInterface(spec);

			generator.GenerateRequestParameters(spec);

			generator.GenerateRequestParametersExtensions(spec);

			generator.GenerateDescriptors(spec);

			generator.GenerateRequests(spec);

			generator.GenerateEnums(spec);

			generator.GenerateRawClient(spec);

			generator.GenerateRawDispatch(spec);

			Console.WriteLine("Found {0} api documentation endpoints", spec.Endpoints.Count);
		}

	}
}
