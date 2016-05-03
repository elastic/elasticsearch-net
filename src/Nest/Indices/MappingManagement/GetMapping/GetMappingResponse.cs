using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public interface IGetMappingResponse : IResponse
	{
		Dictionary<string, IDictionary<string, TypeMapping>> Mappings { get; }

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
					this.Mappings.Add(index.Key, new Dictionary<string, TypeMapping>());
					foreach (var mapping in mappings)
					{
						if (mapping.Value == null) continue;
						this.Mappings[index.Key].Add(mapping.Key, mapping.Value);
					}
				}
			}

			this.Mapping = this.Mappings.Where(kv => kv.Value.HasAny(v => v.Value != null))
				.SelectMany(kv => kv.Value)
				.FirstOrDefault(t => t.Value != null).Value;
		}

		public Dictionary<string, IDictionary<string, TypeMapping>> Mappings { get; internal set; } = new Dictionary<string, IDictionary<string, TypeMapping>>();

		public TypeMapping Mapping { get; internal set; }

		public void Accept(IMappingVisitor visitor)
		{
			var walker = new MappingWalker(visitor);
			walker.Accept(this);
		}
	}
}
