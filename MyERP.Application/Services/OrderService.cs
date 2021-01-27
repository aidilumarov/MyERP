using AutoMapper;
using MyERP.Application.Repositories;
using MyERP.Dtos;
using MyERP.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MyERP.Application.Services
{
    public class OrderService : AbstractService<OrderDto, Order>
    {
        private readonly IReportWriter<OrderDto> _writer;

        public OrderService(IRepository<Order> repository, IMapper mapper, IReportWriter<OrderDto> writer)
            : base(repository, mapper)
        {
            _writer = writer;
        }

        /// <summary>
        /// Generates report
        /// </summary>
        /// <returns>Path to report file</returns>
        public async Task<FileInfo> GenerateReport(DateTime startDate, DateTime endDate)
        {
            var relevantEntries = await FilterAsync(x => x.Date >= startDate && x.Date <= endDate);
            var fileInfo = _writer.WriteReportToFile(relevantEntries);
            return fileInfo;
        }

        public async Task<FileInfo> GenerateReport()
        {
            var relevantEntries = await GetAsync();
            var fileInfo = _writer.WriteReportToFile(relevantEntries);
            return fileInfo;
        }
    }
}
