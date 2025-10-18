using Microsoft.EntityFrameworkCore;
using MyCMS.Application.Repositories.Users.Interfaces;
using MyCMS.Application.Repositories.Users.Services;
using MyCMS.DataLayer.AddAuditFieldInterceptors;
using MyCMS.DataLayer.Contexts;
using MyCMS.WebSite;

var builder = WebApplication.CreateBuilder(args);

var app = builder.ConfigService();

app.ConfigPipeLine();

app.Run();
