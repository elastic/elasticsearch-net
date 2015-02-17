
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public class MappingTransformDescriptor
	{
		internal MappingTransform _mappingTransform = new MappingTransform();

		public MappingTransformDescriptor Script(string script)
		{
			this._mappingTransform.Script = script;
			return this;
		}

		public MappingTransformDescriptor Params(IDictionary<string, string> parameters)
		{
			this._mappingTransform.Parameters = parameters;
			return this;
		}

		public MappingTransformDescriptor Language(string language)
		{
			this._mappingTransform.Language = language;
			return this;
		}

		public MappingTransformDescriptor Language(ScriptLang language)
		{
			this._mappingTransform.Language = language.GetStringValue();
			return this;
		}
	}
}
