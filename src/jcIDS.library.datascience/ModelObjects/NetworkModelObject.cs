using System.ComponentModel.DataAnnotations.Schema;

namespace jcIDS.library.datascience.ModelObjects
{
    public class NetworkModelObject
    {
        [Column("0")]
        public string IPAddress { get; set; }
    }
}