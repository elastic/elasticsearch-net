using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
    [JsonConverter(typeof(TypesJsonConverter))]
    public class Types : Union<Types.AllTypes, Types.ManyTypes>, IUrlParameter
    {
        public class AllTypes { internal AllTypes() { } }
		public static AllTypes All { get; } = new AllTypes();
		public class ManyTypes
		{
			private readonly List<TypeName> _types = new List<TypeName>();
			public IReadOnlyList<TypeName> Types => _types;
			internal ManyTypes(IEnumerable<TypeName> types) { this._types.AddRange(types); }

			public ManyTypes And<T>()
			{
				this._types.Add(typeof(T));
				return this;
			}
		}

		internal Types(Types.AllTypes all) : base(all) { }
		internal Types(Types.ManyTypes types) : base(types) { }

		public static Types Single(TypeName type) => new ManyTypes(new[] { type });
		public static Types Single<T>() => new ManyTypes(new TypeName[] { typeof(T) });
		public static Types Many(IEnumerable<TypeName> types) => new ManyTypes(types);
		public static Types Many(params TypeName[] types) => new ManyTypes(types);
		public static ManyTypes Type<T>() => new ManyTypes(new TypeName[] { typeof(T) });

		public static Types Parse(string typesString)
		{
            if (typesString.IsNullOrEmpty()) return Types.All;
			var types = typesString.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
			return Many(types.Select(i => (TypeName)i));
		}

		public static implicit operator Types(string typesString) => Parse(typesString);
		public static implicit operator Types(AllTypes all) => new Types(all);
		public static implicit operator Types(ManyTypes many) => new Types(many);
		public static implicit operator Types(TypeName type) => Types.Single(type);
		public static implicit operator Types(Type type) => Types.Single(type);

		string IUrlParameter.GetString(IConnectionConfigurationValues settings)
		{
			return this.Match(
				all => null,
				many =>
				{
					var nestSettings = settings as IConnectionSettingsValues;
					if (nestSettings == null)
						throw new Exception("Tried to pass field name on querysting but it could not be resolved because no nest settings are available");
					var infer = new ElasticInferrer(nestSettings);
					var types = this.Item2.Types.Select(t => infer.TypeName(t));
					return string.Join(",", types);
				}
			);

		}
    }
}
