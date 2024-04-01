using Microsoft.AspNetCore.Mvc.Testing;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediportaTests.Mediporta.Test.Integration
{
    internal class MediportaTestApp : WebApplicationFactory<Program>
    {
        public HttpClient Client { get; }

        public MediportaTestApp()
        {
            Client = WithWebHostBuilder(builder =>
            {

            }).CreateClient();
        }

    }
}