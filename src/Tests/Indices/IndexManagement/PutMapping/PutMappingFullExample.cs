using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Tests.Framework.MockData;
using Tests.Framework.Integration;

namespace Tests.Indices.IndexManagement
{
	public class PutMappingFullExample : PutMapping
	{
		public PutMappingFullExample(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			properties = new
			{
				name = new
				{
					type = "string"
				},
				state = new
				{
					type = "integer"
				},
				startedOn = new
				{
					type = "date"
				},
				lastActivity = new
				{
					type = "date"
				},
				leadDeveloper = new
				{
					type = "object",
					properties = new
					{
						onlineHandle = new
						{
							type = "string"
						},
						gender = new
						{
							type = "integer"
						},
						privateValue = new
						{
							type = "string"
						},
						id = new
						{
							type = "integer"
						},
						firstName = new
						{
							type = "string"
						},
						lastName = new
						{
							type = "string"
						},
						jobTitle = new
						{
							type = "string"
						},
						location = new
						{
							type = "geo_point"
						}
					}
				},
				tags = new
				{
					type = "object",
					properties = new
					{
						added = new
						{
							type = "date"
						},
						name = new
						{
							type = "string"
						}
					}
				},
				curatedTags = new
				{
					type = "object",
					properties = new
					{
						added = new
						{
							type = "date"
						},
						name = new
						{
							type = "string"
						}
					}
				},
				metadata = new
				{
					type = "object"
				}
			}
		};


		protected override Func<PutMappingDescriptor<Project>, IPutMappingRequest> Fluent => m => m
			.AutoMap()
			.Properties(ps => ps
				.Object<Dictionary<string, Metadata>>(o => o
					.Name(p => p.Metadata)
				)
				.Object<Developer>(o => o
					.AutoMap()
					.Name(p => p.LeadDeveloper)
					.Properties(pps => pps
						.GeoPoint(g => g
							.Name(p => p.Location)
						)
					)
				)
			);

		protected override PutMappingRequest<Project> Initializer => new PutMappingRequest<Project>();
	}
}
