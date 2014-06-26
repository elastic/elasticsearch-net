using System.Collections.Generic;
using System.Linq;

namespace CodeGeneration.LowLevelClient.Domain
{
	public class ApiQueryParameters
	{
		public string Type { get; set; }
		public string Description { get; set; }
		public IEnumerable<string> Options { get; set; }

		public string CsharpType(string paramName)
		{
			switch (this.Type)
			{
				case "boolean":
					return "bool";
				case "list":
					return "params string[]";
				case "number":
					return new [] {"boost", "percen"}.Any(s=>paramName.ToLowerInvariant().Contains(s)) 
						? "double" : "long";
				case "time":
				case "duration":
				case "text":
				case "":
				case null:
					return "string";
				case "enum":
					return paramName.ToPascalCase();
				default:
					return this.Type;
			}
		}
		
	}
}