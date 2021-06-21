// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
await builder.Build().RunAsync();
