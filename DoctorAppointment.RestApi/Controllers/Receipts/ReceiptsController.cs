using DoctorAppointment.Services.Unit.Tests.Receipts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointment.RestApi.Controllers.Receipts
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptsController : ControllerBase
    {
        public readonly ReceiptService _service;
        public ReceiptsController(ReceiptService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task Add(AddReceiptDto dto)
        {
            await _service.Add(dto);
        }
    }
}
