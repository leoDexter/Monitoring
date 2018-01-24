using Microsoft.Extensions.Configuration;
using Monitoring.Repository.Exceptions;
using Monitoring.Repository.Interfaces;
using MonitoringApi.dto;
using System;

namespace Monitoring.Repository.Validation
{
    public class TemperatureValidation : ICustomValidation<Temperature>
    {
        CustomValidationException _customValidationException = new CustomValidationException();

        public CustomValidationException ValidationErrors => _customValidationException;        

        public void ValidateInsert(Temperature dto, IConfiguration Config)
        {
            if (dto.CityId <= 0)
                _customValidationException.AddMessage("A temperatura deve estar relacionada a uma cidade.");

            if (dto.CityId <= 0)
                _customValidationException.AddMessage("A temperatura deve estar relacionada a uma cidade.");
        }

        public void ValidateUpdate(Temperature dto, IConfiguration Config)
        {
            throw new NotImplementedException();
        }

        public void ValidateDelete(Temperature dto, IConfiguration Config)
        {
            var cityRepository = new CityRepository(Config);
            var record = cityRepository.GetById(dto.CityId);

            if (dto.CityId <= 0)
                _customValidationException.AddMessage("A cidade deve ser informada.");

            if (record == null || record.Id == 0)
                _customValidationException.AddMessage("A cidade informada não existe mais.");
        }
    }
}
