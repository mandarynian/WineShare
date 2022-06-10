using NLog;
using System;
using System.Threading.Tasks;
using WineDocumentation.Core.Domain;

namespace WineDocumentation.Infrastructure.Service
{
    public interface IHandlerTaskRunner
    {
        IHandlerTask Run(Func<Task> runAsync); 
    }
}