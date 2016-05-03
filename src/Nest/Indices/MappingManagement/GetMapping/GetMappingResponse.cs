using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public interface IGetMappingResponse : IResponse
	{
		Dictionary<string, IList<TypeMapping>> Mappings { get; }

		Dictionary<IndexName, IDictionary<TypeName, TypeMapping>> IndexTypeMappings { get; }

		TypeMapping Mapping { get; }

		void Accept(IMappingVisitor visitor);
	}

	internal class GetRootObjectMappingWrapping : Dictionary<string, Dictionary<string, Dictionary<string, TypeMapping>>>
	{
	}

	public class GetMappingResponse : ResponseBase, IGetMappingResponse
	{
		internal GetMappingResponse() { }

		internal GetMappingResponse(GetRootObjectMappingWrapping dict)
		{
			foreach (var index in dict)
			{
				Dictionary<string, TypeMapping> mappings;
				if (index.Value != null && index.Value.TryGetValue("mappings", out mappings))
				{
					this.Mappings.Add(index.Key, new List<TypeMapping>());
					this.IndexTypeMappings.Add(index.Key, new Dictionary<TypeName, TypeMapping>());
					foreach (var mapping in mappings)
					{
						if (mapping.Value == null) continue;
						this.Mappings[index.Key].Add(mapping.Value);
						this.IndexTypeMappings[index.Key].Add(mapping.Key, mapping.Value);
					}
				}
			}

			this.Mapping = this.Mappings.Where(kv => kv.Value.HasAny(v => v != null))
				.SelectMany(kv => kv.Value)
				.FirstOrDefault(t => t != null);
		}

		public Dictionary<string, IList<TypeMapping>> Mappings { get; internal set; } = new Dictionary<string, IList<TypeMapping>>();

		public Dictionary<IndexName, IDictionary<TypeName, TypeMapping>> IndexTypeMappings { get; internal set; } = new Dictionary<IndexName, IDictionary<TypeName, TypeMapping>>();

		public TypeMapping Mapping { get; internal set; }

		public void Accept(IMappingVisitor visitor)
		{
			var walker = new MappingWalker(visitor);
			walker.Accept(this);
		}
	}
}
