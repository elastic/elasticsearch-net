// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.
//
// ███╗   ██╗ ██████╗ ████████╗██╗ ██████╗███████╗
// ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝██╔════╝
// ██╔██╗ ██║██║   ██║   ██║   ██║██║     █████╗
// ██║╚██╗██║██║   ██║   ██║   ██║██║     ██╔══╝
// ██║ ╚████║╚██████╔╝   ██║   ██║╚██████╗███████╗
// ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝ ╚═════╝╚══════╝
// ------------------------------------------------
//
// This file is automatically generated.
// Please do not edit these files manually.
//
// ------------------------------------------------

#nullable restore

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Requests;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;
using Elastic.Transport.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.IndexLifecycleManagement;

public sealed partial class MoveToStepRequestParameters : RequestParameters
{
}

/// <summary>
/// <para>
/// Move to a lifecycle step.
/// Manually move an index into a specific step in the lifecycle policy and run that step.
/// </para>
/// <para>
/// WARNING: This operation can result in the loss of data. Manually moving an index into a specific step runs that step even if it has already been performed. This is a potentially destructive action and this should be considered an expert level API.
/// </para>
/// <para>
/// You must specify both the current step and the step to be executed in the body of the request.
/// The request will fail if the current step does not match the step currently running for the index
/// This is to prevent the index from being moved from an unexpected step into the next step.
/// </para>
/// <para>
/// When specifying the target (<c>next_step</c>) to which the index will be moved, either the name or both the action and name fields are optional.
/// If only the phase is specified, the index will move to the first step of the first action in the target phase.
/// If the phase and action are specified, the index will move to the first step of the specified action in the specified phase.
/// Only actions specified in the ILM policy are considered valid.
/// An index cannot move to a step that is not part of its policy.
/// </para>
/// </summary>
public sealed partial class MoveToStepRequest : PlainRequest<MoveToStepRequestParameters>
{
	public MoveToStepRequest(Elastic.Clients.Elasticsearch.IndexName index) : base(r => r.Required("index", index))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexLifecycleManagementMoveToStep;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "ilm.move_to_step";

	/// <summary>
	/// <para>
	/// The step that the index is expected to be in.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("current_step")]
	public Elastic.Clients.Elasticsearch.IndexLifecycleManagement.StepKey CurrentStep { get; set; }

	/// <summary>
	/// <para>
	/// The step that you want to run.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("next_step")]
	public Elastic.Clients.Elasticsearch.IndexLifecycleManagement.StepKey NextStep { get; set; }
}

/// <summary>
/// <para>
/// Move to a lifecycle step.
/// Manually move an index into a specific step in the lifecycle policy and run that step.
/// </para>
/// <para>
/// WARNING: This operation can result in the loss of data. Manually moving an index into a specific step runs that step even if it has already been performed. This is a potentially destructive action and this should be considered an expert level API.
/// </para>
/// <para>
/// You must specify both the current step and the step to be executed in the body of the request.
/// The request will fail if the current step does not match the step currently running for the index
/// This is to prevent the index from being moved from an unexpected step into the next step.
/// </para>
/// <para>
/// When specifying the target (<c>next_step</c>) to which the index will be moved, either the name or both the action and name fields are optional.
/// If only the phase is specified, the index will move to the first step of the first action in the target phase.
/// If the phase and action are specified, the index will move to the first step of the specified action in the specified phase.
/// Only actions specified in the ILM policy are considered valid.
/// An index cannot move to a step that is not part of its policy.
/// </para>
/// </summary>
public sealed partial class MoveToStepRequestDescriptor<TDocument> : RequestDescriptor<MoveToStepRequestDescriptor<TDocument>, MoveToStepRequestParameters>
{
	internal MoveToStepRequestDescriptor(Action<MoveToStepRequestDescriptor<TDocument>> configure) => configure.Invoke(this);

	public MoveToStepRequestDescriptor(Elastic.Clients.Elasticsearch.IndexName index) : base(r => r.Required("index", index))
	{
	}

	public MoveToStepRequestDescriptor() : this(typeof(TDocument))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexLifecycleManagementMoveToStep;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "ilm.move_to_step";

	public MoveToStepRequestDescriptor<TDocument> Index(Elastic.Clients.Elasticsearch.IndexName index)
	{
		RouteValues.Required("index", index);
		return Self;
	}

