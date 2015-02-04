using System;
using System.Globalization;
using System.Resources;
using System.Threading.Tasks;

namespace ResManDeadlock
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var resManager = new ResourceManager("ResManDeadlock.Forms", typeof(Forms).Assembly);
                var t2 = Task.Run(() =>
                {
                    foreach (var r in resManager.GetResourceSet(CultureInfo.CurrentCulture, true, true))
                    {
                    
                    }
                });

                var t1 = Task.Run(() =>
                {
                    var rs = resManager.GetResourceSet(CultureInfo.CurrentCulture, true, true);
                    rs.GetString("formsEdit_description");
                });

                Task.WaitAll(t1, t2);

                Console.Write(".");
            }
        }
    }
}
