// Configuração inicial da aplicação web
using GeoEspectro.Data;
using GeoEspectro.Services;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços ao container DI (Dependency Injection)

// Configuração da ligação à base de dados SQL Server
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Configuração do Identity (autenticação/autorização) com confirmação de conta obrigatória
// Adiciona suporte a roles (perfis de utilizador)
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

// Configuração para permitir uploads de ficheiros até 20MB
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 20 * 1024 * 1024; //20MB
});

// Configuração dos controllers para ignorar referências circulares no JSON
builder.Services.AddControllers()
                .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Configuração do JWT (JSON Web Tokens) para autenticação
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

// Configuração dos esquemas de autenticação:
// - Cookies para autenticação tradicional
// - JWT Bearer para autenticação API
builder.Services.AddAuthentication(options => { })
   .AddCookie("Cookies", options => {
       options.LoginPath = "/Identity/Account/Login";
       options.AccessDeniedPath = "/Identity/Account/AccessDenied";
   })
   .AddJwtBearer("Bearer", options => {
       options.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuer = true,
           ValidateAudience = true,
           ValidateLifetime = true,
           ValidateIssuerSigningKey = true,
           ValidIssuer = jwtSettings["Issuer"],
           ValidAudience = jwtSettings["Audience"],
           IssuerSigningKey = new SymmetricSecurityKey(key)
       };
   });

// Configuração do SignalR para comunicação em tempo real
builder.Services.AddSignalR();
// Registo do serviço para geração de tokens
builder.Services.AddScoped<TokenService>();

// Configuração de políticas de autorização
// Exemplo: política "AdminPolicy" que requer a claim "IsAdmin" com valor "true"
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
        policy.RequireClaim("IsAdmin", "true"));
});

// Configuração do Swagger/OpenAPI para documentação da API
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Minha API de gestão de Artigos de Investigação",
        Version = "v1",
        Description = "API para gestão de categorias, recursos multimédia associados aos artigos publicados"
    });

    // Incluir comentários XML na documentação
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

// Construir a aplicação
var app = builder.Build();

// Configurar o pipeline de pedidos HTTP

// Em desenvolvimento:
// - Usar páginas de erro de migração
// - Ativar Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    // Em produção:
    // - Usar página de erro personalizada
    // - Forçar HTTPS (HTTP Strict Transport Security)
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Configuração inicial das roles (perfis) e utilizador admin
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        // Roles a criar
        string[] roleNames = { "Admin", "User", "Editor" };

        // Criar cada role se não existir
        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        // Criar um utilizador admin por defeito (opcional)
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        string adminEmail = "admin@example.com";
        string adminPassword = "Admin@123";

        if (await userManager.FindByEmailAsync(adminEmail) == null)
        {
            var adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };

            var createUser = await userManager.CreateAsync(adminUser, adminPassword);
            if (createUser.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocorreu um erro ao criar as roles ou usuário admin");
    }
}

// Configurar o middleware
app.UseHttpsRedirection();      // Redirecionar HTTP para HTTPS
app.UseStaticFiles();          // Servir ficheiros estáticos
app.UseRouting();              // Roteamento
app.UseAuthentication();       // Autenticação
app.UseAuthorization();        // Autorização

// Mapear o hub do SignalR
app.MapHub<GostosHub>("/gostoshub");

// Mapear as rotas MVC e Razor Pages
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

// Iniciar a aplicação
app.Run();