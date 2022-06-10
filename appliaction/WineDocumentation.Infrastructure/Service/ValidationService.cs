using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WineDocumentation.Infrastructure.Service
{
    public class ValidationService : IValidationService
    {
        private static readonly Regex NameRegex = new Regex("^(?![_.-])(?!.*[_.-]{2})[a-zA-Z0-9._.-]+(?<![_.-])$");

        public ValidationService()
        {
        }
        
        public async Task<bool> NameVlidate(string name)
        {
            bool isValid = false;
            try 
            {

                if (string.IsNullOrWhiteSpace(name))
                {
                    throw new Exception("Name is empty or white space.");
                }

                // if(!NameRegex.IsMatch(name))
                // {
                //     throw new Exception("Name is invalid.");
                // }

                isValid = true;
            }
            catch (Exception ex)
            {   
                await Task.FromException(ex);
            }

            return await Task.FromResult(isValid);
        }
    }
}