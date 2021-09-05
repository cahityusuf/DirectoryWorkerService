using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstractions.Data;
using Abstractions.Dtos;
using Abstractions.Results;
using Domain.Entities;
using Infrastructure.DataContexts;
using Microsoft.Extensions.DependencyInjection;

namespace DirectoryWorkerService.Services
{
    public class ReportService
    {
        private readonly IServiceProvider _serviceProvider;

        public ReportService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IDataResult<List<ReportsDetailDto>> Rapor()
        {
            //var _contactInfo = _unitOfWork.GetRepository<ContactInfo>();
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DirectoryDbContext>();

                var res = context.ContactInfo.Where(p => p.ContactTypeId == 3).ToList();

                if (res != null)
                {
                    var result = res.GroupBy(x => new { x.Description })
                        .Select(b => new ReportsDetailDto()
                        {
                            kisisayisi = b.Select(bn => bn.UserId).ToList().Count(),
                            konum = b.Where(s => s.Description == b.Key.Description).FirstOrDefault().Description,
                            UserId = b.Where(s => s.Description == b.Key.Description).Select(s => s.UserId).ToList(),
                        }).ToList();

                    if (result.Count != 0)
                    {
                        var telefonListesi = context.ContactInfo.Where(p => p.ContactTypeId == 1).ToList();

                        if (telefonListesi.Count != 0)
                        {
                            foreach (var item in telefonListesi)
                            {
                                foreach (var group in result)
                                {
                                    var telefonuVarmi = group.UserId.Exists(p => p == item.UserId);

                                    if (telefonuVarmi)
                                    {
                                        group.telefonsayisi += 1;
                                    }
                                }
                            }
                        }

                    }

                    return new SuccessDataResult<List<ReportsDetailDto>>(result.ToList());
                }
            }


            return new ErrorDataResult<List<ReportsDetailDto>>("Hata Oluştu");
        }
    }
}
