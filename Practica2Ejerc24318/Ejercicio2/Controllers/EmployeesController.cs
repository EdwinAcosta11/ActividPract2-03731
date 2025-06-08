using Microsoft.AspNetCore.Mvc;
using ApiLibros.Models;
using ApiLibros.Data;

namespace ApiLibros.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Employee>> GetAll() => EmployeeRepository.GetAll();

        [HttpGet("{id}")]
        public ActionResult<Employee> GetById(int id)
        {
            var empleado = EmployeeRepository.GetById(id);
            if (empleado == null) return NotFound();
            return empleado;
        }

        [HttpPost]
        public IActionResult Create(Employee empleado)
        {
            if (string.IsNullOrWhiteSpace(empleado.Nombre))
                return BadRequest("El nombre no puede estar vacío.");

            if (empleado.Salario < 0)
                return BadRequest("El salario no puede ser negativo.");

            EmployeeRepository.Add(empleado);
            return CreatedAtAction(nameof(GetById), new { id = empleado.Id }, empleado);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Employee empleado)
        {
            if (string.IsNullOrWhiteSpace(empleado.Nombre))
                return BadRequest("El nombre no puede estar vacío.");

            if (empleado.Salario < 0)
                return BadRequest("El salario no puede ser negativo.");

            bool actualizado = EmployeeRepository.Update(id, empleado);
            return actualizado ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool eliminado = EmployeeRepository.Delete(id);
            return eliminado ? NoContent() : NotFound();
        }
    }
}

