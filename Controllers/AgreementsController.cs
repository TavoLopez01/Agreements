using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agreements.Data;
using Agreements.Models;

namespace Agreements.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AgreementsController : ControllerBase
    {
        private readonly AgreementsData _agreementsData;

        public AgreementsController(AgreementsData agreementsData)
        {
            this._agreementsData = agreementsData ?? throw new ArgumentException(nameof(agreementsData));
        }

        [HttpGet]
        public async Task<List<AgreementsModel>> get()
        {
            return await _agreementsData.GetAll();
        }

       
    }
}
