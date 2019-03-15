using System.Net.Sockets;

using jcIDS.lib.Helpers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace jcIDS.lib.UnitTests
{
    [TestClass]
    public class ExtensionMethodTests
    {
        [TestMethod]
        public void ExtensionMethods_UnknownPacketType()
        {
            const byte packet = 123;

            var packetType = packet.ToProtocolType();

            Assert.IsTrue(packetType == ProtocolType.Unknown);
        }
    }
}