
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public static class TemplateBuilder
	{
		public static Func<SortFieldDescriptor<T>, IFieldSort> Section<T>(string variable, Func<SortFieldDescriptor<T>, IFieldSort> s)
			where T : class
		{
			return new Func<SortFieldDescriptor<T>, IFieldSort>(inner => new TemplateSectionSortFieldDescriptor(variable, s(inner)));
		}

		public static QueryContainer Section(string variable, QueryContainer o)
		{
			return new TemplateSectionQueryContainer(variable, o);
		}

		public static string Variable(string name, string defaultValue = null)
		{
			if (!string.IsNullOrWhiteSpace(defaultValue))
				return "{{" + name + "}}{{^" + name + "}}" + defaultValue + "{{/" + name + "}}";
			return "{{" + name + "}}";
		}

		public static IEnumerable<string> Array(string name)
		{
			return new[] {
				"{{#"+name+"}",
				"{{.}}",
				"{{/" + name + "}"
			};
		}
	}
}