	private Elastic.Clients.Elasticsearch.IndexLifecycleManagement.StepKey CurrentStepValue { get; set; }
	private Elastic.Clients.Elasticsearch.IndexLifecycleManagement.StepKeyDescriptor CurrentStepDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.IndexLifecycleManagement.StepKeyDescriptor> CurrentStepDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.IndexLifecycleManagement.StepKey NextStepValue { get; set; }
	private Elastic.Clients.Elasticsearch.IndexLifecycleManagement.StepKeyDescriptor NextStepDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.IndexLifecycleManagement.StepKeyDescriptor> NextStepDescriptorAction { get; set; }

	/// <summary>
	/// <para>
	/// The step that the index is expected to be in.
	/// </para>
	/// </summary>
	public MoveToStepRequestDescriptor<TDocument> CurrentStep(Elastic.Clients.Elasticsearch.IndexLifecycleManagement.StepKey currentStep)
	{
		CurrentStepDescriptor = null;
		CurrentStepDescriptorAction = null;
		CurrentStepValue = currentStep;
		return Self;
	}

	public MoveToStepRequestDescriptor<TDocument> CurrentStep(Elastic.Clients.Elasticsearch.IndexLifecycleManagement.StepKeyDescriptor descriptor)
	{
		CurrentStepValue = null;
		CurrentStepDescriptorAction = null;
		CurrentStepDescriptor = descriptor;
		return Self;
	}

	public MoveToStepRequestDescriptor<TDocument> CurrentStep(Action<Elastic.Clients.Elasticsearch.IndexLifecycleManagement.StepKeyDescriptor> configure)
	{
		CurrentStepValue = null;
		CurrentStepDescriptor = null;
		CurrentStepDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The step that you want to run.
	/// </para>
	/// </summary>
	public MoveToStepRequestDescriptor<TDocument> NextStep(Elastic.Clients.Elasticsearch.IndexLifecycleManagement.StepKey nextStep)
	{
		NextStepDescriptor = null;
		NextStepDescriptorAction = null;
		NextStepValue = nextStep;
		return Self;
	}

	public MoveToStepRequestDescriptor<TDocument> NextStep(Elastic.Clients.Elasticsearch.IndexLifecycleManagement.StepKeyDescriptor descriptor)
	{
		NextStepValue = null;
		NextStepDescriptorAction = null;
		NextStepDescriptor = descriptor;
		return Self;
	}

	public MoveToStepRequestDescriptor<TDocument> NextStep(Action<Elastic.Clients.Elasticsearch.IndexLifecycleManagement.StepKeyDescriptor> configure)
	{
		NextStepValue = null;
		NextStepDescriptor = null;
		NextStepDescriptorAction = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (CurrentStepDescriptor is not null)
		{
			writer.WritePropertyName("current_step");
			JsonSerializer.Serialize(writer, CurrentStepDescriptor, options);
		}
		else if (CurrentStepDescriptorAction is not null)
		{
			writer.WritePropertyName("current_step");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.IndexLifecycleManagement.StepKeyDescriptor(CurrentStepDescriptorAction), options);
		}
		else
		{
			writer.WritePropertyName("current_step");
			JsonSerializer.Serialize(writer, CurrentStepValue, options);
		}

		if (NextStepDescriptor is not null)
		{
			writer.WritePropertyName("next_step");
			JsonSerializer.Serialize(writer, NextStepDescriptor, options);
		}
		else if (NextStepDescriptorAction is not null)
		{
			writer.WritePropertyName("next_step");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.IndexLifecycleManagement.StepKeyDescriptor(NextStepDescriptorAction), options);
		}
		else
		{
			writer.WritePropertyName("next_step");
			JsonSerializer.Serialize(writer, NextStepValue, options);
		}

		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Move to a lifecycle step.
/// Manually move an index into a specific step in the lifecycle policy and run that step.
/// </para>
/// <para>
/// WARNING: This operation can result in the loss of data. Manually moving an index into a specific step runs that step even if it has already been performed. This is a potentially destructive action and this should be considered an expert level API.
/// </para>
/// <para>
/// You must specify both the current step and the step to be executed in the body of the request.
/// The request will fail if the current step does not match the step currently running for the index
/// This is to prevent the index from being moved from an unexpected step into the next step.
/// </para>
/// <para>
/// When specifying the target (<c>next_step</c>) to which the index will be moved, either the name or both the action and name fields are optional.
/// If only the phase is specified, the index will move to the first step of the first action in the target phase.
/// If the phase and action are specified, the index will move to the first step of the specified action in the specified phase.
/// Only actions specified in the ILM policy are considered valid.
/// An index cannot move to a step that is not part of its policy.
/// </para>
/// </summary>
public sealed partial class MoveToStepRequestDescriptor : RequestDescriptor<MoveToStepRequestDescriptor, MoveToStepRequestParameters>
{
	internal MoveToStepRequestDescriptor(Action<MoveToStepRequestDescriptor> configure) => configure.Invoke(this);

