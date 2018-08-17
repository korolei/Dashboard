using System;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace Dashboard.Common.Services
{
    internal class GenericServiceProxy<TInterface, TClass> : SimpleServiceProxy<TInterface, TClass>
        where TClass : class, TInterface
        where TInterface : class
    {
        public readonly Uri EndpointUri;

        public GenericServiceProxy(Uri endpointAddress)
        {
            EndpointUri = endpointAddress;
        }

        protected override TClass CreateInstance()
        {
            EndpointAddress address = new EndpointAddress(EndpointUri);
            Binding binding = CreateBinding(EndpointUri);

            TClass instance = (TClass)Activator.CreateInstance(typeof(TClass), binding, address);

            return instance;
        }

        private static Binding CreateBinding(Uri url)
        {
            Binding binding = null;

            if (url.Scheme == Uri.UriSchemeHttps)
            {
                var httpBinding = new CustomBinding();
                var httpTransport = new HttpsTransportBindingElement
                {
                    AuthenticationScheme = AuthenticationSchemes.Negotiate,
                    RequireClientCertificate = false
                };
                
                httpBinding.Elements.Add(httpTransport);
                binding = httpBinding;
            }
            else
            {
                BasicHttpBinding httpBinding =
                    new BasicHttpBinding
                    {
                        Security =
                        {
                            Mode = BasicHttpSecurityMode.TransportCredentialOnly,
                            Transport = {ClientCredentialType = HttpClientCredentialType.Windows}
                        },
                        MaxReceivedMessageSize = Int32.MaxValue
                    };


                binding = httpBinding;
            }

            return binding;
        }
    }

    public class SimpleServiceProxy<TInterface, TClass> : IServiceProxy<TInterface>
        where TClass : class, TInterface
        where TInterface : class
    {
        protected virtual TClass CreateInstance()
        {
            return Activator.CreateInstance<TClass>();
        }

        protected void CleanupInstance(TClass instance)
        {
            if (instance == null) return;
            var disposableInstance = instance as IDisposable;
            //disposableInstance?.Dispose();
        }


        public void Execute(Action<TInterface> methodToExecute)
        {
            TClass instance = null;

            try
            {
                instance = CreateInstance();
                methodToExecute(instance);
            }
            finally
            {
                if (instance != null)
                    CleanupInstance(instance);
            }
        }

        public void Execute(Action<TInterface> methodToExecute, bool explicitlyOpen)
        {
            Execute(methodToExecute);
        }

        public TResult Execute<TResult>(Func<TInterface, TResult> methodToExecute)
        {
            TClass instance = null;

            try
            {
                instance = CreateInstance();

                return methodToExecute(instance);
            }
            finally
            {
                if (instance != null)
                    CleanupInstance(instance);
            }
        }

        public TResult Execute<TResult>(Func<TInterface, TResult> methodToExecute, bool explicitlyOpen)
        {
            return Execute(methodToExecute);
        }

        public void ExecuteAsync(Func<TInterface, Func<AsyncCallback, object, IAsyncResult>> methodToExecute, Func<TInterface, Action<IAsyncResult>> methodToCleanup)
        {
            TClass instance = null;

            try
            {
                instance = CreateInstance();

                using (var task = Task.Factory.StartNew(() => methodToExecute(instance)))
                {
                    task.Wait();

                    var cleanup = methodToCleanup(instance);
                    var callBack = new AsyncCallback(cleanup);

                    task.Result(callBack, null);
                }
            }
            finally
            {
                if (instance != null)
                    CleanupInstance(instance);
            }
        }

        public TResult ExecuteAsync<TArg, TResult>(Func<TInterface, Func<TArg, AsyncCallback, object, IAsyncResult>> methodToExecute, Func<TInterface, Func<IAsyncResult, TResult>> methodToCleanup, TArg argument)
        {
            TClass instance = null;

            try
            {
                instance = CreateInstance();

                using (var task = Task.Factory.StartNew(() => methodToExecute(instance)))
                {
                    task.Wait();

                    IAsyncResult asyncResult = task.Result(argument, null, null);

                    var cleanup = methodToCleanup(instance);

                    TResult result = cleanup(asyncResult);

                    return result;
                }
            }
            finally
            {
                if (instance != null)
                    CleanupInstance(instance);
            }
        }
    }
}
