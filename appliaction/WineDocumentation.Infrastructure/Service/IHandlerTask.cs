using NLog;
using System;
using System.Threading.Tasks;
using WineDocumentation.Core.Domain;

namespace WineDocumentation.Infrastructure.Service
{
    public interface IHandlerTask
    {
        IHandlerTask Always(Func<Task> always);
        IHandlerTask OnCustomError(Func<Exception, Task> onCustomError,
            bool propagateException = false, bool executeOnError = false);
        IHandlerTask OnCustomError(Func<Exception, Logger, Task> onCustomError,
            bool propagateException = false, bool executeOnError = false);
        IHandlerTask OnError(Func<Exception, Task> onError, bool propagateException = false);
        IHandlerTask OnError(Func<Exception, Logger, Task> onError, bool propagateException = false);
        IHandlerTask OnSuccess(Func<Task> onSuccess);
        IHandlerTask PropagateException();
        IHandlerTask DoNotPropagateException();
        IHandler Next();
        Task ExecuteAsync();
    }
}