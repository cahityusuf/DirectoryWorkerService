using System;
using System.Collections.Generic;
using System.Reflection;

namespace Data.Helpers
{
    public class ApiOptions
    {
        /// <summary>
        /// FluentValidation deklerasyonlarinin aranacagi assembly'ler
        /// </summary>
        public IEnumerable<Assembly> RegistrationAssemblies { get; set; }

        public IEnumerable<Type> HubList { get; set; }
        public IEnumerable<Type> ApiClientList { get; set; }

        public string ApiName { get; set; }

        ///// <summary>
        ///// AutoMapper Profilleri için
        ///// </summary>
        //public IEnumerable<Profile> MappingProfiles { get; set; }
    }
}
