using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_MVVM_Base.Models;

namespace WPF_MVVM_Base.Services
{
    public interface IDemoService
    {
        Task<string> Talk();
        Task<string> Talk(int timeout);
        Task<int> Write(string message);
        Models.Messages Read();
    }

    public class DemoService : IDemoService
    {
        private MessagingContext _db = new MessagingContext();

        public DemoService()
        {
            _db.Database.EnsureCreated();
        }

        public async Task<string> Talk(int timeout)
        {
            await Task.Delay(timeout);
            return "Hello World";
        }

        public async Task<string> Talk()
        {
            return await Talk(5000);
        }

        public async Task<int> Write(string message)
        {
            _db.Add(new Message() { MessageId = Guid.NewGuid(), CreateAt = DateTime.Now, Text = message });
            return await _db.SaveChangesAsync();
        }

        public Models.Messages Read()
        {
            return new Models.Messages(_db.Messages.OrderBy(b => b.CreateAt).ToList());
        }
    }
}
