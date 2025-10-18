using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCMS.DataLayer.Models
{
    public class Categury:BaseEntity
    {
        public string CateguryTitle { get; set; }
        public ICollection<Page> Pages { get; set; }
    }
}
