﻿using Model.EF;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ProductDao
    {
        OnlineShopDbContext db = null;
        public ProductDao()
        {
            db = new OnlineShopDbContext();
        }
        public List<Product> ListNewProduct(int top)
        {
            return db.Products.OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }
        public List<string> ListName(string keywword)
        {
            return db.Products.Where(x => x.Name.Contains(keywword)).Select(x=>x.Name).ToList();
        }
        /// <summary>
        /// Get list product by category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public List<ProductViewModel> ListByCategoryId(long category, ref int totalRecord, int pageIndex=1,int pageSize=2)
        {
            totalRecord = db.Products.Where(x => x.CategoryID == category).Count();
            var model= from a in db.Products
                       join b in db.ProductCategories
                       on a.CategoryID equals b.ID

                       where a.CategoryID == category
                       select new ProductViewModel()
                       {
                           CateMetaTitle = b.MetaTitle,
                           CateName=b.Name,
                           CreatedDate=a.CreatedDate,
                           ID=a.ID,
                           Images=a.Image,
                           Name=a.Name,
                           MetaTitle=a.MetaTitle,
                           Price=a.Price
                       };
        model.OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1)* pageSize).Take(pageSize);
            return model.ToList();
        }

        public List<ProductViewModel> Search(string keyword, ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        {
            totalRecord = db.Products.Where(x => x.Name.Contains(keyword)).Count();
            var model = (from a in db.Products
                        join b in db.ProductCategories
                        on a.CategoryID equals b.ID
                        where a.Name.Contains(keyword)
                        select new 
                        {
                            CateMetaTitle = b.MetaTitle,
                            CateName = b.Name,
                            CreatedDate = a.CreatedDate,
                            ID = a.ID,
                            Images = a.Image,
                            Name = a.Name,
                            MetaTitle = a.MetaTitle,
                            Price = a.Price
                        }).AsEnumerable().Select(x=> new ProductViewModel(){
                            CateMetaTitle = x.MetaTitle,
                            CateName = x.Name,
                            CreatedDate = x.CreatedDate,
                            ID = x.ID,
                            Images = x.Images,
                            Name = x.Name,
                            MetaTitle = x.MetaTitle,
                            Price = x.Price
                        });
            model.OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return model.ToList();
        }
        /// <summary>
        /// Get feature product
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<Product> ListFeatureProduct(int top)
        {
            return db.Products.Where(x => x.TopHight != null && x.TopHight >DateTime.Now).OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }
        public List<Product> ListRelatedProduct(long productId)
        {
            var product = db.Products.Find(productId);
            return db.Products.Where(x => x.ID != productId && x.CategoryID == product.CategoryID).ToList();
        }
        public Product ViewDetail(long id)
        {
            return db.Products.Find(id);
        }
    }
    
}

