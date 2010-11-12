using System;

namespace MvcTurbine.MembershipProvider.Helpers
{
    public static class CurrentDateTime
    {
        private static DateTime? now;

        public static DateTime Now
        {
            get { return now ?? DateTime.Now; }
        }

        public static void SetNow(DateTime now)
        {
            CurrentDateTime.now = now;
        }
    }
}