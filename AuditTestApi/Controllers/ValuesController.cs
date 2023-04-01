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
        public ActionResult<IEnumerable<ProductEntity>> Get()
        {
            return Ok(_provider.GetProducts());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductEntity>> Get(int id)
        {
            return Ok(await _provider.GetAsync(id));
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] ProductEntity value)
        {
            return Ok(await _provider.InsertAsync(value));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] ProductEntity value)
        {
            await _provider.ReplaceAsync(id, value);
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
