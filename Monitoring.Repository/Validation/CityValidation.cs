using Microsoft.Extensions.Configuration;
using Monitoring.Repository.Exceptions;
using Monitoring.Repository.Interfaces;
using MonitoringApi.dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Monitoring.Repository.Validation
{
    public class CityValidation : ICustomValidation<City>
    {
        CustomValidationException _customValidationException;

        public CustomValidationException ValidationErrors
        {
            get { return _customValidationException != null ? _customValidationException : _customValidationException = new CustomValidationException(); }
        }

        public void ValidateInsert(City dto, IConfiguration Config)
        {
            var cityRepository = new CityRepository(Config);            

            if (cityRepository.AlreadyExists(dto.Name))
                ValidationErrors.AddMessage("A cidade informada já está cadastrada.");                

            if (string.IsNullOrWhiteSpace(dto.Name))
                ValidationErrors.AddMessage("O nome da cidade deve ser informado.");
        }

        public void ValidateUpdate(City dto, IConfiguration Config)
        {
            if (dto.Id <= 0)
                ValidationErrors.AddMessage("O id da cidade deve ser informado.");
        }

        public void ValidateDelete(City dto, IConfiguration Config)
        {
            var cityRepository = new CityRepository(Config);
            var record = cityRepository.GetByName(dto.Name);

            if (record == null && record.Id == 0)
                ValidationErrors.AddMessage("A cidade informada não está cadastrada.");
            if (dto.Id == 0)
                ValidationErrors.AddMessage("O Id da cidade deve ser informado.");
            if (string.IsNullOrWhiteSpace(dto.Name))
                ValidationErrors.AddMessage("O nome da cidade deve ser informado.");
        }
    }
}
