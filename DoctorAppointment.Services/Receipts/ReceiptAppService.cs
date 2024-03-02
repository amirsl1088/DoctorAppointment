using DoctorAppointment.Contracts.Interfaces;
using DoctorAppointment.Entities.Receipts;
using DoctorAppointment.Services.Receipts.Contracts.Dto;
using DoctorAppointment.Services.Receipts.Exceptions;

namespace DoctorAppointment.Services.Unit.Tests.Receipts
{
    public class ReceiptAppService : ReceiptService
    {
        private readonly ReceiptRepository _repository;
        private readonly UnitOfWork _unitOfWork;
        public ReceiptAppService
            (ReceiptRepository repository
            ,UnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Add(AddReceiptDto dto)
        {
            var doctor = await _repository.FindDoctorById(dto.DoctorId);
            var patient = await _repository.FindPatientById(dto.PatientId);
            
            if(doctor == null)
            {
                throw new DoctorIdNotFoundException();
            }
            
            if (patient ==null)
            {
                throw new PatientIdNotFoundException();
            }
           
            var receipt = new Receipt
            {
                DoctorId = doctor.Id,
                PatientId = patient.Id,
                ReserveDate = dto.ReserveDate
            };
            var result = _repository.CountOfReceipts();
            
            if (result > 5)
            {
                throw new DoctorCannotVisitMoreThanFivePatientsException();
            }
            if (dto.ReserveDate < DateTime.UtcNow)
            {
                throw new DateTimePassedException();
            }


            _repository.Add(receipt);
            await _unitOfWork.Complete();
        }

        public async Task Delete(int id)
        {
            var receipt = await _repository.FindReceiptById(id);
            if(receipt is null)
            {
                throw new ReceiptIdNotFoundException();
            }
            _repository.Delete(receipt);
            await _unitOfWork.Complete();
        }

        public async Task<List<GetReceiptDto>> GetAll()
        {
          return await  _repository.GetAll();
        }

        public async Task Update(int id,UpdateReceiptDto dto)
        {
            var receipt = await _repository.FindReceiptById(id);
            if (receipt == null)
            {
                throw new ReceiptIdNotFoundException();
            }
            receipt.ReserveDate = dto.ReserveDate;
            await _unitOfWork.Complete();

        }
    }
}
