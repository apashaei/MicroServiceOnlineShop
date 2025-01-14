// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: protobuf.proto
// </auto-generated>
#pragma warning disable 0414, 1591, 8981
#region Designer generated code

using grpc = global::Grpc.Core;

namespace DiscountServices.Protos {
  public static partial class DiscountServicesProto
  {
    static readonly string __ServiceName = "DiscountServicesProto";

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::DiscountServices.Protos.RequestGetDiscountBycode> __Marshaller_RequestGetDiscountBycode = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::DiscountServices.Protos.RequestGetDiscountBycode.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::DiscountServices.Protos.ResponseGetDiscountBycode> __Marshaller_ResponseGetDiscountBycode = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::DiscountServices.Protos.ResponseGetDiscountBycode.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::DiscountServices.Protos.RequestGetDiscountById> __Marshaller_RequestGetDiscountById = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::DiscountServices.Protos.RequestGetDiscountById.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::DiscountServices.Protos.RequestUseDiscount> __Marshaller_RequestUseDiscount = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::DiscountServices.Protos.RequestUseDiscount.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::DiscountServices.Protos.ResponseUseDiscount> __Marshaller_ResponseUseDiscount = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::DiscountServices.Protos.ResponseUseDiscount.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::DiscountServices.Protos.RequestAddNewDiscount> __Marshaller_RequestAddNewDiscount = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::DiscountServices.Protos.RequestAddNewDiscount.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::DiscountServices.Protos.ResponseAddNewDiscount> __Marshaller_ResponseAddNewDiscount = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::DiscountServices.Protos.ResponseAddNewDiscount.Parser));

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::DiscountServices.Protos.RequestGetDiscountBycode, global::DiscountServices.Protos.ResponseGetDiscountBycode> __Method_GetDiscountByCode = new grpc::Method<global::DiscountServices.Protos.RequestGetDiscountBycode, global::DiscountServices.Protos.ResponseGetDiscountBycode>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetDiscountByCode",
        __Marshaller_RequestGetDiscountBycode,
        __Marshaller_ResponseGetDiscountBycode);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::DiscountServices.Protos.RequestGetDiscountById, global::DiscountServices.Protos.ResponseGetDiscountBycode> __Method_GetDiscountById = new grpc::Method<global::DiscountServices.Protos.RequestGetDiscountById, global::DiscountServices.Protos.ResponseGetDiscountBycode>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetDiscountById",
        __Marshaller_RequestGetDiscountById,
        __Marshaller_ResponseGetDiscountBycode);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::DiscountServices.Protos.RequestUseDiscount, global::DiscountServices.Protos.ResponseUseDiscount> __Method_UseDiscount = new grpc::Method<global::DiscountServices.Protos.RequestUseDiscount, global::DiscountServices.Protos.ResponseUseDiscount>(
        grpc::MethodType.Unary,
        __ServiceName,
        "UseDiscount",
        __Marshaller_RequestUseDiscount,
        __Marshaller_ResponseUseDiscount);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::DiscountServices.Protos.RequestAddNewDiscount, global::DiscountServices.Protos.ResponseAddNewDiscount> __Method_AddNewDiscount = new grpc::Method<global::DiscountServices.Protos.RequestAddNewDiscount, global::DiscountServices.Protos.ResponseAddNewDiscount>(
        grpc::MethodType.Unary,
        __ServiceName,
        "AddNewDiscount",
        __Marshaller_RequestAddNewDiscount,
        __Marshaller_ResponseAddNewDiscount);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::DiscountServices.Protos.ProtobufReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of DiscountServicesProto</summary>
    [grpc::BindServiceMethod(typeof(DiscountServicesProto), "BindService")]
    public abstract partial class DiscountServicesProtoBase
    {
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::DiscountServices.Protos.ResponseGetDiscountBycode> GetDiscountByCode(global::DiscountServices.Protos.RequestGetDiscountBycode request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::DiscountServices.Protos.ResponseGetDiscountBycode> GetDiscountById(global::DiscountServices.Protos.RequestGetDiscountById request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::DiscountServices.Protos.ResponseUseDiscount> UseDiscount(global::DiscountServices.Protos.RequestUseDiscount request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::DiscountServices.Protos.ResponseAddNewDiscount> AddNewDiscount(global::DiscountServices.Protos.RequestAddNewDiscount request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for DiscountServicesProto</summary>
    public partial class DiscountServicesProtoClient : grpc::ClientBase<DiscountServicesProtoClient>
    {
      /// <summary>Creates a new client for DiscountServicesProto</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public DiscountServicesProtoClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for DiscountServicesProto that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public DiscountServicesProtoClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected DiscountServicesProtoClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected DiscountServicesProtoClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::DiscountServices.Protos.ResponseGetDiscountBycode GetDiscountByCode(global::DiscountServices.Protos.RequestGetDiscountBycode request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetDiscountByCode(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::DiscountServices.Protos.ResponseGetDiscountBycode GetDiscountByCode(global::DiscountServices.Protos.RequestGetDiscountBycode request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetDiscountByCode, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::DiscountServices.Protos.ResponseGetDiscountBycode> GetDiscountByCodeAsync(global::DiscountServices.Protos.RequestGetDiscountBycode request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetDiscountByCodeAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::DiscountServices.Protos.ResponseGetDiscountBycode> GetDiscountByCodeAsync(global::DiscountServices.Protos.RequestGetDiscountBycode request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetDiscountByCode, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::DiscountServices.Protos.ResponseGetDiscountBycode GetDiscountById(global::DiscountServices.Protos.RequestGetDiscountById request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetDiscountById(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::DiscountServices.Protos.ResponseGetDiscountBycode GetDiscountById(global::DiscountServices.Protos.RequestGetDiscountById request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetDiscountById, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::DiscountServices.Protos.ResponseGetDiscountBycode> GetDiscountByIdAsync(global::DiscountServices.Protos.RequestGetDiscountById request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetDiscountByIdAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::DiscountServices.Protos.ResponseGetDiscountBycode> GetDiscountByIdAsync(global::DiscountServices.Protos.RequestGetDiscountById request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetDiscountById, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::DiscountServices.Protos.ResponseUseDiscount UseDiscount(global::DiscountServices.Protos.RequestUseDiscount request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return UseDiscount(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::DiscountServices.Protos.ResponseUseDiscount UseDiscount(global::DiscountServices.Protos.RequestUseDiscount request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_UseDiscount, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::DiscountServices.Protos.ResponseUseDiscount> UseDiscountAsync(global::DiscountServices.Protos.RequestUseDiscount request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return UseDiscountAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::DiscountServices.Protos.ResponseUseDiscount> UseDiscountAsync(global::DiscountServices.Protos.RequestUseDiscount request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_UseDiscount, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::DiscountServices.Protos.ResponseAddNewDiscount AddNewDiscount(global::DiscountServices.Protos.RequestAddNewDiscount request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return AddNewDiscount(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::DiscountServices.Protos.ResponseAddNewDiscount AddNewDiscount(global::DiscountServices.Protos.RequestAddNewDiscount request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_AddNewDiscount, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::DiscountServices.Protos.ResponseAddNewDiscount> AddNewDiscountAsync(global::DiscountServices.Protos.RequestAddNewDiscount request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return AddNewDiscountAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::DiscountServices.Protos.ResponseAddNewDiscount> AddNewDiscountAsync(global::DiscountServices.Protos.RequestAddNewDiscount request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_AddNewDiscount, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected override DiscountServicesProtoClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new DiscountServicesProtoClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static grpc::ServerServiceDefinition BindService(DiscountServicesProtoBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_GetDiscountByCode, serviceImpl.GetDiscountByCode)
          .AddMethod(__Method_GetDiscountById, serviceImpl.GetDiscountById)
          .AddMethod(__Method_UseDiscount, serviceImpl.UseDiscount)
          .AddMethod(__Method_AddNewDiscount, serviceImpl.AddNewDiscount).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static void BindService(grpc::ServiceBinderBase serviceBinder, DiscountServicesProtoBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_GetDiscountByCode, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::DiscountServices.Protos.RequestGetDiscountBycode, global::DiscountServices.Protos.ResponseGetDiscountBycode>(serviceImpl.GetDiscountByCode));
      serviceBinder.AddMethod(__Method_GetDiscountById, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::DiscountServices.Protos.RequestGetDiscountById, global::DiscountServices.Protos.ResponseGetDiscountBycode>(serviceImpl.GetDiscountById));
      serviceBinder.AddMethod(__Method_UseDiscount, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::DiscountServices.Protos.RequestUseDiscount, global::DiscountServices.Protos.ResponseUseDiscount>(serviceImpl.UseDiscount));
      serviceBinder.AddMethod(__Method_AddNewDiscount, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::DiscountServices.Protos.RequestAddNewDiscount, global::DiscountServices.Protos.ResponseAddNewDiscount>(serviceImpl.AddNewDiscount));
    }

  }
}
#endregion
