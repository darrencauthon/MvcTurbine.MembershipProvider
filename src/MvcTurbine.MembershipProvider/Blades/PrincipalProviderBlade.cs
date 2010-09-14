using MvcTurbine.Blades;
using MvcTurbine.ComponentModel;

namespace MvcTurbine.MembershipProvider.Blades
{
    public class PrincipalProviderBlade : Blade, ISupportAutoRegistration
    {
        public override void Spin(IRotorContext context)
        {
            // nothing
        }

        public void AddRegistrations(AutoRegistrationList registrationList)
        {
            registrationList.Add(ComponentModel.Registration.Simple<IPrincipalProvider>());
        }
    }
}