using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Receive_API._Repositorys.Interfaces;
using Receive_API._Services.Interfaces;
using Receive_API.Dto;
using Receive_API.Helpers;
using Receive_API.Models;

namespace Receive_API._Services.Services
{
    public class ProductService : IProductService
    {   
        private readonly IProductRepository _repoProduct;
        private readonly ICategoryRepository _repoCategory;
        public ProductService(  IProductRepository repoProduct,
                                ICategoryRepository repoCategory) {
            _repoProduct = repoProduct;
            _repoCategory = repoCategory;
        }

        public async Task<string> Add(Product model)
        {
            var productFind =  await _repoProduct.GetAll().Where(x => x.ID.Trim() == model.ID.Trim()).FirstOrDefaultAsync();
            if(productFind != null) {
                return "exist";
            } else {
                model.Update_Time = DateTime.Now;
                model.Status = "1";
                _repoProduct.Add(model);
                if(await _repoProduct.SaveAll()) {
                    return "ok";
                } else {
                    return "error";
                }
            }
        }

        public async Task<bool> Delete(string id)
        {
            var product = await _repoProduct.GetAll().Where(x => x.ID.Trim() == id.Trim()).FirstOrDefaultAsync();
            if(product != null) {
                _repoProduct.Remove(product);
                return await _repoProduct.SaveAll();
            } else {
                return false;
            }
        }

        public async Task<List<Category>> GetAllCategory()
        {
            var categories = await _repoCategory.GetAll().ToListAsync();
            return categories;
        }

        public async Task<PagedList<Product_Dto>> GetWithPaginations(PaginationParams param)
        {
            var products = await _repoProduct.GetAll().ToListAsync();
            var categorys = await _repoCategory.GetAll().ToListAsync();
            var data = (from a in products join b in categorys 
                    on a.CatID equals b.ID select new Product_Dto(){
                        ID = a.ID,
                        Name = a.Name,
                        CatID = a.CatID,
                        CatName = b.Name,
                        Update_By = a.Updated_By,
                        Update_Time = a.Update_Time
                    }).OrderByDescending(x => x.Update_Time).ToList();
            return PagedList<Product_Dto>.Create(data, param.PageNumber, param.PageSize);
        }

        public async Task<bool> Update(Product model)
        {
            var product = await _repoProduct.GetAll().Where(x => x.ID.Trim() == model.ID.Trim()).FirstOrDefaultAsync();
            if(product != null) {
                product.Name = model.Name;
                product.CatID = model.CatID;
                product.Status = "1";
                product.Update_Time = DateTime.Now;
                return await _repoProduct.SaveAll();
            } else {
                return false;
            }
        }
    }
}