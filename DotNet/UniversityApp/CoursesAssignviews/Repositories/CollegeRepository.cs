using CoursesAssignviews.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoursesAssignviews.Repositories;
namespace CoursesAssignviews.Repositories
{
    public class CollegeRepository<T>: ICollegeRepository<T> where T : class
    {
        private readonly CollegeDbContext _context;
        //private readonly IemailService _emailService;
        private DbSet<T> _dbSet;

        public CollegeRepository(CollegeDbContext context) { 
            _context = context;
            //_emailService = emailService;
            _dbSet = context.Set<T>();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T>  CreateAsync(T record)
        {
            _context.Add(record);
            await _context.SaveChangesAsync();
            return record;
        }

        public async Task<T> UpdateAsync(T record)
        {
            _context.Update(record);
            await _context.SaveChangesAsync();
            return record;
        }

        public async Task<bool> DeleteAsync(T entity)
        {


            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Course>> GetAllCourses()
        {
            return await _context.Courses.Include(b=> b.Students).ToListAsync();
        }

        //public string Generaterandomnumber()
        //{
        //    Random random = new Random();
        //    string randomnum = random.Next(0, 1000000).ToString("D6");
        //    return randomnum;
        //}

        //public async Task SendOtpMail(string Email, string otptext, string name)
        //{
        //    var mailrequest = new MailRequest();

        //    mailrequest.email = Email;
        //    mailrequest.subject = "thanks for resistering:OTP";
        //    mailrequest.body = Generatebody(name, otptext);
        //    this._emailService.SendEmail(mailrequest);


        //}

        //private string Generatebody(string name, string OTPtext)
        //{
        //    string emailbody = string.Empty;
        //    emailbody = "<div style='width= 100%; background-color:grey'>";
        //    emailbody += "<h1>Hi" + name + " , thanks for registering </h1>";
        //    emailbody += "<h2>Please enter the otp text and conplete the resitraition </h2>";
        //    emailbody += "<h2>OTP text is  " + OTPtext + "</h2>";
        //    emailbody = "</div>";

        //    return emailbody;

        //}
    }
}
