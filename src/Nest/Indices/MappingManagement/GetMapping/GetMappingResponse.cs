using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public interface IGetMappingResponse : IResponse
	{
		IReadOnlyDictionary<string, IReadOnlyDictionary<string, TypeMapping>> Mappings { get; }

		TypeMapping Mapping { get; }

		void Accept(IMappingVisitor visitor);
	}

	internal class GetRootObjectMappingWrapping : Dictionary<string, Dictionary<string, Dictionary<string, TypeMapping>>>
	{
	}

	public class GetMappingResponse : ResponseBase, IGetMappingResponse
	{
		public GetMappingResponse() { }

		internal GetMappingResponse(GetRootObjectMappingWrapping dict)
		{
			if (dict == null) return;
			foreach (var index in dict)
			{
				if (index.Value == null || !index.Value.TryGetValue("mappings", out var mappings)) continue;
				this._mappings.Add(index.Key, new Dictionary<string, TypeMapping>());
				foreach (var mapping in mappings)
				{
					if (mapping.Value == null) continue;
					this._mappings[index.Key].Add(mapping.Key, mapping.Value);
				}
			}

			this.Mapping = this.Mappings.Where(kv => kv.Value.HasAny(v => v.Value != null))
				.SelectMany(kv => kv.Value)
				.FirstOrDefault(t => t.Value != null).Value;
		}

		private Dictionary<string, Dictionary<string, TypeMapping>> _mappings = new Dictionary<string, Dictionary<string, TypeMapping>>();
		public IReadOnlyDictionary<string, IReadOnlyDictionary<string, TypeMapping>> Mappings => this._mappings
			.ToDictionary(k=>k.Key, v=>(IReadOnlyDictionary<string, TypeMapping>)v.Value);

		public TypeMapping Mapping { get; }

		public void Accept(IMappingVisitor visitor)
		{
			var walker = new MappingWalker(visitor);
			walker.Accept(this);
		}
	}
}
