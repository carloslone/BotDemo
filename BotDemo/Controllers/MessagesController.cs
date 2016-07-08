﻿using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Utilities;
using Newtonsoft.Json;

namespace BotDemo
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<Message> Post([FromBody]Message message)
        {
            if (message.Type == "Message")
            {
                var contador = message.GetBotPerUserInConversationData<int>("contador");

                
                Message respuestaMensaje = message.
                    CreateReplyMessage($"{++contador} Usted dijo: { message.Text}");

                respuestaMensaje.SetBotPerUserInConversationData("contador", contador);

                return respuestaMensaje;
            }
            else
            {
                return HandleSystemMessage(message);
            }
        }

        private Message HandleSystemMessage(Message message)
        {
            if (message.Type == "Ping")
            {
                Message reply = message.CreateReplyMessage();
                reply.Type = "Ping";
                return reply;
            }
            else if (message.Type == "DeleteUserData")
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == "BotAddedToConversation")
            {
                Message reply = message.CreateReplyMessage();
                reply.Type = "Message";
                if (DateTime.Now.Hour < 12)
                    reply.Text = "Buenos Días, ¿Como puedo ayudarle?";
                else if (DateTime.Now.Hour >= 12 && DateTime.Now.Hour <= 18)
                    reply.Text = "Buenas Tardes, ¿Como puedo ayudarle?";
                else
                    reply.Text = "Buenas Noches, ¿Como puedo ayudarle?";
                return reply;
            }
            else if (message.Type == "BotRemovedFromConversation")
            {
            }
            else if (message.Type == "UserAddedToConversation")
            {
               
            }
            else if (message.Type == "UserRemovedFromConversation")
            {
            }
            else if (message.Type == "EndOfConversation")
            {
            }

            return null;
        }
    }
}