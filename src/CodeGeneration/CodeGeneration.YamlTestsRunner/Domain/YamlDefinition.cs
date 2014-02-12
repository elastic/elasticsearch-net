using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneration.YamlTestsRunner.Domain
{
	public class YamlDefinition
	{
		public string FileName { get; set; }
		public string Contents { get; set; }
		public IEnumerable<TestSuite> Suites { get; set; }
		public TestSuite SetupSuite { get; set; }
		public string Folder { get; set; }
		public string Suffix { get; set; }
	}
}
