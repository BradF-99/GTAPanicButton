using System.ComponentModel;
using PanicButton.Models;
using PanicButton.Services;
using Sentry;

namespace PanicButton.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private string _status;

        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                NotifyPropertyChanged();
            }
        }

        private int _progress;

        public int Progress
        {
            get
            {
                return _progress;
            }
            set
            {
                _progress = value;
                NotifyPropertyChanged();
            }
        }

        private readonly XInputService _xInputService;

        public MainWindowViewModel()
        {
            EventAggregator.Instance.Subscribe(this);
            _xInputService = new XInputService();
            Status = "Currently idle.";
        }

        public override void Handle(SignalMessage msg)
        {
            if(msg.exception != null)
                SentrySdk.CaptureException(msg.exception);

            if (msg.progress != null && msg.progress != Progress)
                Progress = (int)msg.progress;

            if (msg.processStatus != SignalMessageEnums.ProcessStatus.None)
            {
                Status = msg.processStatus switch
                {
                    SignalMessageEnums.ProcessStatus.TerminationError =>
                        "An error occurred when closing one of the game processes.",
                    SignalMessageEnums.ProcessStatus.SuspendError =>
                        "An error occurred when suspending the game process.",
                    SignalMessageEnums.ProcessStatus.ResumeError => "An error occurred when resuming the game process.",
                    SignalMessageEnums.ProcessStatus.Terminated => "Attempted to terminate game and associated processes.",
                    SignalMessageEnums.ProcessStatus.Suspended => "Game process suspended, please wait.",
                    SignalMessageEnums.ProcessStatus.Resumed => "Game process resumed.",
                    SignalMessageEnums.ProcessStatus.None => "How did this happen?",
                    _ => "Hi! This message should never appear."
                };
            }
            else if (msg.controllerStatus != SignalMessageEnums.ControllerStatus.None)
            {
                Status = msg.controllerStatus switch
                {
                    SignalMessageEnums.ControllerStatus.ControllerDetected => "Controller detected.",
                    SignalMessageEnums.ControllerStatus.ControllerRemoved => "Controller removed.",
                    _ => "Hi! This is another message that shouldn't appear."
                };
            }
        }
    }
}
