using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
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
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        }


        [BindProperty]
        public IFormFile UploadFile { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            
            var UploadFilePath = Path.Combine(_env.ContentRootPath,"Uploads", UploadFile.FileName);
            using (var stream = new FileStream(UploadFilePath, FileMode.Create))
            {
                await UploadFile.CopyToAsync(stream);
            }

            StreamReader sr = new StreamReader(UploadFilePath);

            while (!sr.EndOfStream)
            {
                var sor = sr.ReadLine();
                var part = sor?.Split()[4];
                if (!_context.Partok.Select(x => x.RovidNev).Contains(part))
                {
                    Part ujPart = new Part();
                    ujPart.RovidNev = part;
                    _context.Partok.Add(ujPart);

                }



            }
            sr.Close();
            _context.SaveChanges();

            sr=new(UploadFilePath);
            while (!sr.EndOfStream)
            {
                var sor = sr.ReadLine();
                var elemek = sor?.Split();
                Jelolt ujJelolt = new Jelolt();
                
                ujJelolt.Kerulet = int.Parse(elemek[0]);
                ujJelolt.SzavazatokSzama = int.Parse(elemek[1]);
                ujJelolt.Nev = elemek[2] + " " + elemek[3];
                
                ujJelolt.PartRovidNev = elemek[4];
                _context.JeloltekListaja.Add(ujJelolt);
                


            }

                sr.Close();
            _context.SaveChanges();
                return Page();
        }
    }
}
