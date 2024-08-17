using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using PacketDotNet;

namespace MLIDS.UnitTests.lib.ML.Objects
{
    [TestClass]
    public class PayloadItem
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PayloadItem_NullTest()
        {
            new MLIDS.lib.ML.Objects.PayloadItem(ProtocolType.IPv6NoNextHeader, null, payloadPacket: null, false);
        }
    }
}