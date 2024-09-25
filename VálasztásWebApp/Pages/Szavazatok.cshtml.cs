using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VálasztásWebApp.Models;

namespace VálasztásWebApp.Pages
{
    public class SzavazatokModel : PageModel
    {
        private readonly VálasztásWebApp.Models.ValasztasDbContext _context;

        public SzavazatokModel(VálasztásWebApp.Models.ValasztasDbContext context)
        {
            _context = context;
        }

        public IList<Jelolt> Jelolt { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Jelolt = await _context.JeloltekListaja.ToListAsync();
        }
    }
}
