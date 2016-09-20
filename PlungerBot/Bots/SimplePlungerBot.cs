using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlungerBot.Bots
{
    public class SimplePlungerBot
    {
        public static IForm<Plunger> BuildForm()
        {
            return new FormBuilder<Plunger>()
                .Field(nameof(Plunger.CalculationType))
                .Field(nameof(Plunger.Force), (plunger) => plunger.CalculationType != PlungerCalculationType.Force)
                .Field(nameof(Plunger.Area), (plunger) => plunger.CalculationType != PlungerCalculationType.Area)
                .Field(nameof(Plunger.Pressure), (plunger) => plunger.CalculationType != PlungerCalculationType.Pressure)
                .OnCompletionAsync(async (context, plunger) =>
                {
                    plunger.Calculate();

                    var message = context.MakeMessage();
                    message.Text = plunger.ToString();
                    await context.PostAsync(message);
                }).Build();
        }
    }
}