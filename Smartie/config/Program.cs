using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smartie.config;

namespace Smartie
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Bot bot = new Bot();
            bot.runAsync().GetAwaiter().GetResult();
        }
    }
}