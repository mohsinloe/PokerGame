using Microsoft.Extensions.DependencyInjection.Extensions;
using PokerGame.Core.Interfaces;
using PokerGame.Core.Services;
using System.Reflection;

namespace PokerGame.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            
            builder.Services.AddSwaggerGen(c =>
            {
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
            });

            builder.Services.AddSingleton<IDeckService, DeckService>();
            builder.Services.AddSingleton<IPlayerService, PlayerService>();
            builder.Services.AddSingleton<IHandService, HandService>();
            builder.Services.AddSingleton<IScorerService, ScorerService>();
            builder.Services.AddSingleton<IGameService, GameService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
