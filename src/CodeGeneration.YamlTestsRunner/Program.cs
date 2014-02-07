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
			var specification = YamlTestsGenerator.GetYamlTestSpecification();

			YamlTestsGenerator.GenerateProject(specification);
		}
	}
}
