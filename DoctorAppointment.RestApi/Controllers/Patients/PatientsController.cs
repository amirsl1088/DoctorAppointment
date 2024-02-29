﻿using DoctorAppointment.Services.Patients.Contracts;
using DoctorAppointment.Services.Patients.Contracts.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointment.RestApi.Controllers.Patients
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly PatientService _service;
        public PatientsController(PatientService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task Add([FromBody]AddPatientDto dto)
        {
           await _service.Add(dto);
        }
        [HttpGet]
        public async Task<List<GetPatientDto>> Get()
        {
            return await _service.GetPatients();
        }
        [HttpPut("{id}")]
        public async Task Update([FromRoute]int id,[FromBody]UpdatePatientDto dto)
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
