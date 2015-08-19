using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Nest.DSL.Visitor;

namespace Nest
{
	public interface IGetMappingResponse : IResponse
	{
		Dictionary<string, IList<TypeMapping>> Mappings { get; }
		RootObjectProperty Mapping { get; }
		void Accept(IMappingVisitor visitor);
	}

	public class TypeMapping
	{
		public string TypeName { get; internal set; }
		public RootObjectProperty Mapping  { get; internal set; }
	}

	internal class GetRootObjectMappingWrapping : Dictionary<string, Dictionary<string, Dictionary<string, RootObjectProperty>>>
	{
		
	}

	public class GetMappingResponse : BaseResponse, IGetMappingResponse
	{
		internal GetMappingResponse(IApiCallDetails status, GetRootObjectMappingWrapping dict)
		{
			this.Mappings = new Dictionary<string, IList<TypeMapping>>();
			//TODO can dict truely ever be null, whats the response look like when field mapping is not found.
			//does status.Success not already reflect this?
			this.IsValid = status.Success && dict != null && dict.Count > 0;
			if (!this.IsValid) return;
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
					var typeMapping = new TypeMapping
					{
						TypeName = mapping.Key,
						Mapping = mapping.Value
					};
					mapping.Value.Name = mapping.Key;
					this.Mappings[index.Key].Add(typeMapping);
				}
			}
			
			this.Mapping = this.Mappings.Where(kv=>kv.Value.HasAny(v=>v.Mapping != null))
				.SelectMany(kv=>kv.Value)
				.Select(tm=>tm.Mapping)
				.FirstOrDefault(t=>t != null);
		}

		public Dictionary<string, IList<TypeMapping>> Mappings { get; internal set; }= new Dictionary<string, IList<TypeMapping>>();

		public RootObjectProperty Mapping { get; internal set; }

		public void Accept(IMappingVisitor visitor)
		{
			var walker = new MappingWalker(visitor);
			walker.Accept(this);
		}
	}
}