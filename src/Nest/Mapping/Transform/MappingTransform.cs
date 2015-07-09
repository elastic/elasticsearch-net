using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class MappingTransform
	{
		[JsonProperty("script")]
		public string Script { get; set; }

        [JsonProperty("script_file")]
        public string ScriptFile { get; set; }

		[JsonProperty("params")]
		public IDictionary<string, string> Parameters { get; set; }

		[JsonProperty("lang")]
		public string Language { get; set; }
	}

	public class MappingTransformDescriptor
	{
		internal MappingTransform _mappingTransform = new MappingTransform();

		public MappingTransformDescriptor Script(string script)
		{
			this._mappingTransform.Script = script;
			return this;
		}

        public MappingTransformDescriptor ScriptFile(string scriptFile)
        {
            this._mappingTransform.ScriptFile = scriptFile;
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
