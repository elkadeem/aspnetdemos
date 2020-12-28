using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using WebAPIDemo.Entities;

namespace WebAPIDemo.Controllers
{
    public class CustomersController : ApiController
    {
        private readonly EmployeesDbContext _dbContext;
        public CustomersController()
        {
            _dbContext = new EmployeesDbContext();
        }
        // GET: api/Customers
        public  IEnumerable<Customer> Get()
        {
            return _dbContext.Customers.ToList();
        }

        // GET: api/Customers/5
        public async Task<IHttpActionResult> Get(int id)
        {
            var item = await _dbContext.Customers.FindAsync(id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpGet]
        public IHttpActionResult Get(string name, int pageIndex)
        {
            var query = _dbContext.Customers.AsQueryable();
            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(c => c.Name.Contains(name));

            var totalItems = query.Count();
            var items = query.OrderBy(c => c.Name).Skip(pageIndex * 10)
                .Take(10).ToList();

            return Ok(new CustomersDto { TotalItemsCount = totalItems, Customers = items });

        }

        // POST: api/Customers
        public async Task<IHttpActionResult> Post([FromBody] CreateCustomerDto customer)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            Customer newCustomer = new Customer { Age = customer.Age, Name = customer.Name };
            _dbContext.Customers.Add(newCustomer);
            await _dbContext.SaveChangesAsync();
            return Created($"api/customers/{newCustomer.Id}", customer);
        }

        // PUT: api/Customers/5
        public async Task<IHttpActionResult> Put(int id, [FromBody]UpdateCustomerDto customer)
        {
            if (!ModelState.IsValid || id != customer.Id)
                return BadRequest();

            Customer currentCustomer = await _dbContext.Customers.FindAsync(id);
            if (currentCustomer == null)
                return NotFound();

            currentCustomer.Name = customer.Name;
            currentCustomer.Age = customer.Age;

            await _dbContext.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/Customers/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            Customer currentCustomer = await _dbContext.Customers.FindAsync(id);
            if (currentCustomer == null)
                return NotFound();

            _dbContext.Customers.Remove(currentCustomer);
            await _dbContext.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
    }

    public class CreateCustomerDto
    {
        [Required]
        public string Name { get; set; }


        [Required]
        [Range(10, 60)]
        public int Age { get; set; }
    
    }

    public class UpdateCustomerDto
    {       
        
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }


        [Required]
        [Range(10, 60)]
        public int Age { get; set; }

    }

    public class CustomersDto
    {
        public IEnumerable<Customer> Customers { get; set; }

        public int TotalItemsCount { get; set; }
    }
}
