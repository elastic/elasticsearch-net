/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Collections.Generic;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Mapping.Types.Core.Number
{
	public class NumberPropertyTests : PropertyTestsBase
	{
		public NumberPropertyTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			properties = new
			{
				numberOfCommits = new
				{
					type = "integer",
					doc_values = true,
					store = true,
					index = false,
					null_value = 0.0,
					ignore_malformed = true,
					coerce = true
				}
			}
		};

		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
			.Number(n => n
				.Name(p => p.NumberOfCommits)
				.Type(NumberType.Integer)
				.DocValues()
				.Store()
				.Index(false)
				.NullValue(0.0)
				.IgnoreMalformed()
				.Coerce()
			);


		protected override IProperties InitializerProperties => new Properties
		{
			{
				"numberOfCommits", new NumberProperty(NumberType.Integer)
				{
					DocValues = true,
					Store = true,
					Index = false,
					NullValue = 0.0,
					IgnoreMalformed = true,
					Coerce = true
				}
			}
		};
	}

	public class ScaledFloatNumberPropertyTests : PropertyTestsBase
	{
		public ScaledFloatNumberPropertyTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			properties = new
			{
				numberOfCommits = new
				{
					type = "scaled_float",
					scaling_factor = 10.0,
					doc_values = true,
					store = true,
					index = false,
					null_value = 0.0,
					ignore_malformed = true,
					coerce = true
				}
			}
		};

		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
			.Number(n => n
				.Name(p => p.NumberOfCommits)
				.Type(NumberType.ScaledFloat)
				.ScalingFactor(10)
				.DocValues()
				.Store()
				.Index(false)
				.NullValue(0.0)
				.IgnoreMalformed()
				.Coerce()
			);


		protected override IProperties InitializerProperties => new Properties
		{
			{
				"numberOfCommits", new NumberProperty(NumberType.ScaledFloat)
				{
					ScalingFactor = 10,
					DocValues = true,
					Store = true,
					Index = false,
					NullValue = 0.0,
					IgnoreMalformed = true,
					Coerce = true
				}
			}
		};
	}

	[SkipVersion("<7.10.0", "Introduced in 7.10.0")]
	public class UnsignedLongNumberPropertyTests : PropertyTestsBase
	{
		public UnsignedLongNumberPropertyTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			properties = new
			{
				numberOfCommits = new
				{
					type = "unsigned_long",
					doc_values = true,
					store = true,
					index = false,
					ignore_malformed = true
				}
			}
		};

		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
			.Number(n => n
				.Name(p => p.NumberOfCommits)
				.Type(NumberType.UnsignedLong)
				.DocValues()
				.Store()
				.Index(false)
				.IgnoreMalformed()
			);
		
		protected override IProperties InitializerProperties => new Properties
		{
			{
				"numberOfCommits", new NumberProperty(NumberType.UnsignedLong)
				{
					DocValues = true,
					Store = true,
					Index = false,
					IgnoreMalformed = true
				}
			}
		};
	}

	[SkipVersion("<7.13.0", "Script support added in 7.13.0")]
	public class ScriptedNumberPropertyTests : PropertyTestsBase
	{
		public ScriptedNumberPropertyTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			properties = new
			{
				doublesCommits = new
				{
					type = "long",
					script = new
					{
						source = "emit((long)(doc['numberOfCommits'].value * params.multiplier))",
						@params = new Dictionary<string, int>
						{
							{"multiplier", 2 }
						}
					},
					on_script_error = "continue"
				}
			}
		};

		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
			.Number(n => n
				.Name("doublesCommits")
				.Type(NumberType.Long)
				.Script(s => s.Source("emit((long)(doc['numberOfCommits'].value * params.multiplier))").Params(p => p.Add("multiplier", 2)))
				.OnScriptError(OnScriptError.Continue)
			);

		protected override IProperties InitializerProperties => new Properties
		{
			{
				"doublesCommits", new NumberProperty(NumberType.Long)
				{
					Script = new InlineScript("emit((long)(doc['numberOfCommits'].value * params.multiplier))")
					{
						Params = new Dictionary<string, object>
						{
							{ "multiplier", 2 }
						}
					},
					OnScriptError = OnScriptError.Continue
				}
			}
		};
	}
}
