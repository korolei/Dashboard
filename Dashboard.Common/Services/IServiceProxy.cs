using System;

namespace Dashboard.Common.Services
{
    public interface IServiceProxy<TInterface>
        where TInterface : class
    {
        void Execute(Action<TInterface> methodToExecute);
        void Execute(Action<TInterface> methodToExecute, bool explicitlyOpen);
        TResult Execute<TResult>(Func<TInterface, TResult> methodToExecute);
        TResult Execute<TResult>(Func<TInterface, TResult> methodToExecute, bool explicitlyOpen);
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        void ExecuteAsync(Func<TInterface, Func<AsyncCallback, object, IAsyncResult>> methodToExecute, Func<TInterface, Action<IAsyncResult>> methodToCleanup);
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        TResult ExecuteAsync<TArg, TResult>(Func<TInterface, Func<TArg, AsyncCallback, object, IAsyncResult>> methodToExecute, Func<TInterface, Func<IAsyncResult, TResult>> methodToCleanup, TArg argument);
    }
}
