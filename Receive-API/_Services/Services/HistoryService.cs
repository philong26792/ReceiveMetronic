using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Receive_API._Repositorys.Interfaces;
using Receive_API._Services.Interfaces;
using Receive_API.Dto;
using Receive_API.Helpers;

namespace Receive_API._Services.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly IReceiveRepository _repoReceive;
        private readonly IDepartmentRepository _repoDepartmane;
        private readonly IProductRepository _repoProduct;
        private readonly IUserRepository _repoUser;
        public HistoryService(  IReceiveRepository repoReceive,
                                IDepartmentRepository repoDepartmane,
                                IProductRepository repoProduct,
                                IUserRepository repoUser) {
            _repoReceive = repoReceive;
            _repoDepartmane = repoDepartmane;
            _repoProduct = repoProduct;
            _repoUser = repoUser;
        }

        public async Task<PagedList<ReceiveInformationModel>> GetWithPaginations(PaginationParams param, string user)
        {
            var receives = await _repoReceive.GetAll().Where(x => x.Status.Trim() == "2").ToListAsync();
            var products = await _repoProduct.GetAll().ToListAsync();
            var users = await _repoUser.GetAll().ToListAsync();
            var userLogin = users.Where(x => x.ID.Trim() == user.Trim()).FirstOrDefault();
            if(userLogin.RoleID == 1) {
            
            } else if (userLogin.RoleID == 2) {
                receives = receives.Where(x => x.DepID.Trim() == userLogin.DepID.Trim()).ToList();
            } else if(userLogin.RoleID == 3) {
                receives = receives.Where(x => x.UserID.Trim() == user).ToList();
            }
            var data = (from a in receives join b in products on a.ProductID.Trim() equals b.ID.Trim()
                select new ReceiveInformationModel() {
                    ID = a.ID,
                    UserID = a.UserID,
                    Accept_ID = a.Accept_ID,
                    DepID = a.DepID,
                    ProductID = a.ProductID,
                    ProductName = b.Name,
                    Qty = a.Qty,
                    Register_Date = a.Register_Date,
                    Accept_Date = a.Accept_Date,
                    Updated_By = a.Updated_By,
                    Status = a.Status,
                    Updated_Time = a.Updated_Time
                }).OrderByDescending(x => x.Register_Date).ToList();
            return PagedList<ReceiveInformationModel>.Create(data, param.PageNumber, param.PageSize);
        }

        public async Task<PagedList<ReceiveInformationModel>> SearchWithPaginations(PaginationParams param, string userLogin, HistoryParam filterParam)
        {
            var receives = await _repoReceive.GetAll().Where(x => x.Status.Trim() == "2").ToListAsync();
            var products = await _repoProduct.GetAll().ToListAsync();
            var users = await _repoUser.GetAll().ToListAsync();
            var userCurrent = users.Where(x => x.ID.Trim() == userLogin.Trim()).FirstOrDefault();
            if(userCurrent.RoleID == 1) {
            
            } else if (userCurrent.RoleID == 2) {
                receives = receives.Where(x => x.DepID.Trim() == userCurrent.DepID.Trim()).ToList();
            } else if(userCurrent.RoleID == 3) {
                receives = receives.Where(x => x.UserID.Trim() == userLogin).ToList();
            }

            if(filterParam.UserID != null && filterParam.UserID != string.Empty) {
                receives = receives.Where(x => x.UserID.Trim() == filterParam.UserID.Trim()).ToList();
            }
            if(filterParam.From_Date != "" && filterParam.To_Date != "") {
                receives = receives.Where(x => x.Register_Date >= Convert.ToDateTime(filterParam.From_Date + " 00:00:00.000") &&
                x.Register_Date <= Convert.ToDateTime(filterParam.To_Date + " 23:59:59.997")).ToList();
            }

            var data = (from a in receives join b in products on a.ProductID.Trim() equals b.ID.Trim()
                select new ReceiveInformationModel() {
                    ID = a.ID,
                    UserID = a.UserID,
                    Accept_ID = a.Accept_ID,
                    DepID = a.DepID,
                    ProductID = a.ProductID,
                    ProductName = b.Name,
                    Qty = a.Qty,
                    Register_Date = a.Register_Date,
                    Accept_Date = a.Accept_Date,
                    Updated_By = a.Updated_By,
                    Status = a.Status,
                    Updated_Time = a.Updated_Time
                }).OrderByDescending(x => x.Register_Date).ToList();
            return PagedList<ReceiveInformationModel>.Create(data, param.PageNumber, param.PageSize);
        }
    }
}