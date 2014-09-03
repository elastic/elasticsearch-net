using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonConverter(typeof(TemplateSectionConverter))]
	public class TemplateSectionQueryContainer : QueryContainer, ITemplateSection
	{
		private readonly QueryContainer _queryContainer;
		private readonly string _variable;
		object ITemplateSection.Instance { get { return _queryContainer; } }
		string ITemplateSection.Variable { get { return _variable; } }

		public TemplateSectionQueryContainer(string variable, QueryContainer o)
		{
			_variable = variable;
			_queryContainer = o;
		}
	}
}
