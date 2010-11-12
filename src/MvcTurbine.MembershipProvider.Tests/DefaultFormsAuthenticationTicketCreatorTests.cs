using System;
using System.Security.Principal;
using System.Web.Security;
using AutoMoq;
using MvcTurbine.MembershipProvider.Helpers;
using NUnit.Framework;
using Should;

namespace MvcTurbine.MembershipProvider.Tests
{
    [TestFixture]
    public class DefaultFormsAuthenticationTicketCreatorTests
    {
        private AutoMoqer mocker;

        [SetUp]
        public void Setup()
        {
            mocker = new AutoMoqer();
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
        public void Adds_2880_minutes_to_the_current_date_as_the_expiration_date()
        {
            CurrentDateTime.SetNow(new DateTime(2010, 12, 25, 1, 2, 3));
            var expectedExpirationDate = new DateTime(2010, 12, 25, 1, 2, 3).AddMinutes(2880);

            var result = CreateTheTicket();

            result.Expiration.ShouldEqual(expectedExpirationDate);
        }

        [Test]
        public void The_ticket_is_persistent()
        {
            var result = CreateTheTicket();

            result.IsPersistent.ShouldBeTrue();
        }

        [Test]
        public void The_username_on_the_ticket_should_be_set_to_the_name_on_the_identity()
        {
            var creator = GetTheTicketCreator();
            var principal = new GenericPrincipal(new GenericIdentity("expected"), new string[] {});
            var result = creator.CreateFormsAuthenticationTicket(principal);

            result.Name.ShouldEqual("expected");
        }

        [Test]
        public void The_user_data_on_the_ticket_should_be_set_to_an_empty_string()
        {
            var result = CreateTheTicket();

            result.UserData.ShouldEqual("");
        }

        [Test]
        public void The_cookie_path_should_be_set_to_the_forms_authentication_cookie_path()
        {
            var result = CreateTheTicket();

            result.CookiePath.ShouldEqual(FormsAuthentication.FormsCookiePath);
        }

        private DefaultFormsAuthenticationTicketCreator GetTheTicketCreator()
        {
            return mocker.Resolve<DefaultFormsAuthenticationTicketCreator>();
        }

        private FormsAuthenticationTicket CreateTheTicket()
        {
            var creator = GetTheTicketCreator();
            return
                creator.CreateFormsAuthenticationTicket(new GenericPrincipal(new GenericIdentity(""), new string[] {}));
        }
    }
}