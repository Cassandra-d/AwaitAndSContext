using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class DefaultController : ApiController
    {
        private static int counter = 1;

        [HttpGet]
        public async Task<IHttpActionResult> Index()
        {
            Log();
            // thread A
            // sc saved
            await Magic();
            // thread B
            // sc restored
            Log();

            return Ok();
        }

        async Task Magic()
        {
            Log();
            // thread A
            // sc as before
            await DarkMagic().ConfigureAwait(false);
            // thread B
            // sc is null
            Log();
        }

        async Task DarkMagic()
        {
            Log();
            // thread A
            await (new StreamWriter(Path.GetTempFileName()).WriteAsync(Enumerable.Repeat('a', 5000000).ToArray()));
            // thread B
            Log();
        }

        static void Log()
        {
            Debug.WriteLine($"{counter} Tid:{Thread.CurrentThread.ManagedThreadId} SC:{SynchronizationContext.Current?.GetHashCode() ?? 0}");
            counter += 1;
        }
    }
}
