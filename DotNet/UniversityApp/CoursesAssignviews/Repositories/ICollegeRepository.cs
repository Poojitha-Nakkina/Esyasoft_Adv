using CoursesAssignviews.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace CoursesAssignviews.Repositories
{
    public interface ICollegeRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<List<Course>> GetAllCourses();
        Task<T> GetAsync(int id);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        
    }

   

 
        
    
}
