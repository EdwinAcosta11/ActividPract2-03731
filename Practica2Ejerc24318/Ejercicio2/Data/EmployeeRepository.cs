using ApiLibros.Models;

namespace ApiLibros.Data
{
    public static class EmployeeRepository
    {
        private static List<Employee> empleados = new List<Employee>();
        private static int nextId = 1;

        public static List<Employee> GetAll() => empleados;

        public static Employee? GetById(int id) => empleados.FirstOrDefault(e => e.Id == id);

        public static void Add(Employee empleado)
        {
            empleado.Id = nextId++;
            empleados.Add(empleado);
        }

        public static bool Update(int id, Employee actualizado)
        {
            var empleado = GetById(id);
            if (empleado == null) return false;

            empleado.Nombre = actualizado.Nombre;
            empleado.Puesto = actualizado.Puesto;
            empleado.Salario = actualizado.Salario;
            return true;
        }

        public static bool Delete(int id)
        {
            var empleado = GetById(id);
            if (empleado == null) return false;

            empleados.Remove(empleado);
            return true;
        }
    }
}
