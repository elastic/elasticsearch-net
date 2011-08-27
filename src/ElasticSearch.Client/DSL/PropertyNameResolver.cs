using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Linq.Expressions;
using System.Reflection;
using Fasterflect;

namespace ElasticSearch.Client.DSL
{
	class PropertyNameResolver
	{
		private JsonSerializerSettings SerializationSettings { get; set; }
		private ElasticResolver ContractResolver { get; set; }

		public PropertyNameResolver(JsonSerializerSettings settings)
		{
			this.SerializationSettings = settings;
			this.ContractResolver = settings.ContractResolver as ElasticResolver;

			var t = this.ContractResolver.GetType();

			var bindingFlags = BindingFlags.FlattenHierarchy;

		}
		public string Resolve(MemberExpression memberExpression)
		{
			var segments = new List<string>();
			while (memberExpression != null)
			{
				var name = memberExpression.Member.Name;
				var resolvedName = this.ContractResolver.ResolvePropertyName(name);
				segments.Insert(0, resolvedName);
				memberExpression = memberExpression.Expression as MemberExpression;
			}
			return string.Join(".",segments.ToArray());
		}
	}
}
