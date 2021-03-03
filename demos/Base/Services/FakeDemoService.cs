using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_MVVM_Base.Services
{
    public class FakeDemoService : IDemoService
    {
        public Task<string> Talk() => throw new NotImplementedException();
        public Task<string> Talk(int timeout) => throw new NotImplementedException();
        public Task<int> Write(string message) => throw new NotImplementedException();
        public Models.Messages Read() => throw new NotImplementedException();
    }
}
