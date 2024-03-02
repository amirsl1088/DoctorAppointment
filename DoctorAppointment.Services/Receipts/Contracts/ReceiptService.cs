using DoctorAppointment.Services.Receipts.Contracts.Dto;

namespace DoctorAppointment.Services.Unit.Tests.Receipts
{
    public interface ReceiptService
    {
        Task Add(AddReceiptDto dto);
        Task Delete(int id);
        Task<List<GetReceiptDto>> GetAll();
        Task Update(int id,UpdateReceiptDto dto);
    }
}
