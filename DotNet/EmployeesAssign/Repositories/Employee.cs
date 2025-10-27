using EmployeesAssign.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeesAssign.Repositories
{
    // Renamed class to EmployeeRepository to avoid confusion with Models.Employee
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDbContext _dbContext;

        public EmployeeRepository(EmployeeDbContext employeeDbContext)
        {
            _dbContext = employeeDbContext;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _dbContext.Employees.ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _dbContext.Employees.FindAsync(id);
        }

        public async Task AddAsync([FromBody] Employee employee)
        {
            _dbContext.Employees.Add(employee);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync([FromBody] Employee employee)
        {
            _dbContext.Employees.Update(employee);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var p = await _dbContext.Employees.FindAsync(id);
            if (p != null)
            {
                _dbContext.Employees.Remove(p);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
