﻿global using System;
global using System.Collections;
global using System.Collections.Generic;
global using System.ComponentModel.DataAnnotations;
global using System.Data.Common;
global using System.Linq;
global using System.Linq.Expressions;
global using System.Reflection;
global using System.Text;
global using System.Threading;
global using System.Threading.Tasks;

global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.JsonPatch;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Migrations;
global using Microsoft.EntityFrameworkCore.Design;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Diagnostics.HealthChecks;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Options;

global using Newtonsoft.Json;
global using Newtonsoft.Json.Linq;

global using LightningArc.Contracts;
global using LightningArc.Persistence.Abstractions;
global using LightningArc.Persistence.EntityFramework.Configuration;
global using LightningArc.Persistence.EntityFramework.Context;
global using LightningArc.Persistence.EntityFramework.HealthCheck;

global using LightningArc.QueryBuilder;
global using LightningArc.Domain.Abstractions;
global using LightningArc.Module.Abstractions;
