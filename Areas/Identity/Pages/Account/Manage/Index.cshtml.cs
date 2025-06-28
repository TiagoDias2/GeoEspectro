using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GeoEspectro.Data;
using Microsoft.EntityFrameworkCore;
using GeoEspectro.Models;

namespace GeoEspectro.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<IndexModel> logger,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = "Nome")]
            public string Nome { get; set; }

            [Display(Name = "Morada")]
            public string Morada { get; set; }

            [Display(Name = "Código Postal")]
            public string CodPostal { get; set; }

            [Display(Name = "País")]
            public string Pais { get; set; }

            [Display(Name = "NIF")]
            public string Nif { get; set; }

            [Display(Name = "Telemóvel")]
            public string Telemovel { get; set; }

        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            var utilizador = await _context.Utilizadores
                .FirstOrDefaultAsync(u => u.IdentityUserId == user.Id);

            Username = userName;

            Input = new InputModel
            {
                Nome = utilizador?.Nome,
                Morada = utilizador?.Morada,
                CodPostal = utilizador?.CodPostal,
                Pais = utilizador?.Pais,
                Nif = utilizador?.Nif,
                Telemovel = utilizador?.Telemovel ?? phoneNumber // fallback se não houver
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                foreach (var entry in ModelState)
                {
                    foreach (var error in entry.Value.Errors)
                    {
                        _logger.LogError($"Erro em {entry.Key}: {error.ErrorMessage}");
                    }
                }

                await LoadAsync(user);
                return Page();
            }


            var utilizador = await _context.Utilizadores
                .FirstOrDefaultAsync(u => u.IdentityUserId == user.Id);

            if (utilizador == null)
            {
                utilizador = new Utilizadores
                {
                    IdentityUserId = user.Id,
                    Nome = Input.Nome,
                    Morada = Input.Morada,
                    CodPostal = Input.CodPostal,
                    Pais = Input.Pais,
                    Nif = Input.Nif,
                    Telemovel = Input.Telemovel,
                    UserName = user.UserName
                };
                _context.Utilizadores.Add(utilizador);
            }
            else
            {
                utilizador.Nome = Input.Nome;
                utilizador.Morada = Input.Morada;
                utilizador.CodPostal = Input.CodPostal;
                utilizador.Pais = Input.Pais;
                utilizador.Nif = Input.Nif;
                utilizador.Telemovel = Input.Telemovel;
                _context.Update(utilizador);
            }

            await _context.SaveChangesAsync();

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "O seu perfil foi atualizado com sucesso.";
            return RedirectToPage();
        }
    }
}
