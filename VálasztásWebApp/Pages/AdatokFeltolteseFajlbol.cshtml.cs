using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VálasztásWebApp.Models;

namespace VálasztásWebApp.Pages
{
    public class AdatokFeltolteseFajlbolModel : PageModel
    {
        public IWebHostEnvironment? _env { get; set; }
        public ValasztasDbContext _context { get; set; }

        public AdatokFeltolteseFajlbolModel(IWebHostEnvironment env, ValasztasDbContext context)
        {
            _context = context;
            _env = env;
        }


        [BindProperty]
        public IFormFile UploadFile { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var UploadDirPath = Path.Combine(_env.ContentRootPath, "Uploads");
            var UploadFilePath = Path.Combine(UploadDirPath, UploadFile.FileName);
            using (var stream = new FileStream(UploadFilePath, FileMode.Create))
            {
                await UploadFile.CopyToAsync(stream);
            }

            StreamReader sr = new StreamReader(UploadFilePath);

            while (!sr.EndOfStream)
            {
                var sor = sr.ReadLine();
                var elemek = sor.Split();
                Jelolt ujJelolt = new Jelolt();
                Part ujPart = new Part();
                ujJelolt.Kerulet = int.Parse(elemek[0]);
                ujJelolt.SzavazatokSzama = int.Parse(elemek[1]);
                ujJelolt.Nev = elemek[2] + " " + elemek[3];
                ujPart.RovidNev = elemek[4];
                ujJelolt.Part = ujPart;
                _context.JeloltekListaja.Add(ujJelolt);
                _context.Partok.Add(ujPart);


            }

                sr.Close();
            _context.SaveChanges();
                return Page();
        }
    }
}
