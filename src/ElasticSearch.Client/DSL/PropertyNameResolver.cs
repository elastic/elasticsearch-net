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
		private IContractResolver ContractResolver { get; set; }
		private MethodInvoker PropertyResolver { get; set; }

		public PropertyNameResolver(JsonSerializerSettings settings)
		{
			this.SerializationSettings = settings;
			this.ContractResolver = settings.ContractResolver;

			var bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly;
			this.PropertyResolver = this.ContractResolver.GetType()
				.DelegateForCallMethod("ResolvePropertyName", bindingFlags , new[] { typeof(string) });

		}
		public string Resolve(MemberExpression memberExpression)
		{
			var segments = new List<string>();
			while (memberExpression != null)
			{
				var name = memberExpression.Member.Name;
				var resolvedName = this.PropertyResolver(this.ContractResolver, name) as string;
				segments.Insert(0, resolvedName);
				memberExpression = memberExpression.Expression as MemberExpression;
			}
			return string.Join(".",segments.ToArray());
		}
	}
}
