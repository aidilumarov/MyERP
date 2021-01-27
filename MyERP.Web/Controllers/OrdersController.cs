using Microsoft.AspNetCore.Mvc;
using MyERP.Application.Services;
using MyERP.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyERP.Web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly OrderService _orderService;

        public OrdersController(OrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetAsync();
            return View(orders);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View(new OrderDto());
        }

        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> CreatePost(OrderDto order)
        {
            try
            {
                await _orderService.CreateAsync(order);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> Edit([FromRoute] string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guid = Guid.Parse(id);
            var order = await _orderService.GetAsync(guid);
            return View(order);
        }

        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> EditPost(OrderDto order)
        {
            try
            {
                await _orderService.UpdateAsync(order.Id, order);
                return RedirectToAction(nameof(Index)); 
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var guid = Guid.Parse(id);
                await _orderService.DeleteAsync(guid);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> Report()
        {
            return View();
        }

        public async Task<IActionResult> OrdersReport([FromQuery]OrderReportRequest reportRequest)
        {
            try
            {
                FileInfo fileInfo;

                if (!reportRequest.From.HasValue || !reportRequest.To.HasValue)
                {
                    fileInfo = await _orderService.GenerateReport();
                }
                else
                {
                    fileInfo = await _orderService.GenerateReport(reportRequest.From.Value, reportRequest.To.Value);
                }

                string mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                
                return new PhysicalFileResult(fileInfo.FullName, mimeType)
                {
                    FileDownloadName = "OrdersReport.xlsx"
                };  
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
