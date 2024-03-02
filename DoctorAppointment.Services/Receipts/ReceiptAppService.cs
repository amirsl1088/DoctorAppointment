using DoctorAppointment.Contracts.Interfaces;
using DoctorAppointment.Entities.Receipts;
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
            var doctor = _repository.FindDoctorById(dto.DoctorId);
            var patient = _repository.FindPatientById(dto.PatientId);
            
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
            var result = _repository.FindDoctorReceipt(doctor.Id).Result;
            var count = result.Count();
            if (count>5)
            {
                throw new DoctorCannotVisitMoreThanFivePatientsException();
            }
            
           
            _repository.Add(receipt);
            await _unitOfWork.Complete();
        }
    }
}
