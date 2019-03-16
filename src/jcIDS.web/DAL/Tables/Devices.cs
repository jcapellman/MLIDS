using jcIDS.web.DAL.Tables.Base;

namespace jcIDS.web.DAL.Tables
{
    public class Devices : BaseTable
    {
        public string Name { get; set; }

        public string Token { get; set; }

        public byte[] Model { get; set; }
    }
}