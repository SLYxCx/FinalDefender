// FINAL DEFENDER (SPACE INVADERS clone) PROJECT
// Steven Cole + Levi Moreau
// April 25, 2025
// --
// Space Invaders game. User attempts to hold off invading enemies as they attempt to land. 
// --
// Controls: 
// - Move  Left: ←
// - Move Right: →
// - Shoot     : [spacebar]

// < Note: The game is unplayable... >
// < This project was created with AI (Copilot)'s assistance to help figure out how to set up some of the systems and movement >


using FinalDefender.Components.GameSystems;
using Microsoft.Extensions.Logging;

namespace FinalDefender
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddSingleton<GameState>();
            //builder.Services.AddLogging(logging =>
            //{
            //    logging.AddConsole();
            //})

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
