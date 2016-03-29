using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;

namespace Nest
{
	public interface IGetMappingResponse : IResponse
	{
		/// <summary>
		/// TODO dict|indexname, imappings|
		/// </summary>
		Dictionary<string, IList<TypeMapping>> Mappings { get; }
		TypeMapping Mapping { get; }
		void Accept(IMappingVisitor visitor);
	}

	internal class GetRootObjectMappingWrapping : Dictionary<string, Dictionary<string, Dictionary<string, TypeMapping>>>
	{
	}

	public class GetMappingResponse : ResponseBase, IGetMappingResponse
	{
		public override bool IsValid => base.IsValid && this.Mapping != null;

		internal GetMappingResponse() { }

		internal GetMappingResponse(IApiCallDetails status, GetRootObjectMappingWrapping dict)
		{
			this.Mappings = new Dictionary<string, IList<TypeMapping>>();
			//TODO can dict truely ever be null, whats the response look like when field mapping is not found.
			//does status.Success not already reflect this?
			//this.IsValid = status.Success && dict != null && dict.Count > 0;
			if (!status.Success || dict == null) return;

			foreach (var index in dict)
			{
				if (index.Value == null || !index.Value.ContainsKey("mappings"))
					continue;

				var mappings = index.Value["mappings"];
				this.Mappings.Add(index.Key, new List<TypeMapping>());
				if (mappings == null) continue;
				foreach (var mapping in mappings)
				{
					if (mapping.Value == null) continue;
					this.Mappings[index.Key].Add(mapping.Value);
				}
			}
			
			this.Mapping = this.Mappings.Where(kv=>kv.Value.HasAny(v=>v != null))
				.SelectMany(kv=>kv.Value)
				.FirstOrDefault(t=>t != null);
		}

		public Dictionary<string, IList<TypeMapping>> Mappings { get; internal set; } = new Dictionary<string, IList<TypeMapping>>();

		public TypeMapping Mapping { get; internal set; }

		public void Accept(IMappingVisitor visitor)
		{
			var walker = new MappingWalker(visitor);
			walker.Accept(this);
		}
	}
}