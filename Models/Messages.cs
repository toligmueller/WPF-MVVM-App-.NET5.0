using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace WPF_MVVM_Base.Models
{
    public class MessagingContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite("Data Source=messaging.db");
    }

    public class Messages : ObservableCollection<Message> 
    {
        public Messages() : base() { }

        public Messages(ObservableCollection<Message> value) : base(value) { }

        public Messages(List<Message> value) : base(value) { }

        public Messages(IEnumerable<Message> value) : base(value) { }
    }

    public class Message : ObservableObject, IDataErrorInfo
    {
        public Guid MessageId
        {
            get => _messageId;
            set => SetProperty(ref _messageId, value);
        }
        private Guid _messageId;

        public DateTime CreateAt
        {
            get => _createdAt;
            set => SetProperty(ref _createdAt, value);
        }
        private DateTime _createdAt;

        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }
        private string _text;

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(Text):
                        if (string.IsNullOrEmpty(Text))
                        {
                            return "Text can't be empty";
                        }
                        break;
                }
                return string.Empty;
            }
        }
    }
}
