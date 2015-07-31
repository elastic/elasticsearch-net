using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Nest.Resolvers.Writers;
using Newtonsoft.Json;

namespace Nest
{
	public interface INestedType : IElasticType
	{

	}

	[JsonObject(MemberSerialization.OptIn)]
	public class NestedType : ObjectType, INestedType
	{
		public NestedType() : base("nested") { }

		[JsonProperty("include_in_parent")]
		public bool? IncludeInParent { get; set; }

		[JsonProperty("include_in_root")]
		public bool? IncludeInRoot { get; set; }
	}

	public class NestedObjectTypeDescriptor<TParent, TChild>
		where TParent : class
		where TChild : class

	{
		private readonly IConnectionSettingsValues _connectionSettings;

		internal NestedType _Mapping { get; set; }
		internal TypeName _TypeName { get; set; }
		public ElasticInferrer Infer { get; set; }

		public NestedObjectTypeDescriptor(IConnectionSettingsValues connectionSettings)
		{
			this._connectionSettings = connectionSettings;
			this._TypeName = TypeName.Create<TChild>();
			this._Mapping = new NestedType() { };
			this.Infer = new ElasticInferrer(this._connectionSettings);
		}
		public NestedObjectTypeDescriptor<TParent, TChild> Name(string name)
		{
			this._Mapping.Name = name;
			return this;
		}

		public NestedObjectTypeDescriptor<TParent, TChild> Name(Expression<Func<TParent, TChild>> objectPath)
		{
			this._Mapping.Name = objectPath;
			return this;
		}

		public NestedObjectTypeDescriptor<TParent, TChild> Name(Expression<Func<TParent, IEnumerable<TChild>>> objectPath)
		{
			this._Mapping.Name = objectPath;
			return this;
		}
		/// <summary>
		/// Convenience method to map from most of the object from the attributes/properties.
		/// Later calls on the fluent interface can override whatever is set is by this call. 
		/// This helps mapping all the ints as ints, floats as floats etcetera withouth having to be overly verbose in your fluent mapping
		/// </summary>
		/// <returns></returns>
		public NestedObjectTypeDescriptor<TParent, TChild> MapFromAttributes(int maxRecursion = 0)
		{
			var writer = new TypeMappingWriter(typeof(TChild), this._TypeName, this._connectionSettings, maxRecursion);
			var mapping = writer.NestedObjectMappingFromAttributes();
			if (mapping == null)
				return this;
			if (this._Mapping.Properties == null)
				this._Mapping.Properties = new Dictionary<FieldName, IElasticType>();

			var properties = mapping.Properties;
			foreach (var p in properties)
			{
				var key = this.Infer.FieldName(p.Key);
				this._Mapping.Properties[key] = p.Value;
			}
			return this;
		}

		public NestedObjectTypeDescriptor<TParent, TChild> Dynamic(DynamicMappingOption dynamic)
		{
			this._Mapping.Dynamic = dynamic;
			return this;
		}
		public NestedObjectTypeDescriptor<TParent, TChild> Dynamic(bool dynamic = true)
		{
			return this.Dynamic(dynamic ? DynamicMappingOption.Allow : DynamicMappingOption.Ignore);
		}
		public NestedObjectTypeDescriptor<TParent, TChild> Enabled(bool enabled = true)
		{
			this._Mapping.Enabled = enabled;
			return this;
		}
		public NestedObjectTypeDescriptor<TParent, TChild> IncludeInParent(bool includeInParent = true)
		{
			this._Mapping.IncludeInParent = includeInParent;
			return this;
		}
		public NestedObjectTypeDescriptor<TParent, TChild> IncludeInRoot(bool includeInRoot = true)
		{
			this._Mapping.IncludeInRoot = includeInRoot;
			return this;
		}
		public NestedObjectTypeDescriptor<TParent, TChild> IncludeInAll(bool includeInAll = true)
		{
			this._Mapping.IncludeInAll = includeInAll;
			return this;
		}
		public NestedObjectTypeDescriptor<TParent, TChild> Path(string path)
		{
			this._Mapping.Path = path;
			return this;
		}

		public NestedObjectTypeDescriptor<TParent, TChild> Properties(Func<PropertiesDescriptor<TChild>, PropertiesDescriptor<TChild>> propertiesSelector)
		{
			propertiesSelector.ThrowIfNull("propertiesSelector");
			var properties = propertiesSelector(new PropertiesDescriptor<TChild>(this._connectionSettings));
			if (this._Mapping.Properties == null)
				this._Mapping.Properties = new Dictionary<FieldName, IElasticType>();

			foreach (var t in properties._Deletes)
			{
				_Mapping.Properties.Remove(t);
			}
			foreach (var p in properties.Properties)
			{
				_Mapping.Properties[p.Key] = p.Value;
			}
			return this;
		}

	}
}