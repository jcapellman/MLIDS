using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using jcIDS.lib.Handlers;

namespace jcIDS.lib.UnitTests
{
    [TestClass]
    public class AuthHandlerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AuthHandlerTests_NullConstructor()
        {
            var authHandler = new AuthHandler(null);
        }
    }
}