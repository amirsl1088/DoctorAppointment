using DoctorAppointment.Services.Receipts.Contracts.Dto;
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
        [HttpGet]
        public async Task<List<GetReceiptDto>>GetAll()
        {
          return  await _service.GetAll();
        }
        [HttpPatch("{id}")]
        public async Task Update([FromRoute]int id,UpdateReceiptDto dto)
        {
            await _service.Update(id, dto);
        }
        [HttpDelete("{id}")]
        public async Task Delete([FromRoute]int id)
        {
            await _service.Delete(id);
        }
    }
}
