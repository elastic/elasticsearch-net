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
	[DescriptorFor("IndicesPutTemplate")]
	public partial class PutTemplateDescriptor : NamePathDescriptor<PutTemplateDescriptor, PutTemplateQueryString>
		, IPathInfo<PutTemplateQueryString>
	{
		private readonly IConnectionSettings _connectionSettings;

		internal TemplateMapping _TemplateMapping { get; set; }

		public PutTemplateDescriptor(IConnectionSettings connectionSettings)
		{
			_connectionSettings = connectionSettings;
			this._TemplateMapping = new TemplateMapping();
		}


		/// <summary>
		/// Initialize the descriptor using the values from for instance a previous Get Template Mapping call.
		/// </summary>
		public PutTemplateDescriptor InitializeUsing(TemplateMapping templateMapping)
		{
			this._TemplateMapping = templateMapping;
			return this;
		}

		public PutTemplateDescriptor Order(int order)
		{
			this._TemplateMapping.Order = order;
			return this;
		}

		public PutTemplateDescriptor Template(string template)
		{
			template.ThrowIfNull("name");
			this._TemplateMapping.Template = template;
			return this;
		}

		public PutTemplateDescriptor Settings(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> settingsSelector)
		{
			settingsSelector.ThrowIfNull("settingsDescriptor");
			this._TemplateMapping.Settings = settingsSelector(this._TemplateMapping.Settings ?? new FluentDictionary<string, object>());
			return this;
		}

		public PutTemplateDescriptor AddMapping<T>(Func<PutMappingDescriptor<T>, PutMappingDescriptor<T>> mappingSelector)
			where T : class
		{
			mappingSelector.ThrowIfNull("mappingSelector");
			var rootObjectMappingDescriptor = mappingSelector(new PutMappingDescriptor<T>(this._connectionSettings));
			rootObjectMappingDescriptor.ThrowIfNull("rootObjectMappingDescriptor");

			var typeName = rootObjectMappingDescriptor._Type.Resolve(this._connectionSettings);
			this._TemplateMapping.Mappings[typeName] = rootObjectMappingDescriptor._Mapping;
			return this;

		}

		public PutTemplateDescriptor AddWarmer<T>(Func<CreateWarmerDescriptor, CreateWarmerDescriptor> warmerSelector)
			where T : class
		{
			warmerSelector.ThrowIfNull("warmerSelector");
			var warmerDescriptor = warmerSelector(new CreateWarmerDescriptor());
			warmerDescriptor.ThrowIfNull("warmerDescriptor");
			warmerDescriptor._WarmerName.ThrowIfNull("warmer has no name");

			var warmer = new WarmerMapping
			{
				Name = warmerDescriptor._WarmerName, 
				Types = warmerDescriptor._Types, 
				Source = warmerDescriptor._SearchDescriptor
			};

			this._TemplateMapping.Warmers[warmerDescriptor._WarmerName] = warmer;
			return this;

		}

		ElasticSearchPathInfo<PutTemplateQueryString> IPathInfo<PutTemplateQueryString>.ToPathInfo(IConnectionSettings settings)
		{
			var pathInfo = base.ToPathInfo<PutTemplateQueryString>(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.PUT;
			pathInfo.Name = this._Name;
			return pathInfo;
		}

	}
}
