using System;
using System.Reflection;
using Newtonsoft.Json;

namespace Nest
{
	[AttributeUsage(AttributeTargets.Property)]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public abstract class ElasticsearchCorePropertyAttributeBase : ElasticsearchPropertyAttributeBase, ICoreProperty
	{
		private ICoreProperty Self => this;

		bool? ICoreProperty.Store { get; set; }
		IProperties ICoreProperty.Fields { get; set; }
		Fields ICoreProperty.CopyTo { get; set; }

		public string Similarity { get; set; }
		public bool Store { get { return Self.Store.GetValueOrDefault(); } set { Self.Store = value; } }

		protected ElasticsearchCorePropertyAttributeBase(string typeName) : base(typeName)
		{
		}

		protected ElasticsearchCorePropertyAttributeBase(Type type) : base(type)
		{
		}

		public new static ElasticsearchCorePropertyAttributeBase From(MemberInfo memberInfo)
		{
			return memberInfo.GetCustomAttribute<ElasticsearchCorePropertyAttributeBase>(true);
		}
	}
}
