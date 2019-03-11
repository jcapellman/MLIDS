using System;

namespace jcIDS.web.DAL.Tables.Base
{
    public class BaseTable
    {
        public int ID { get; set; }

        public DateTimeOffset Created { get; set; }

        public DateTimeOffset Modified { get; set; }

        public bool Active { get; set; }
    }
}