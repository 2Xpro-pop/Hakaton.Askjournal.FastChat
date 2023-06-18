using System.Net.Http;
using FastChat.Models;
using FastChat.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;
using Radzen;
using Radzen.Blazor;

namespace FastChat.Shared
{
    public partial class MainLayout
    {
        [Inject]
        protected IJSRuntime JSRuntime
        {
            get; set;
        }

        [Inject]
        protected NavigationManager NavigationManager
        {
            get; set;
        }

        [Inject]
        protected DialogService DialogService
        {
            get; set;
        }

        [Inject]
        protected TooltipService TooltipService
        {
            get; set;
        }

        [Inject]
        protected ContextMenuService ContextMenuService
        {
            get; set;
        }

        [Inject]
        protected NotificationService NotificationService
        {
            get; set;
        }
        [Inject]
        protected Api Api
        {
            get; set;
        }

        [Inject]
        protected FastChatCaller FastChatCaller
        {
            get; set;
        }


        public string Message
        {
            get; set;
        }

        public List<MessageVm> Messages = new();

        async Task OnChange(string a)
        {
            var value = a.Trim();
            if (string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            Messages.Add(new MessageVm
            {
                Message = value.Trim(),
                Side = "text-end"
            });

            Message = null;

            StateHasChanged();

            if (value.ToLower() == "список депутатов")
            {
                var all = await Api.GetAllDeputats();
                var msg = string.Join("<br/>", all.Select(x => x.Name));
                Messages.Add(new MessageVm
                {
                    Message = new MarkupString(msg),
                    Side = "text-start"
                });
                StateHasChanged();
                return;
            }

            if (value.ToLower() == "подробнее")
            {
                if (Messages.LastOrDefault(m => m.Message is Deputat)?.Message is not Deputat deputat)
                {
                    Messages.Add(new MessageVm
                    {
                        Message = "Извините, выберте депутата",
                        Side = "text-start"
                    });
                    StateHasChanged();
                    return;
                }


                var votes = string.Join("<br/>", deputat.Votes.Select(x => $"Тема: {x.Name}.<br/> Голос : {x.Vote} <br/>"));
                var analytics = deputat.AnalyticRu;
                var msg = $"<h2>Аналитика</h2><p>{analytics}</p><br/><h2>Участвовал в голосованиях:</h2>{votes} <br/> <h4>Партия: {deputat.Party.Name}</h4>";
                Messages.Add(new MessageVm
                {
                    Message = new MarkupString(msg),
                    Side = "text-start"
                });
                StateHasChanged();
                return;
            }

            if (value.ToLower() == "очистить")
            {
                Messages.Clear();
                StateHasChanged();
                return;
            }

            if (value.Split(' ').Length == 3)
            {
                try
                {
                    var response = await Api.GetDeputat(value.Trim());
                    if (response == null || response.Id == 0)
                    {
                        throw new InvalidOperationException();
                    }
                    Messages.Add(new MessageVm
                    {
                        Message = response,
                        Side = "text-start"
                    });

                    StateHasChanged();
                    return;
                }
                catch(Exception ex)
                {

                }
                
            }
            try
            {
                Messages.Add(new()
                {
                    Message = FastChatCaller.SendQuestion(value),
                    Side = "text-start"
                });

                StateHasChanged();
            }
            catch (Exception ex)
            {
                Messages.Add(new()
                {
                    Message = "Простите я не могу вам помочь ╥﹏╥",
                    Side = "text-start"
                });
                StateHasChanged();
            }
        }

        public List<string> phrases = new()
        {
            "Кто ищет тот всегда найдет!",
            "Желаю вам удачи в поисках (-ω- )",
            "Надеюсь вам это поможет ٩(^ᴗ^)۶",
            "С любовью ваш личный помощник ( ˘ ³˘)♥",
            "Надеюсь я облегчил вам поиски"
        };

        public class MessageVm
        {
            public object Message
            {
                get; set;
            }
            public string Side
            {
                get; set;
            }
        }

        public class VoteCounter
        {
            public string Name
            {
                get; set;
            }
            public int Count
            {
                get; set;
            }
        }
    }
}
