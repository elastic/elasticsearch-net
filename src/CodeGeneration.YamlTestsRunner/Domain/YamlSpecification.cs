using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneration.YamlTestsRunner.Domain
{
	public class YamlSpecification
	{
		/// <summary>
		/// The commit we generated this specification from.
		/// </summary>
		public string Commit { get; set; }

		public Dictionary<string, IList<YamlDefinition>> Definitions { get; set; }
	}
}
