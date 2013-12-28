using System.Collections.Generic;

namespace RawClientGenerator
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
					return "int";
				case "time":
				case "duration":
				case "text":
				case "":
				case null:
					return "string";
				case "enum":
					return paramName.ToPascalCase() + "Options";
				default:
					return this.Type;
			}
		}
		
	}
}