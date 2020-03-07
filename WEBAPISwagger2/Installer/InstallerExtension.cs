using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPISwagger2.Installer
{
    public static class InstallerExtension
    {
        public static void InstallServicesAssembly (this IServiceCollection services, IConfiguration Configuration)
        {
            var AllClassesImplementedfromIINTERFACE = typeof(Startup).Assembly.ExportedTypes.Where(x => typeof(IInstaller).IsAssignableFrom(x)
             && !x.IsAbstract && !x.IsInterface).Select(Activator.CreateInstance).Cast<IInstaller>().ToList();
            AllClassesImplementedfromIINTERFACE.ForEach(installer => installer.InstallServices(services, Configuration));



        }
    }
}
