using Microsoft.AspNetCore.Mvc;

namespace AuditTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductService _provider;

        public ProductsController(IProductService provider)
        {
            _provider = provider;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Order>> Get()
        {
            return Ok(_provider.GetProducts());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> Get(int id)
        {
            return Ok(await _provider.GetAsync(id));
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<int>> Post()
        {



            return Ok(await _provider.InsertAsync());
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task Put()
        {
            await _provider.ReplaceAsync(1);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return Ok(await _provider.DeleteAsync(id));
        }

        // DELETE api/values/delete
        [HttpDelete]
        [Route("delete")]
        public async Task<ActionResult<bool>> Delete([FromBody] string ids)
        {
            return Ok(await _provider.DeleteMultipleAsync(ids.Split(',').Select(s => int.Parse(s)).ToArray()));
        }
    }
}
