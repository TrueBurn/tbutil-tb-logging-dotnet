# Introduction 
Custom .NET logging with fully-structured events built on top of [Serilog](https://serilog.net/) that write logs in JSON format

# Getting Started
In order to use the NuGet package in your project, we've provided some examples below how to call the logging lib. The logging lib provides 3 output options:

* ```DebugConsole``` - Custom formatted colored line handy for development
* ```Console``` - JSON formatted output, perfect for containers
* ```File``` - JSON formatted output, saved to a file

## 1. Interface with Autofac logging
This option allows you to register your logging library with autofac container as seen below. 

### Setup for ```DebugConsole``` output
Register the logging lib for colored debug output to console
```
var builder = new ContainerBuilder();

builder.RegisterType<BasicTests>()
	.As<IBasicTest>();

builder.RegisterType<CustomConsoleLogger>()
	.As<ICustomLogger>()
	.WithParameter("_entityType", EntityType.API)
	.WithParameter("_entityName", "MyAutoFacSetup")
	.WithParameter("_logLevel", LogLevel.Verbose)
	.WithParameter("_debugConsole", "true");

var container = builder.Build();

container.Resolve<ICustomLogger>().Verbose("Calling Trace from main");			
```

### Setup for ```Console``` output
Register the logging lib for JSON output to console

```
var builder = new ContainerBuilder();

builder.RegisterType<CustomConsoleLogger>()
    .As<ICustomLogger>()
    .WithParameter("_entityType", EntityType.API)
    .WithParameter("_entityName", "MyAutoFacSetup")
    .WithParameter("_logLevel", LogLevel.Verbose);

var container = builder.Build();

container.Resolve<ICustomLogger>().Verbose("Calling Trace from main");			
```

### Setup for ```File``` output
Register the logging lib for JSON output to file

```
var builder = new ContainerBuilder();

builder.RegisterType<BasicTests>()
	.As<IBasicTest>();

            builder.RegisterType<CustomFileLogger>()
                .As<ICustomLogger>()
                .WithParameter("_entityType", EntityType.Service)
                .WithParameter("_entityName", "MyAutoFacSetup")
                .WithParameter("_fullLogNameAndPath", @"c:\temp\okcool.log")
                .WithParameter("_logLevel", LogLevel.Verbose);

var container = builder.Build();

container.Resolve<ICustomLogger>().Verbose("Calling Trace from main");			
```

## 2. Global static logging
Set up the static logging once and then everywhere you call CustomLogger.___ will output accordingly. The logging library provides a global static handle CustomLogger.___ to access from anywhere within your project, but require the setup below first.

### Setup for ```DebugConsole``` output
If you want to use global logging with colored output for debugging, then follow this setup below 
```
CustomLogger.SetupDebugConsole(EntityType.API, "MotherShipApp", LogLevel.Verbose);
CustomLogger.Verbose("Calling Trace from main");
```

### Setup for ```Console``` output
If you want to use global logging with JSON output for debugging (perfect for containers), then follow this setup below 
```
CustomLogger.SetupConsole(EntityType.API, "MotherShipApp", LogLevel.Verbose);
CustomLogger.Verbose("Calling Trace from main");
```

### Setup for ```File``` output
If you want to use global logging with JSON output for debugging (perfect for containers), then follow this setup below 
```
CustomLogger.SetupFile(EntityType.Service, "MotherShipApp", @"c:\Logs\ConsoleTestApp-.log", LogLevel.Verbose);
CustomLogger.Verbose("Calling Trace from main");
```

## 3. Factory creating a logging instance
This option allows you to create an instance of the logging library using the factory. 

### Setup for ```DebugConsole``` output
Logging instance with colored output for debugging, then follow this setup below 
```
ICustomLogger debugConsoleLogger = 
    CustomLogFactory.CreateLogger(EntityType.Service, "Test_DebugConsole", LogLevel.Debug, true);
debugConsoleLogger.Verbose("Calling Trace from main");
```

### Setup for ```Console``` output
Logging instance with JSON output for container logging, then follow this setup below 
```
ICustomLogger consoleLogger = CustomLogFactory.CreateLogger(EntityType.API, "Test_Console");
debugConsoleLogger.Verbose("Calling Trace from main");
```

### Setup for ```File``` output
Logging instance with JSON output to file logging, then follow this setup below 
```
ICustomLogger fileLogger 
   = CustomLogFactory.CreateLogger(EntityType.Service, "Test_File", @"c:\temp\test2.log");
debugConsoleLogger.Verbose("Calling Trace from main");
```

# Contribute
*Any dev is welcome to make changes. Just need to go through a code-review before merging into master*