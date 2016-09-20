using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using Microsoft.Bot.Builder.Dialogs;
using PlungerBot.Bots;
using Microsoft.Bot.Builder.FormFlow;

namespace PlungerBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        //private static IDialog<Plunger> MakeRootDialog()
        //{
        //    return Chain.From(() => FormDialog.FromForm(SimplePlungerBot.BuildForm));
        //}

        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == "message")
            {
                ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                // calculate something for us to return
                int length = (activity.Text ?? string.Empty).Length;

                // return our reply to the user
                Activity reply = activity.CreateReply($"You sent {activity.Text} which was {length} characters");
                await connector.Conversations.ReplyToActivityAsync(reply);
            }
            else
            {
                HandleSystemMessage(activity);
            }

            return new HttpResponseMessage(System.Net.HttpStatusCode.Accepted);
        }

        private Activity HandleSystemMessage(Activity activity)
        {
            if (activity.Type == "Ping")
            {
                Activity reply = activity.CreateReply();
                reply.Type = "Ping";
                return reply;
            }
            else if (activity.Type == "DeleteUserData")
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (activity.Type == "BotAddedToConversation")
            {
            }
            else if (activity.Type == "BotRemovedFromConversation")
            {
            }
            else if (activity.Type == "UserAddedToConversation")
            {
            }
            else if (activity.Type == "UserRemovedFromConversation")
            {
            }
            else if (activity.Type == "EndOfConversation")
            {
            }

            return null;
        }
    }
}