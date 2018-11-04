using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public interface IGetMappingResponse : IResponse
	{
		TypeMapping Mapping { get; }
		IReadOnlyDictionary<string, IReadOnlyDictionary<string, TypeMapping>> Mappings { get; }

		void Accept(IMappingVisitor visitor);
	}

	internal class GetRootObjectMappingWrapping : Dictionary<string, Dictionary<string, Dictionary<string, TypeMapping>>> { }

	public class GetMappingResponse : ResponseBase, IGetMappingResponse
	{
		private readonly Dictionary<string, Dictionary<string, TypeMapping>> _mappings = new Dictionary<string, Dictionary<string, TypeMapping>>();

		internal GetMappingResponse() { }

		internal GetMappingResponse(GetRootObjectMappingWrapping dict)
		{
			foreach (var index in dict)
			{
				Dictionary<string, TypeMapping> mappings;
				if (index.Value != null && index.Value.TryGetValue("mappings", out mappings))
				{
					_mappings.Add(index.Key, new Dictionary<string, TypeMapping>());
					foreach (var mapping in mappings)
					{
						if (mapping.Value == null) continue;

						_mappings[index.Key].Add(mapping.Key, mapping.Value);
					}
				}
			}

			Mapping = Mappings.Where(kv => kv.Value.HasAny(v => v.Value != null))
				.SelectMany(kv => kv.Value)
				.FirstOrDefault(t => t.Value != null)
				.Value;
		}

		public TypeMapping Mapping { get; internal set; }

		public IReadOnlyDictionary<string, IReadOnlyDictionary<string, TypeMapping>> Mappings => _mappings
			.ToDictionary(k => k.Key, v => (IReadOnlyDictionary<string, TypeMapping>)v.Value);

		public void Accept(IMappingVisitor visitor)
		{
			var walker = new MappingWalker(visitor);
			walker.Accept(this);
		}
	}
}
