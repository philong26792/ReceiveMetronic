using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Receive_API._Repositorys.Interfaces;
using Receive_API._Services.Interfaces;
using Receive_API.Dto;
using Receive_API.Helpers;
using Receive_API.Models;

namespace Receive_API._Services.Services
{
    public class ReceiveService : IReceiveService
    {
        private readonly ICategoryRepository _repoCategory;
        private readonly IProductRepository _repoProduct;
        private readonly IReceiveRepository _repoReceive;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public ReceiveService(  ICategoryRepository repoCategory,
                                IProductRepository repoProduct,
                                IReceiveRepository repoReceive,
                                IMapper mapper,
                                MapperConfiguration configMapper) {
            _repoCategory = repoCategory;
            _repoProduct = repoProduct;
            _repoReceive = repoReceive;
            _mapper = mapper;
            _configMapper = configMapper;
        }
        public async Task<List<Category>> GetAllCategory()
        {
            var categorys = await _repoCategory.GetAll().ToListAsync();
            return categorys;
        }

        public async Task<List<Product>> GetProductByCatID(int categoryID)
        {
            var products = await _repoProduct.GetAll().Where(x => x.CatID == categoryID).ToListAsync();
            return products;
        }

        public async Task<bool> ReceiveRegister(Receive_Dto model)
        {
            model.ID = this.RandomString();
            model.Register_Date = DateTime.Now;
            model.Updated_Time = DateTime.Now;
            model.Status = "0";
            var receive = _mapper.Map<Receive>(model);
            _repoReceive.Add(receive);
            return await _repoReceive.SaveAll();
        }
        public string RandomString() {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[10];
            var random = new Random();
            for (int i = 0; i < 10; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            var finalString = new String(stringChars);
            return finalString;
        }

        public async Task<PagedList<ReceiveInformationModel>> GetWithPaginations(PaginationParams param, string userID)
        {
            var products = await _repoProduct.GetAll().ToListAsync();
            var receives = await _repoReceive.GetAll().Where(x => x.UserID.Trim() == userID && x.Status != "-1" 
                                                        && x.Status != "2").ToListAsync();
            var data = (from a in receives join b in products
                on a.ProductID equals b.ID select new ReceiveInformationModel() {
                    ID = a.ID,
                    UserID = a.UserID,
                    Accept_ID = a.Accept_ID,
                    DepID = a.DepID,
                    ProductID = a.ProductID,
                    ProductName = b.Name,
                    Qty = a.Qty,
                    Register_Date = a.Register_Date,
                    Accept_Date = a.Accept_Date,
                    Status = a.Status,
                    Updated_By = a.Updated_By,
                    Updated_Time = a.Updated_Time
                }).OrderByDescending(x => x.Register_Date).ToList();
            return PagedList<ReceiveInformationModel>.Create(data, param.PageNumber, param.PageSize);
        }
    }
}