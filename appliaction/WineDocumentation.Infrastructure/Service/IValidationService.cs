using System.Threading.Tasks;

namespace WineDocumentation.Infrastructure.Service
{
    public interface IValidationService
    {
        Task<bool> NameVlidate(string name);
    }
}