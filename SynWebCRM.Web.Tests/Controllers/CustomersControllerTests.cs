using System;
using System.Collections.Generic;
using System.Text;
using SynWebCRM.Data.Mock;
using SynWebCRM.Web.Controllers;
using Xunit;

namespace SynWebCRM.Web.Tests.Controllers
{
    class CustomersControllerTests
    {
        [Fact]
        public void EditInvalidVKTest()
        {
            var contoller = new CustomersController(new CustomerRepository());
        }
    }
}