	public MoveToStepRequestDescriptor(Elastic.Clients.Elasticsearch.IndexName index) : base(r => r.Required("index", index))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexLifecycleManagementMoveToStep;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "ilm.move_to_step";

	public MoveToStepRequestDescriptor Index(Elastic.Clients.Elasticsearch.IndexName index)
	{
		RouteValues.Required("index", index);
		return Self;
	}

	private Elastic.Clients.Elasticsearch.IndexLifecycleManagement.StepKey CurrentStepValue { get; set; }
	private Elastic.Clients.Elasticsearch.IndexLifecycleManagement.StepKeyDescriptor CurrentStepDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.IndexLifecycleManagement.StepKeyDescriptor> CurrentStepDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.IndexLifecycleManagement.StepKey NextStepValue { get; set; }
	private Elastic.Clients.Elasticsearch.IndexLifecycleManagement.StepKeyDescriptor NextStepDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.IndexLifecycleManagement.StepKeyDescriptor> NextStepDescriptorAction { get; set; }

	/// <summary>
	/// <para>
	/// The step that the index is expected to be in.
	/// </para>
	/// </summary>
	public MoveToStepRequestDescriptor CurrentStep(Elastic.Clients.Elasticsearch.IndexLifecycleManagement.StepKey currentStep)
	{
		CurrentStepDescriptor = null;
		CurrentStepDescriptorAction = null;
		CurrentStepValue = currentStep;
		return Self;
	}

	public MoveToStepRequestDescriptor CurrentStep(Elastic.Clients.Elasticsearch.IndexLifecycleManagement.StepKeyDescriptor descriptor)
	{
		CurrentStepValue = null;
		CurrentStepDescriptorAction = null;
		CurrentStepDescriptor = descriptor;
		return Self;
	}

	public MoveToStepRequestDescriptor CurrentStep(Action<Elastic.Clients.Elasticsearch.IndexLifecycleManagement.StepKeyDescriptor> configure)
	{
		CurrentStepValue = null;
		CurrentStepDescriptor = null;
		CurrentStepDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The step that you want to run.
	/// </para>
	/// </summary>
	public MoveToStepRequestDescriptor NextStep(Elastic.Clients.Elasticsearch.IndexLifecycleManagement.StepKey nextStep)
	{
		NextStepDescriptor = null;
		NextStepDescriptorAction = null;
		NextStepValue = nextStep;
		return Self;
	}

	public MoveToStepRequestDescriptor NextStep(Elastic.Clients.Elasticsearch.IndexLifecycleManagement.StepKeyDescriptor descriptor)
	{
		NextStepValue = null;
		NextStepDescriptorAction = null;
		NextStepDescriptor = descriptor;
		return Self;
	}

	public MoveToStepRequestDescriptor NextStep(Action<Elastic.Clients.Elasticsearch.IndexLifecycleManagement.StepKeyDescriptor> configure)
	{
		NextStepValue = null;
		NextStepDescriptor = null;
		NextStepDescriptorAction = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (CurrentStepDescriptor is not null)
		{
			writer.WritePropertyName("current_step");
			JsonSerializer.Serialize(writer, CurrentStepDescriptor, options);
		}
		else if (CurrentStepDescriptorAction is not null)
		{
			writer.WritePropertyName("current_step");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.IndexLifecycleManagement.StepKeyDescriptor(CurrentStepDescriptorAction), options);
		}
		else
		{
			writer.WritePropertyName("current_step");
			JsonSerializer.Serialize(writer, CurrentStepValue, options);
		}

		if (NextStepDescriptor is not null)
		{
			writer.WritePropertyName("next_step");
			JsonSerializer.Serialize(writer, NextStepDescriptor, options);
		}
		else if (NextStepDescriptorAction is not null)
		{
			writer.WritePropertyName("next_step");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.IndexLifecycleManagement.StepKeyDescriptor(NextStepDescriptorAction), options);
		}
		else
		{
			writer.WritePropertyName("next_step");
			JsonSerializer.Serialize(writer, NextStepValue, options);
		}

		writer.WriteEndObject();
	}
}