using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Nest.Resolvers;
using Elasticsearch.Net;

namespace Nest
{
	public class DynamicTemplatesDescriptor<T> where T : class
	{
		internal IDictionary<string, DynamicTemplate>  Templates = new Dictionary<string, DynamicTemplate>();
		internal IList<string> _Deletes = new List<string>();
		private readonly IConnectionSettingsValues _connectionSettings;

		public DynamicTemplatesDescriptor(IConnectionSettingsValues connectionSettings)
		{
			this._connectionSettings = connectionSettings;
		}

		public DynamicTemplatesDescriptor<T> Add(
			Func<DynamicTemplateDescriptor<T>, DynamicTemplateDescriptor<T>> templateSelector)
		{
			templateSelector.ThrowIfNull("templateSelector");
			var d = templateSelector(new DynamicTemplateDescriptor<T>(this._connectionSettings));
			if (d == null || d._Name.IsNullOrEmpty())
				throw new Exception("Could not get name for dynamic template");

			this.Templates[d._Name] = d._Template;
			return this;
		}
		public DynamicTemplatesDescriptor<T> Remove(string name)
		{
			this._Deletes.Add(name);
			return this;
		}
	}
	public class DynamicTemplateDescriptor<T> where T : class
	{
		private readonly IConnectionSettingsValues _connectionSettings;

		internal string _Name { get; private set; }
		internal DynamicTemplate _Template { get; private set; }

		public DynamicTemplateDescriptor(IConnectionSettingsValues connectionSettings)
		{
			this._Template = new DynamicTemplate();
			this._connectionSettings = connectionSettings;
		}

		public DynamicTemplateDescriptor<T> Name(string name)
		{
			this._Name = name;
			return this;
		}

		public DynamicTemplateDescriptor<T> Match(string match)
		{
			this._Template.Match = match;
			return this;
		}

		public DynamicTemplateDescriptor<T> Unmatch(string unMatch)
		{
			this._Template.Unmatch = unMatch;
			return this;
		}

		public DynamicTemplateDescriptor<T> MatchMappingType(string matchMappingType)
		{
			this._Template.MatchMappingType = matchMappingType;
			return this;
		}

		public DynamicTemplateDescriptor<T> PathMatch(string pathMatch)
		{
			this._Template.PathMatch = pathMatch;
			return this;
		}

		public DynamicTemplateDescriptor<T> PathUnmatch(string pathUnmatch)
		{
			this._Template.PathUnmatch = pathUnmatch;
			return this;
		}

		public DynamicTemplateDescriptor<T> Mapping(Func<SingleMappingDescriptor<T>, IElasticType> mappingSelector)
		{
			mappingSelector.ThrowIfNull("mappingSelector");
			var mapping = mappingSelector(new SingleMappingDescriptor<T>(this._connectionSettings));

			this._Template.Mapping = mapping;
			return this;
		}


	}
}