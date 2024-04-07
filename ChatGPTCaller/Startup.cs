using ChatGPTCaller.Services;
using ChatGPTCaller.Services.Admin;
using ChatGPTCaller.Services.SinhVien;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Threading.Tasks;

namespace ChatGPTCaller
{
	public class Startup
	{
        public IConfiguration Configuration { get; }
		public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
		{
            Configuration.GetConnectionString("userdb");
            services.AddRazorPages();
			services.AddSingleton<ChatGPTService>();
			services.AddSingleton<RegisterService>();
			services.AddSingleton<LoginService>();
			services.AddSingleton<GetService>();
            services.AddSingleton<UpdateService>();
			services.AddSingleton<DeleteService>();
			services.AddSingleton<AdminService>();
            services.AddSingleton<ConversationService>();
			services.AddControllers();
        }

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();
			app.UseStaticFiles();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapRazorPages();
				endpoints.MapControllers();
			});
		}
	}
}
