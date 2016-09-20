using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;

namespace PlungerBot.Bots
{
    [Serializable]
    public class AdvancedPlungerBot : IDialog<Plunger>
    {
        private Plunger plunger;

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<Message> result)
        {
            var message = await result;

            this.plunger = new Plunger();

            if (message.Text.Equals("hi", StringComparison.CurrentCultureIgnoreCase))
            {
                PromptDialog.Number(context,
                    AreaReceivedAsync,
                    "What area (in cm²)?",
                    "Excuse me, I didn't get you. Please enter the area without any units, e.g. 20");
            }            
        }

        private async Task AreaReceivedAsync(IDialogContext context, IAwaitable<double> result)
        {
            this.plunger.Area = await result;

            var message = context.MakeMessage();
            message.Text = "Thank you and good bye.";
            await context.PostAsync(message);

            context.Done(this.plunger);
        }
    }
}