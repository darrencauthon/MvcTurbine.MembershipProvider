using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web.Security;
using MvcTurbine.ComponentModel;
using MvcTurbine.MembershipProvider.Helpers;
using NUnit.Framework;
using Should;

namespace MvcTurbine.MembershipProvider.Tests
{
    [TestFixture]
    public class DefaultFormsAuthenticationTicketCreatorTests
    {
        private TestPrincipalProvider testPrincipalProvider;

        [SetUp]
        public void Setup()
        {
            testPrincipalProvider = new TestPrincipalProvider{TicketDataToReturn = new TicketData()};
        }

        [Test]
        public void Returns_a_forms_authentication_ticket()
        {
            var result = CreateTheTicket();
            result.ShouldNotBeNull();
        }

        [Test]
        public void Version_number_should_be_1()
        {
            var result = CreateTheTicket();
            result.Version.ShouldEqual(1);
        }

        [Test]
        public void Uses_the_current_date_as_the_ticket_issue_date()
        {
            CurrentDateTime.SetNow(new DateTime(2010, 12, 25, 1, 2, 3));

            var result = CreateTheTicket();

            result.IssueDate.ShouldEqual(new DateTime(2010, 12, 25, 1, 2, 3));
        }

        [Test]
        public void Adds_the_number_of_minutes_from_the_principal_provider_to_the_current_date_to_get_the_expiration_date()
        {
            testPrincipalProvider.TicketDataToReturn.NumberOfMinutesUntilExpiration = 31;

            CurrentDateTime.SetNow(new DateTime(2010, 12, 25, 1, 2, 3));
            var expectedExpirationDate = new DateTime(2010, 12, 25, 1, 2, 3).AddMinutes(31);

            var result = CreateTheTicket();

            result.Expiration.ShouldEqual(expectedExpirationDate);
        }

        [Test]
        public void The_ticket_is_persistent_when_ticket_data_states_so()
        {
            testPrincipalProvider.TicketDataToReturn.IsPersistent = true;

            var result = CreateTheTicket();

            result.IsPersistent.ShouldBeTrue();
        }

        [Test]
        public void The_ticket_is_not_persistent_when_ticket_data_states_so()
        {
            testPrincipalProvider.TicketDataToReturn.IsPersistent = false;

            var result = CreateTheTicket();

            result.IsPersistent.ShouldBeFalse();
        }

        [Test]
        public void The_name_on_the_ticket_should_be_set_according_to_the_ticket_data()
        {
            testPrincipalProvider.TicketDataToReturn.Username = "expected";

            var creator = GetTheTicketCreator();
            var principal = new GenericPrincipal(new GenericIdentity("not expected"), new string[] { });
            var result = creator.CreateFormsAuthenticationTicket(principal, typeof(TestPrincipalProvider));

            result.Name.ShouldEqual("expected");
        }

        [Test]
        public void The_user_data_on_the_ticket_should_be_set_to_the_type_followed_by_pipe_followed_by_the_data_from_the_principal_provider()
        {
            var principalProviderType = typeof(TestPrincipalProvider);

            testPrincipalProvider.TicketDataToReturn.UserData = "expected";

            var creator = GetTheTicketCreator();
            var result = creator.CreateFormsAuthenticationTicket(new GenericPrincipal(new GenericIdentity(""), new string[] {}), principalProviderType);

            result.UserData.ShouldEqual(string.Format("{0}, {1}|{2}", 
                principalProviderType.FullName, 
                principalProviderType.Assembly.FullName,
                "expected"));
        }

        [Test]
        public void The_cookie_path_should_be_set_to_the_forms_authentication_cookie_path()
        {
            var result = CreateTheTicket();

            result.CookiePath.ShouldEqual(FormsAuthentication.FormsCookiePath);
        }

        private FormsAuthenticationTicketCreator GetTheTicketCreator()
        {
            var serviceLocator = new TestServiceLocator();

            serviceLocator.ResolveThisInstanceAsThisType(testPrincipalProvider);

            return new FormsAuthenticationTicketCreator(serviceLocator);
        }

        private FormsAuthenticationTicket CreateTheTicket()
        {
            var creator = GetTheTicketCreator();
            return
                creator.CreateFormsAuthenticationTicket(new GenericPrincipal(new GenericIdentity(""), new string[] {}), typeof(TestPrincipalProvider));
        }

        private class TestPrincipalProvider : IPrincipalProvider
        {
            public PrincipalProviderResult GetPrincipal(string userId, string userData)
            {
                throw new NotImplementedException();
            }

            public IPrincipal CreatePrincipalFromTicketData(string userName, string userData)
            {
                throw new NotImplementedException();
            }

            public TicketData ConvertPrincipalToTicketData(IPrincipal principal)
            {
                return TicketDataToReturn;
            }

            public TicketData TicketDataToReturn { get; set; }
        }

        #region test service locator
        private class TestServiceLocator : IServiceLocator
        {
            private IDictionary<Type, object> thingsToResolve = new Dictionary<Type, object>();
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
                return thingsToResolve[typeof(T)] as T;
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