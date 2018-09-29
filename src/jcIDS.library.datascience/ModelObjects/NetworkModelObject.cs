using Microsoft.ML.Runtime.Api;

namespace jcIDS.library.datascience.ModelObjects
{
    public class NetworkModelObject
    {
        [Column("0")]
        public string IPAddress { get; set; }
    }
}