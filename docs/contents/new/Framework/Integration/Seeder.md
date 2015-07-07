---
template: layout.jade
title: x
menusection: concepts
menuitem: breaking-changes
---
```
this.CreateIndicesAndMappings();
var putTemplateResult = this.Client.PutTemplate("raw_fields", p => p
				.Template("*") //match on all created indices
				.Settings(s=>s
					.Add("index.number_of_shards", "2")
					.Add("index.number_of_replicas", "0")
				)
				.AddMapping<object>(pm => pm
					.Type("_default_")
					.DynamicTemplates(dt => dt
						.Add(dtt => dtt
							.Name("raw_fields") //register a raw fields dynamic template
							.Match("*") //matches all fields
							.MatchMappingType("string") //that are a string
							.Mapping(m=>m
								.String(sm=>sm //map as string
									.Fields(f=>f //with a multifield 'raw' that is not analyzed
										.String(ssm=>ssm.Name("raw").Index(FieldIndexOption.NotAnalyzed))
									)
								)
							)
						)
					)
				)
			);
putTemplateResult.IsValid.Should().BeTrue();
var createProjectIndex = this.Client.CreateIndex(c => c
				.Index<Project>()
				.AddMapping<Project>(m=>m
					.Properties(props=>props
						.Date(d=>d.Name(p=>p.StartedOn))
						.NestedObject<Tag>(mo=>mo
							.Name(p=>p.Tags)
							.Properties(TagProperties)
						)
						.Object<Developer>(o=>o
							.Name(p=>p.LeadDeveloper)
							.Properties(DeveloperProperties)
						)
					)
				)
				.AddMapping<CommitActivity>(m=>m
					.SetParent<Project>()
					.Properties(props=>props
						.Object<Developer>(o=>o
							.Name(p=>p.Committer)
							.Properties(DeveloperProperties)
						)
						.String(prop=>prop.Name(p=>p.ProjectName).NotAnalyzed())
					)
				)

			);
createProjectIndex.IsValid.Should().BeTrue();
this.Client.IndexMany(Project.Projects);
this.Client.Refresh(r=>r.Index<Project>());
```
