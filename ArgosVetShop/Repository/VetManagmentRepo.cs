using ArgosVetShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ArgosVetShop.Repository
{
    public class VetManagmentRepo
    {
        private ApplicationDbContext _dbContext;
        private ApplicationUserManager _userManager;
        
        public VetManagmentRepo(ApplicationDbContext dbContext, ApplicationUserManager userManager)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }
            if (userManager == null)
            {
                throw new ArgumentNullException("userManager");
            }
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<List<AppointmentModel>> GetAllAppointments()
        {
            return await _dbContext.Appointments.ToListAsync();
        }

        public async Task<string> GetOwnerNameById(string ownerId)
        {
            var owner = await _userManager.FindByIdAsync(ownerId);
            return owner.UserName;
        }

        public async Task<AppointmentModel> GetAppointmentById(int appointmentId)
        {
            return await _dbContext.Appointments.Where(a => a.AppoinmentID == appointmentId).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAppointment(AppointmentModel originalModel)
        {
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }
    }
}