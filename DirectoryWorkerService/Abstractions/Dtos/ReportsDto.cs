using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstractions.Enums;

namespace Abstractions.Dtos
{
    public class ReportsDto
    {
        public ReportsDto()
        {
            ReportDetail = new HashSet<ReportsDetailDto>();
        }

        public long Id { get; set; }
        public DateTime ReportDateTime { get; set; }
        public ReportStatusEnum ReportStatusId { get; set; }

        public ICollection<ReportsDetailDto> ReportDetail { get; set; }

    }
}
