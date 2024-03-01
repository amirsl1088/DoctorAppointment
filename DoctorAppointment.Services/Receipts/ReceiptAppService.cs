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
            var receipt = new Receipt
            {
                DoctorName = dto.DoctorName,
                PatientName = dto.PatientName,
                ReserveDate = dto.ReserveDate,
                DoctorId = dto.DoctorId,
                PatientId = dto.PatientId
            };
            var result = _repository.IsExistDoctor(dto.DoctorName);
            if (result == false)
            {
                throw new DoctorNameNotFound();
            }
            var result2 = _repository.IsExistPatient(dto.PatientName);
            if (result2 == false)
            {
                throw new PatientNameNotFound();
            }
            //var count = receipt.Doctor.Receipts.Count();
            //if (count > 5)
            //{
            //    throw new DoctorCannotVisitMoreThanFivePatients();
            //}
            //receipt.Doctor.Receipts.Add(receipt);
            _repository.Add(receipt);
            await _unitOfWork.Complete();
        }
    }
}
