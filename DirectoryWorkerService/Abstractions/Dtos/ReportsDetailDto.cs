using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions.Dtos
{
    public class ReportsDetailDto
    {

        public long Id { get; set; }
        public string konum { get; set; }
        public long kisisayisi { get; set; }
        public long telefonsayisi { get; set; }
        public List<ReportsDto> Reports { get; set; }
        public long ReportsId { get; set; }
        public List<long> UserId { get; set; }
    }
}
