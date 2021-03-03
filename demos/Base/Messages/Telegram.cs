using Microsoft.Toolkit.Mvvm.Messaging.Messages;

namespace WPF_MVVM_Base.Messages
{
    public class TelegramChannel : ValueChangedMessage<string>
    {
        public TelegramChannel(string msg) : base(msg) { }
    }

    public class TelegramRequest : RequestMessage<string> { }
}