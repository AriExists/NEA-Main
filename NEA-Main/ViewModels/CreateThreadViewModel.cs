using NEA_Main.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NEA_Main.Data;
using NEA_Main.Models;
using System.Windows.Data;
using System.Text.RegularExpressions;
using NEA_Main.Models.Generated;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace NEA_Main.ViewModels
{
    public class CreateThreadViewModel : ViewModelBase
    {
        public ICommand CreateThreadCommand { get; set; }


        private string? _inputThreadName;
        public string? InputThreadName
        {
            get => _inputThreadName;
            set 
            { 
                _inputThreadName = value;
                OnProperyChanged(nameof(InputThreadName));
            }
        }

        private string? _inputThreadDescription;
        public string? InputThreadDescription
        {
            get => _inputThreadDescription;
            set
            {
                _inputThreadDescription = value;
                OnProperyChanged(nameof(InputThreadDescription));
            }
        }

        private System.Windows.Media.Brush _responseColour;
        public System.Windows.Media.Brush ResponseColour
        {
            get { return _responseColour; }
            set
            {
                _responseColour = value;
                OnProperyChanged(nameof(ResponseColour));
            }
        }

        private string? _result;
        public string? Result
        {
            get { return _result; }
            set
            {
                _result = value;
                OnProperyChanged(nameof(Result));
            }
        }

        private readonly AppViewModel _parentViewModel;
        public CreateThreadViewModel(AppViewModel parentVM)
        {
            _parentViewModel = parentVM;
            CreateThreadCommand = new CreateThreadCommand(this);
        }

        MasterContext context = new MasterContext();
        private ChatThread newThread;
        public void CreateThread()
        {
            newThread = new ChatThread();
            if (!string.IsNullOrWhiteSpace(InputThreadName) && _parentViewModel.CurrentGroupChat != null)
            {
                foreach (GroupChat chat in context.GroupChats)
                {
                    if (chat.Id == _parentViewModel.CurrentGroupChat.Id)
                    {
                        newThread.GroupChatId = chat.Id;
                        newThread.Name = InputThreadName;
                        newThread.Description = InputThreadDescription;
                    }
                }

                if (newThread != null)
                {
                    context.Add(newThread);
                    context.SaveChanges();
                    Output(new OutputResult("Thread Created", System.Windows.Media.Brushes.Green));
                    return;
                }
            }
            Output(new OutputResult("No groupchat to add thread to (select one)", System.Windows.Media.Brushes.Red));
        }

        public void Output(OutputResult result)
        {
            ResponseColour = result.Colour;
            Result = result.Message;
        }
    }
}
