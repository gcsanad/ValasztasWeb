﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VálasztásWebApp.Models;

namespace VálasztásWebApp.Pages
{
    public class PartokListaModel : PageModel
    {
        private readonly VálasztásWebApp.Models.ValasztasDbContext _context;

        public PartokListaModel(VálasztásWebApp.Models.ValasztasDbContext context)
        {
            _context = context;
        }

        public IList<Part> Part { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Part = await _context.Partok.ToListAsync();
        }
    }
}
