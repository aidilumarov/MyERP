using MyERP.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyERP.Application.Services
{
    public interface IReportWriter<TDto> where TDto : BaseDto
    {
        FileInfo WriteReportToFile(IEnumerable<TDto> items);
    }
}
