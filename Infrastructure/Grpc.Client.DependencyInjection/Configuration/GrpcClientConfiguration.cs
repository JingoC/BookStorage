﻿namespace Grpc.Client.DependencyInjection.Configuration
{
    public abstract class GrpcClientConfiguration
    {
        public string Address { get; set; } = null!;
    }
}