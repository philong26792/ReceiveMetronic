using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using Receive_API._Repositorys.Interfaces;
using Receive_API._Services.Interfaces;
using Receive_API.Dto;
using Receive_API.Helpers;
using Receive_API.Models;

namespace Receive_API._Services.Services
{
    public class ManagerService : IManagerService
    {
        private readonly IReceiveRepository _repoReceive;
        private readonly IProductRepository _repoProduct;
        private readonly IDepartmentRepository _repoDepartment;
        public ManagerService(  IReceiveRepository repoReceive,
                                IProductRepository repoProduct,
                                IDepartmentRepository repoDepartment) {
            _repoReceive = repoReceive;
            _repoProduct = repoProduct;
            _repoDepartment = repoDepartment;
        }

        public async Task<bool> AcceptReceive(string receiveID)
        {
            var receiveModel = await _repoReceive.GetAll().Where(x => x.ID.Trim() == receiveID.Trim()).FirstOrDefaultAsync();
            if(receiveModel != null) {
                receiveModel.Status = "2";
                receiveModel.Updated_Time = DateTime.Now;
                return await _repoReceive.SaveAll();
            } else {
                return false;
            }
        }

        public async Task<bool> DecliceReceive(string receiveID)
        {
            var receiveModel = await _repoReceive.GetAll().Where(x => x.ID.Trim() == receiveID.Trim()).FirstOrDefaultAsync();
            if(receiveModel != null) {
                _repoReceive.Remove(receiveModel);
                return await _repoReceive.SaveAll();
            } else {
                return false;
            }
        }

        public async Task<ReceiveInformationModel> GetReceive(string receiveID)
        {
            var receiveModel = await _repoReceive.GetAll()
                    .Where(x => x.ID.Trim() == receiveID.Trim()).FirstOrDefaultAsync();
            if(receiveModel == null) {
                return null;
            } else {
                var product = await _repoProduct.GetAll().Where(x => x.ID.Trim() == receiveModel.ProductID.Trim()).FirstOrDefaultAsync();
                var department = await _repoDepartment.GetAll().Where(x => x.ID.Trim() == receiveModel.DepID.Trim()).FirstOrDefaultAsync();
                var receiveResult = new ReceiveInformationModel();
                receiveResult.ID = receiveModel.ID;
                receiveResult.UserID = receiveModel.UserID;
                receiveResult.Accept_ID = receiveModel.Accept_ID;
                receiveResult.DepID = receiveModel.DepID;
                receiveResult.DepName = department.Name_LL;
                receiveResult.ProductID = receiveModel.ProductID;
                receiveResult.ProductName = product.Name;
                receiveResult.Qty = receiveModel.Qty;
                receiveResult.Register_Date = receiveModel.Register_Date;
                receiveResult.Accept_Date = receiveModel.Accept_Date;
                return receiveResult;
            }
        }

        public async Task<PagedList<ReceiveInformationModel>> GetWithPaginations(PaginationParams param)
        {
            var products = await _repoProduct.GetAll().ToListAsync();
            var receives = await _repoReceive.GetAll().Where(x => x.Status == "1").ToListAsync();
            var departments = await _repoDepartment.GetAll().ToListAsync();
            var data = (from r in receives join p in products
                on r.ProductID.Trim() equals p.ID.Trim()
                join d in departments on r.DepID.Trim() equals d.ID.Trim()
                select new ReceiveInformationModel() {
                    ID = r.ID,
                    UserID = r.UserID,
                    Accept_ID = r.Accept_ID,
                    DepID = r.DepID,
                    DepName = d.Name_LL,
                    ProductID = r.ProductID,
                    ProductName = p.Name,
                    Qty = r.Qty,
                    Register_Date = r.Register_Date,
                    Accept_Date = r.Accept_Date,
                    Updated_Time = r.Updated_Time,
                    Updated_By = r.Updated_By
                }).OrderByDescending(x => x.Register_Date).ToList();
            return PagedList<ReceiveInformationModel>.Create(data, param.PageNumber, param.PageSize);
        }

        public async Task<bool> ImportExcel(string filePath, string user)
        {

            using(var package = new ExcelPackage(new FileInfo(filePath))) {
                ExcelWorksheet workSheet = package.Workbook.Worksheets[0];
                for(int i = workSheet.Dimension.Start.Row + 1; i <= workSheet.Dimension.End.Row; i ++) {
                    var id = workSheet.Cells[i,1].Value.ToString();
                    if(!(await this.CheckDept(id))) {
                        Department department = new Department();
                        department.Status = "1";
                        department.Updated_Time = DateTime.Now;
                        department.Updated_By = user;
                        department.ID = workSheet.Cells[i,1].Value.ToString();
                        department.Name_ZW =  workSheet.Cells[i,2].Value == null? "": workSheet.Cells[i,2].Value.ToString();
                        department.Name_LL =  workSheet.Cells[i,3].Value == null? "": workSheet.Cells[i,3].Value.ToString();
                        department.Name_EN =  workSheet.Cells[i,4].Value == null? "" : workSheet.Cells[i,4].Value.ToString();
                        _repoDepartment.Add(department);
                    }
                }
                try {
                    await _repoDepartment.SaveAll();
                    return true;
                }
                catch(System.Exception) {
                    return false;
                    throw;
                }
            }
        }
        public async Task<bool> CheckDept(string id) {
            var dept = await _repoDepartment.GetAll().Where(x => x.ID.Trim() == id.Trim()).FirstOrDefaultAsync();
            if(dept != null) {
                return true;
            } else {
                return false;
            }
        }
    }
}