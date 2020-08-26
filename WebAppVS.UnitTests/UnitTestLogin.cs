using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAppVS.Pages;

namespace WebAppVS.UnitTests
{
    [TestClass]
    public class UnitTestLogin
    {
        [TestMethod]
        public void LoginPasswordShoudlBeMandatory()
        {
            var type = typeof(LoginModel);
            var prop = type.GetProperty("Password");

            var attributes = prop.GetCustomAttributes(true);
            Attribute required = null;
            foreach (var attr in attributes)
            {
                required = attr as RequiredAttribute;
                if (required != null) break;
            }
            Assert.IsNotNull(required,
                "Lebensmittelretter: Required attribute not set");
        }
    }
}
