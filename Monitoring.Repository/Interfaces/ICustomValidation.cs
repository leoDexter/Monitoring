using Microsoft.Extensions.Configuration;
using Monitoring.Repository.Exceptions;

namespace Monitoring.Repository.Interfaces
{
    public interface ICustomValidation<T> where T : class
    {
        CustomValidationException ValidationErrors { get; }
        void ValidateInsert(T dto, IConfiguration config);
        void ValidateUpdate(T dto, IConfiguration config);
        void ValidateDelete(T dto, IConfiguration config);
    }
}
