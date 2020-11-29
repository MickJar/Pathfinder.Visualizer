using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Pathfinder.Visualizer.Data;
using Pathfinder.Visualizer.Solvers;

namespace Pathfinder.Visualizer.BlazorClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddSingleton<PathfinderService>();
            builder.Services.AddSingleton<BoardState>();
            builder.Services.AddSingleton<DijkstraSolver>();

            await builder.Build().RunAsync();
        }
    }
}
