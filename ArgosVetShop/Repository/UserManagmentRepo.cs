using ArgosVetShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ArgosVetShop.Repository
{
    public class UserManagmentRepo
    {
        private ApplicationDbContext _dbContext;
        private ApplicationUserManager _userManager;
        
        public UserManagmentRepo(ApplicationDbContext dbContext,ApplicationUserManager userManager)
        {
            if(dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }
            if(userManager == null)
            {
                throw new ArgumentNullException("userManager");
            }
            _dbContext = dbContext;
            _userManager = userManager;
        }
        public async Task<ApplicationUser> GetUser(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<List<PetModel>>GetUserPets(string userId)
        {
            return await _dbContext.Pets.Where(p => p.OwnerId == userId).ToListAsync();            
        }


        public async Task<bool> AddPet(PetModel model)
        {
            _dbContext.Pets.Add(model);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<PetModel> GetPetById(int petId)
        {
            return await _dbContext.Pets.FindAsync(petId);
        }

        public async Task<bool> UpdatePetInformation(PetModel originalModel, PetModel modelToUpdate)
        {
            _dbContext.Entry(originalModel).CurrentValues.SetValues(modelToUpdate);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<List<AppointmentModel>> GetAppointmentsByDate(string date)
        {

            return await _dbContext.Appointments.Where(a => a.Date.Equals(date)).ToListAsync();
        }

        public async Task<bool> BookAppointment(AppointmentModel model)
        {
            _dbContext.Appointments.Add(model);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }
    }
}