using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Framework;
using Nest;
using Tests.Framework.MockData;
using Tests.Framework.Integration;

namespace Tests.Mapping.Types
{
	public class String 
	{
		public class Usage : MapUsageBase
		{
			public Usage(ReadOnlyCluster cluster, ApiUsage usage) : base(cluster, usage) { }

			protected override object ExpectJson => new
			{
				mappings = new
				{
					project = new
					{
						properties = new
						{
							name = new
							{
								type = "string",
								index = "not_analyzed"
							}
						}
					}
				}
			};


			protected override Func<PutMappingDescriptor<Project>, IPutMappingRequest> Fluent => m => m
				.Properties(ps => ps
					.String(s => s
						.Name(p => p.Name)
						.Index(FieldIndexOption.NotAnalyzed)
					)
				);

			protected override PutMappingRequest<Project> Initializer => 
				new PutMappingRequest<Project>
				{
					Mapping = new RootObjectType
					{
						Properties = new Dictionary<FieldName, IElasticType>
						{
							{ "name", new StringType { Index = FieldIndexOption.NotAnalyzed } }
						}
					}
				};
		}
	}
}
