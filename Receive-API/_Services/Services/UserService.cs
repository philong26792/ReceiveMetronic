using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Receive_API._Repositorys.Interfaces;
using Receive_API._Services.Interfaces;
using Receive_API.Dto;
using Receive_API.Helpers;
using Receive_API.Models;

namespace Receive_API._Services.Services
{
    
    public class UserService : IUserService
    {
        
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        private readonly IUserRepository _repoUser;
        private readonly IRoleRepository _repoRole;
        private readonly IDepartmentRepository _repoDepartment;
        public UserService( IMapper mapper,
                            MapperConfiguration configMapper,
                            IUserRepository repoUser,
                            IRoleRepository repoRole,
                            IDepartmentRepository repoDepartment) {
            _mapper = mapper;
            _configMapper = configMapper;
            _repoUser = repoUser;
            _repoRole = repoRole;
            _repoDepartment = repoDepartment;
        }
        public async Task<bool> Add(User_Dto model)
        {
            model.Update_Time = DateTime.Now;
            var user = _mapper.Map<User>(model);
            _repoUser.Add(user);
            return await _repoUser.SaveAll();
        }

        public async Task<bool> CheckExistUser(string userID)
        {
            var userFind = await _repoUser.GetAll().FirstOrDefaultAsync(x => x.ID == userID);
            if(userFind != null)
                return true;
            return false;
        }

        public async Task<bool> Delete(string UserId)
        {
            var user = await _repoUser.FindAll()
                .Where(x => x.ID == UserId).FirstOrDefaultAsync();
            if(user != null) {
                _repoUser.Remove(user);
                return await _repoUser.SaveAll();
            } else {
                return false;
            }
        }

        public async Task<List<Department>> GetAllDepartment()
        {
            var departments = await _repoDepartment.GetAll().ToListAsync();
            return departments;
        }

        public async Task<List<Role>> GetAllRole()
        {
            var roles = await _repoRole.GetAll().ToListAsync();
            return roles;
        }

        public async Task<User> GetUserById(string userId)
        {
            var user = await _repoUser.GetAll().Where(x => x.ID == userId).FirstOrDefaultAsync();
            return user;
        }

        public async Task<PagedList<UserViewModel>> GetWithPaginations(PaginationParams param)
        {
            var lists =  _repoUser.GetAll().ProjectTo<User_Dto>(_configMapper)
                .OrderByDescending(x => x.DepID);
            var roles =  _repoRole.GetAll();
            var departments = _repoDepartment.GetAll();
            var users = (from a in lists join b in roles
                on a.RoleID equals b.ID
                join c in departments on a.DepID equals c.ID
                select new UserViewModel{
                    ID = a.ID,
                    Password = a.Password,
                    Name = a.Name,
                    RoleID = a.RoleID,
                    RoleName = b.Name,
                    DepID = a.DepID,
                    Department_Name = c.Name_LL,
                    Update_By = a.Update_By,
                    Update_Time = a.Update_Time
                });
            return await PagedList<UserViewModel>.CreateAsync(users, param.PageNumber, param.PageSize);
        }

        public async Task<bool> Update(User_Dto model)
        {
            model.Update_Time = DateTime.Now;
            var user = _mapper.Map<User>(model);
            user.Update_Time = DateTime.Now;
            _repoUser.Update(user);
            return await _repoUser.SaveAll();
        }
    }
}