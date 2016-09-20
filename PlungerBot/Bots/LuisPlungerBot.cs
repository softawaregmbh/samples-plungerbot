using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PlungerBot.Bots
{
    [LuisModel("28c0d547-1418-49a1-84e1-9c87736a6c39", "53642da501a640e5bb41bc27d2079fa1")]
    [Serializable]
    public class LuisPlungerBot : LuisDialog<Plunger>
    {
        private readonly BuildFormDelegate<Plunger> MakePlungerForm;

        internal LuisPlungerBot(BuildFormDelegate<Plunger> makePlungerForm)
        {
            this.MakePlungerForm = makePlungerForm;
        }

        [LuisIntent("None")]
        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            var plungerForm = new FormDialog<Plunger>(new Plunger(), this.MakePlungerForm, FormOptions.PromptInStart);
            context.Call<Plunger>(plungerForm, PlungerFormComplete);
        }

        [LuisIntent("Calculate")]
        public async Task CalculatePlunger(IDialogContext context, LuisResult result)
        {
            var plungerForm = new FormDialog<Plunger>(new Plunger(), this.MakePlungerForm, FormOptions.PromptInStart, result.Entities);

            context.Call<Plunger>(plungerForm, PlungerFormComplete);
        }

        private async Task PlungerFormComplete(IDialogContext context, IAwaitable<Plunger> result)
        {
            // TODO
        }
    }
}