using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace EscritorCache.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {

        private readonly IDistributedCache _distributedCache;

        public ValuesController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        // GET api/values
        [HttpGet("{clave}")]
        public async Task<string> Get(string clave)
        {
            var valorGuardado = _distributedCache.GetString(clave);

            if (!string.IsNullOrEmpty(valorGuardado))
            {
                return "Se obtiene de cache : " + valorGuardado;
            }

            return "No se encontraron datos en la cache";
        }


        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]CacheObject cacheobj)
        {
            _distributedCache.SetString(cacheobj.clave, cacheobj.valor);
            return Json(cacheobj);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
