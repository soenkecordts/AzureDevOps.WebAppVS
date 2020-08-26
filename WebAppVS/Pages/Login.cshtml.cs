using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Threading.Tasks;


namespace WebAppVS.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "E-Mail ist Pflichteingabe"), DataType(DataType.EmailAddress, ErrorMessage = "Korrekte E-Mail-Adresse eingeben")]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Kennwort ist Pflichteingabe"), StringLength(20, ErrorMessage = "Kennwort mit maximaler Länge von 20"), DataType(DataType.Password)]
        public string Password { get; set; }

        readonly string connectionString;

        public LoginModel(AppConfig appconfig)
        {
            connectionString = appconfig.ConnectionString;
        }

        public IActionResult OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            bool isCorrect = await ExistsUser(Email, Password);

            if (!ModelState.IsValid || !isCorrect)
            {
                ModelState.AddModelError("", "Ungültige Anmeldung");
                return Page();
            }
            return RedirectToPage("/Index");
        }

        public async Task<bool> ExistsUser(string email, string password)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                var cmd = new SqlCommand("SELECT COUNT(*) Anzahl FROM [User] WHERE Email=@email AND Password=@password", con);
                cmd.Parameters.AddWithValue("@email", Email);
                cmd.Parameters.AddWithValue("@password", Password);
                bool isCorrect = (int)await cmd.ExecuteScalarAsync() == 1;
                return isCorrect;
            }
        }
    }
}