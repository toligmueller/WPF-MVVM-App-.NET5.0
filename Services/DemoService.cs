using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_MVVM_Base.Services
{
    public interface IDemoService
    {
        Task<string> Talk();
        Task<string> Talk(int timeout);
    }

    public class DemoService : IDemoService
    {
        public async Task<string> Talk(int timeout)
        {
            await Task.Delay(timeout);
            return "Hello World";
        }
        public async Task<string> Talk()
        {
            return await Talk(5000);
        }
    }
}
