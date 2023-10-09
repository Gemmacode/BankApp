
using Core.Services;
using Core.Implementations;
using Microsoft.Extensions.DependencyInjection;
using Core;

var services = new ServiceCollection();
services.AddScoped<IAuth, Auth>();
services.AddScoped<ITransactions, Transactions>();
services.AddSingleton<Homepage>();

var serviceProvider = services.BuildServiceProvider();
var home = serviceProvider.GetRequiredService<Homepage>();

home.Run();