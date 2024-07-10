using E_Commerce_BW4_Team4.Services;

namespace E_Commerce_BW4_Team4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.
            builder.Services
                .AddScoped<IProdottoService, ProdottoService>()
                .AddScoped<IGeneriService, GeneriService>()
                .AddScoped<IPiattaformaService, PiattaformaService>()
                .AddScoped<IOrdiniService, OrdiniService>()
                .AddControllersWithViews();
                

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
