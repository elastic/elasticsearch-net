using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers;
using Nest.Domain;

namespace Nest
{
	public class TemplateMappingDescriptor
	{
		internal string _Name { get; set; }
		internal TemplateMapping _TemplateMapping { get; set; }

		

		public TemplateMappingDescriptor()
		{
			this._TemplateMapping = new TemplateMapping();

		}

		public TemplateMappingDescriptor Name(string name)
		{
			name.ThrowIfNull("name");
			this._Name = name;
			return this;
		}

		/// <summary>
		/// Initialize the descriptor using the values from for instance a previous Get Template Mapping call.
		/// </summary>
		public TemplateMappingDescriptor InitializeUsing(TemplateMapping templateMapping)
		{
			this._TemplateMapping = templateMapping;
			return this;
		}

		public TemplateMappingDescriptor Order(int order)
		{
			this._TemplateMapping.Order = order;
			return this;
		}

		public TemplateMappingDescriptor Template(string template)
		{
			template.ThrowIfNull("name");
			this._TemplateMapping.Template = template;
			return this;
		}

		public TemplateMappingDescriptor Settings(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> settingsSelector)
		{
			settingsSelector.ThrowIfNull("settingsDescriptor");
			this._TemplateMapping.Settings = settingsSelector(this._TemplateMapping.Settings ?? new FluentDictionary<string, object>());
			return this;

		}

		public TemplateMappingDescriptor AddMapping<T>(Func<RootObjectMappingDescriptor<T>, RootObjectMappingDescriptor<T>> mappingSelector)
			where T : class
		{
			mappingSelector.ThrowIfNull("mappingSelector");
			var rootObjectMappingDescriptor = mappingSelector(new RootObjectMappingDescriptor<T>());
			rootObjectMappingDescriptor.ThrowIfNull("rootObjectMappingDescriptor");

			this._TemplateMapping.Mappings[rootObjectMappingDescriptor._TypeName] = rootObjectMappingDescriptor._Mapping;
			return this;

		}
		public TemplateMappingDescriptor RemoveMapping<T>()
			where T : class
		{
			var typeName = new TypeNameResolver().GetTypeNameFor<T>();
			return this.RemoveMapping(typeName);

		}
		public TemplateMappingDescriptor RemoveMapping(string typeName)
		{
			typeName.ThrowIfNull("typeName");
			this._TemplateMapping.Mappings.Remove(typeName);

			return this;

		}
	}
}
