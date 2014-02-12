using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneration.YamlTestsRunner
{
	class Program
	{
		static void Main(string[] args)
		{
			var useCache = (args.Length > 0 && args[0] == "cache");

			var specification = YamlTestsGenerator.GetYamlTestSpecification(useCache);

			YamlTestsGenerator.GenerateProject(specification);
		}
	}
}
