using System;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace NHibernate.AspNetCore.Identity {

    public static class IdentityResultExtensions {

        public static string GetErrorsString(this IdentityResult result) {
            if (result == null) {
                throw new ArgumentNullException(nameof(result));
            }
            if (result.Succeeded) {
                return string.Empty;
            }
            var err = new StringBuilder();
            foreach (var error in result.Errors) {
                err.AppendLine($"{error.Code}:{error.Description}");
            }
            return err.ToString();
        }
    }

}
