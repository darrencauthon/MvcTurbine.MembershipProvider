using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web.Security;
using AutoMoq;
using Moq;
using MvcTurbine.ComponentModel;
using MvcTurbine.MembershipProvider.PrincipalHelpers;
using NUnit.Framework;
using Should;

namespace MvcTurbine.MembershipProvider.Tests
{
    [TestFixture]
    public class PrincipalFromTicketCreatorTests
    {
        private AutoMoqer mocker;

        [SetUp]
        public void Setup()
        {
            mocker = new AutoMoqer();
        }

        [Test]
        public void Instantiates_a_principal_provider_based_on_the_type_in_user_data_and_then_passes_the_username_and_data_to_it()
        {
            var expected = CreateTestPrincipal();

            var fakePrincipalProvider = new TestPrincipalProvider("username", "user data", expected);

            var ticket = CreateTicket(fakePrincipalProvider.GetType(), "username", "user data");

            var serviceLocator = new TestServiceLocator();
            serviceLocator.ResolveThisInstanceAsThisType(fakePrincipalProvider);

            var creator = new PrincipalFromTicketCreator(serviceLocator);
            var result = creator.Create(ticket);

            result.ShouldBeSameAs(expected);
        }

        private FormsAuthenticationTicket CreateTicket(Type type, string username, string userData)
        {
            return new FormsAuthenticationTicket(1, username, DateTime.Now, DateTime.Now, true, type + ", " + type.Assembly.FullName.Split(',')[0] + "|" + userData);
        }

        private static IPrincipal CreateTestPrincipal()
        {
            return new Mock<IPrincipal>().Object;
        }

        public class TestPrincipalProvider : IPrincipalProvider
        {
            public string Username { get; set; }
            public string UserData { get; set; }
            public IPrincipal PrincipalToReturn { get; set; }

            public TestPrincipalProvider(string username, string userData, IPrincipal principalToReturn)
            {
                Username = username;
                UserData = userData;
                PrincipalToReturn = principalToReturn;
            }

            public PrincipalProviderResult GetPrincipal(string userId, string password)
            {
                return null;
            }

            public IPrincipal CreatePrincipalFromTicketData(string userName, string userData)
            {
                if (userName == Username && userData == userData)
                    return PrincipalToReturn;
                return null;
            }

            public TicketData ConvertPrincipalToTicketData(IPrincipal principal)
            {
                return null;
            }
        }

        #region test service locator
        private class TestServiceLocator : IServiceLocator
        {
            private IDictionary <Type, object> thingsToResolve = new Dictionary<Type,object>();
            public void ResolveThisInstanceAsThisType<T>(T instance)
            {
                thingsToResolve.Add(typeof(T), instance);
            }

            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public T Resolve<T>() where T : class
            {
                return thingsToResolve[typeof (T)] as T;
            }

            public T Resolve<T>(string key) where T : class
            {
                throw new NotImplementedException();
            }

            public T Resolve<T>(Type type) where T : class
            {
                throw new NotImplementedException();
            }

            public object Resolve(Type type)
            {
                return thingsToResolve[type];
            }

            public IList<T> ResolveServices<T>() where T : class
            {
                throw new NotImplementedException();
            }

            public IList<object> ResolveServices(Type type)
            {
                throw new NotImplementedException();
            }

            public IServiceRegistrar Batch()
            {
                throw new NotImplementedException();
            }

            public void Register<Interface>(Type implType) where Interface : class
            {
                throw new NotImplementedException();
            }

            public void Register<Interface, Implementation>() where Implementation : class, Interface
            {
                throw new NotImplementedException();
            }

            public void Register<Interface, Implementation>(string key) where Implementation : class, Interface
            {
                throw new NotImplementedException();
            }

            public void Register(string key, Type type)
            {
                throw new NotImplementedException();
            }

            public void Register(Type serviceType, Type implType)
            {
                throw new NotImplementedException();
            }

            public void Register<Interface>(Interface instance) where Interface : class
            {
                throw new NotImplementedException();
            }

            public void Register<Interface>(Func<Interface> factoryMethod) where Interface : class
            {
                throw new NotImplementedException();
            }

            public void Release(object instance)
            {
                throw new NotImplementedException();
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }

            public TService Inject<TService>(TService instance) where TService : class
            {
                throw new NotImplementedException();
            }

            public void TearDown<TService>(TService instance) where TService : class
            {
                throw new NotImplementedException();
            }
        }
        #endregion
    }
}