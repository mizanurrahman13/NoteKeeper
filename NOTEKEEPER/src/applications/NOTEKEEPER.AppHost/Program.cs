var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.NOTEKEEPER_Api>("notekeeper-api");

builder.Build().Run();
