using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Linq.Expressions;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class MultiFieldMapping : IElasticType
	{
		private readonly TypeName _defaultType;

		public MultiFieldMapping() 
		{
			this.Fields = new Dictionary<FieldName, IElasticCoreType>();
			_defaultType = "multi_field";
		}

		protected MultiFieldMapping(TypeName defaultType) : this()
		{
			_defaultType = defaultType;
		}

		public FieldName Name { get; set; }

		private TypeName _typeOverride;

		[JsonProperty("type")]
		public virtual TypeName Type
		{
			get { return _typeOverride ?? _defaultType; }
			set { _typeOverride = value; }
		}

        [JsonProperty("path")]
        public string Path { get; set; }

		[JsonProperty("similarity")]
		public string Similarity { get; set; }

		[JsonProperty("include_in_all")]
		public bool? IncludeInAll { get; set; }

		[JsonProperty("fields", DefaultValueHandling = DefaultValueHandling.Ignore), JsonConverter(typeof(ElasticCoreTypeJsonConverter))]
		public IDictionary<FieldName, IElasticCoreType> Fields { get; set; }
	}

    public class MultiFieldMappingPath
    {
        public MultiFieldMappingPath()
        {

        }

        public MultiFieldMappingPath(string customValue)
        {
            Value = customValue;
        }

        public string Value { get; private set; }

        public static MultiFieldMappingPath Full
        {
            get
            {
                return new MultiFieldMappingPath("full");
            }
        }

        public static MultiFieldMappingPath JustName
        {
            get
            {
                return new MultiFieldMappingPath("just_name");
            }
        }
    }

	public class MultiFieldMappingDescriptor<T> where T : class
	{
		internal MultiFieldMapping _Mapping = new MultiFieldMapping();

		public MultiFieldMappingDescriptor<T> Name(string name)
		{
			this._Mapping.Name = name;
			return this;
		}

        public MultiFieldMappingDescriptor<T> Path(MultiFieldMappingPath path)
        {
            this._Mapping.Path = path.Value;
            return this;
        }

		public MultiFieldMappingDescriptor<T> Name(Expression<Func<T, object>> objectPath)
		{
			this._Mapping.Name = objectPath;
			return this;
		}

		public MultiFieldMappingDescriptor<T> Fields(Func<CorePropertiesDescriptor<T>, CorePropertiesDescriptor<T>> fieldSelector)
		{
			fieldSelector.ThrowIfNull("fieldSelector");
			var properties = fieldSelector(new CorePropertiesDescriptor<T>());
			foreach (var p in properties.Properties)
			{
				var value = p.Value as IElasticCoreType;
				if (value == null)
					continue;
				
				_Mapping.Fields[p.Key] = value;
			}
			return this;
		}
		
	}
}