using Microsoft.EntityFrameworkCore;
using ProductServices.Models.Context;
using ProductServices.Models.Dtos;
using ProductServices.Models.Entities;
using ProductServices.Services.ProductServices;
using System.Security.Cryptography.Xml;

namespace ProductServices.Services.CategoryServices
{
    public interface ICategoryServices
    {
        Task<ResultDto<List<CatgoryDto>>> GetCategoryList(Guid? ParentId);
        Task<ResultDto> AddNewCategory(string Name, Guid? ParentId);
        Task<ResultDto> AddNewChidToCategort(string Name, string Link, Guid ParentId);
        Task<ResultDto<List<CatgoryDto>>> GetCategory(Guid? ParentId);
        Task<ResultDto<List<CatgoryDto>>> GetDrpCategories();

    }
    public class CategoryServices : ICategoryServices
    {
        private readonly DataBaseContext dataBaseContext;

        public CategoryServices(DataBaseContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext;
        }

        public async Task<ResultDto<List<CatgoryDto>>> GetCategoryList(Guid? ParentId)
        {

            if (ParentId == null)
            {
                var data = dataBaseContext.CategoryComponents.Where(p => p.CategoryId==null).Select(p => new CatgoryDto
                {
                    ChildCount = p.GetChilgCount(),
                    Id = p.Id,
                    Name = p.Name,
                }).ToList();
                return new ResultDto<List<CatgoryDto>>
                {
                    Data = data,
                    Success = true,
                    Message = ""
                };
            }
            var result = dataBaseContext.CategoryComponents.Where(p => p.CategoryId == ParentId).Select(p => new CatgoryDto
            {
                ChildCount = p.GetChilgCount(),
                Id = p.Id,
                Name = p.Name,

            }).ToList();
            return new ResultDto<List<CatgoryDto>>
            {
                Data = result,
                Success = true,
                Message=""
            };
            
        }

        public async Task<ResultDto> AddNewCategory(string Name,Guid? ParentId)
        {
            if (ParentId is null)
            {
                var category = new Category(Name);
                //category.Add(category);
                dataBaseContext.CategoryComponents.Add(category);
                dataBaseContext.SaveChanges();
                return new ResultDto
                {
                    Message = "دسته بندی با موفقیت اضافه شد.",
                    Success = true,
                };
            }
            var ParentCategory = dataBaseContext.CategoryComponents.Where(p=>p.Id==ParentId).FirstOrDefault();
            ParentCategory.Add(new Category(Name));
            dataBaseContext.SaveChanges();  
            return new ResultDto
            {
                Message = "دسته بندی با موفقیت اضافه شد.",
                Success = true,
            };
        }

        public async Task<ResultDto> AddNewChidToCategort(string Name, string Link, Guid ParentId)
        {
            var ParentCategory = dataBaseContext.CategoryComponents.Where(p => p.Id == ParentId).FirstOrDefault();
            ParentCategory.Add(new CategoryItem(Name, Link));
            dataBaseContext.SaveChanges();
            return new ResultDto
            {
                Message = "افزودن دسته بندی جدید با موفقیت انجام شد."
            };
        }

        public async Task<ResultDto<List<CatgoryDto>>> GetCategory(Guid? ParentId)
        {
            var result = dataBaseContext.CategoryComponents.AsQueryable();
            if (ParentId is null)
            {
               
                var data = result.Where(p => p.CategoryId==null).Select(p=> new CatgoryDto
                {
                    Name = p.Name,
                    Id = p.Id,
                    ChildCount = p.GetChilgCount()
                }).ToList();
                return new ResultDto<List<CatgoryDto>>
                {
                    Data = data,
                    Success = true,
                    Message = ""
                };
            }
            
            var categories= result.Include(p=>p.Category).Where(p=>p.Category.Id == ParentId).Select(p=> new CatgoryDto
            {
               Id = p.Id,
               Name = p.Name,
               ChildCount= p.GetChilgCount()
            }).ToList();

            return new ResultDto<List<CatgoryDto>> { Data = categories, Success = true };   
        }

        public async Task<ResultDto<List<CatgoryDto>>> GetDrpCategories()
        {
           
            var data = dataBaseContext.CategoryComponents
                .Select(p=> new CatgoryDto
                {
                    Id= p.Id,
                    Name = p.Name+"_"+p.Category.Name,  
                    ChildCount = p.GetChilgCount(),
                    
                }).ToList();
            return new ResultDto<List<CatgoryDto>>
            {
                Data = data.Where(p=>p.ChildCount!=0).ToList(),
                Success = true,
                Message = ""
            };
        }
    }

    public class CatgoryDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
     
        public string? Link { get; set; }
        public int? ChildCount { get; set; }
    }

    public class GetCategoriesDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
       
        public int? ChildsCount { get; set; }
        public Guid? ParentId { get; set; }
    }
}
