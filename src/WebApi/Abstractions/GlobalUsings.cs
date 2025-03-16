global using System.Net;
global using System.Text.Json;

global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Routing;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Http.HttpResults;
global using Microsoft.AspNetCore.JsonPatch;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.Logging;

global using Falcon.Contracts;
global using Falcon.Domain.Abstractions.Interfaces.Entities;
global using Falcon.Domain.Abstractions.Interfaces.Repositories;
global using Falcon.Application.Abstractions.Services;

global using Falcon.WebApi.Abstractions.Middlewares;
