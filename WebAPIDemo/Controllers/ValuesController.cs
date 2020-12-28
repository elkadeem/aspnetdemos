using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Http;

namespace WebAPIDemo.Controllers
{
    //api/values
    public class ValuesController : ApiController
    {
        private static Dictionary<int, string> _list = new Dictionary<int, string> {
            { 1, "Value 1"},
            { 2, "Value 2"},
        };
        public ValuesController()
        {

        }
        // GET api/values
        public IHttpActionResult Get()
        {
            return Ok(_list.Values);
        }

        // GET api/values/5
        public IHttpActionResult Get(int id)
        {
            if (_list.ContainsKey(id))
                return Ok(_list[id]);
            else
                return NotFound();
        }

        // POST api/values
        public IHttpActionResult Post([FromBody] string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return BadRequest();

            int id = _list.Keys.Max() + 1;
            _list.Add(id, value);            
            return Created($"api/values/{id}", value);
        }

        // PUT api/values/5
        public IHttpActionResult Put(int id, [FromBody] string value)
        {
            if (_list.ContainsKey(id))
            {
                _list[id] = value;
                return StatusCode(System.Net.HttpStatusCode.NoContent);
            }
            else
                return NotFound();
        }

        // DELETE api/values/5
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            if (_list.ContainsKey(id))
            {
                _list.Remove(id);
                return StatusCode(System.Net.HttpStatusCode.NoContent);
            }
            else
                return NotFound();
        }
    }

    public class ValueDto
    {
        [Required]
        public string Value { get; set; }
    }
}
