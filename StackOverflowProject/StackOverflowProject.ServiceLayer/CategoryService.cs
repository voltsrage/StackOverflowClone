using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackOverflowProject.DomainModels;
using StackOverflowProject.Repositories;
using StackOverflowProject.ViewModels;
using AutoMapper;

namespace StackOverflowProject.ServiceLayer
{
    public interface ICategoryService
    {
        void InsertCategory(CategoryViewModel cvm);
        void UpdateCategory(CategoryViewModel cvm);
        void DeleteCategory(int CategoryID);

        List<CategoryViewModel> GetCategories();
        CategoryViewModel GetCategoriesByCategoryID(int CategoryID);
    }

    public class CategoryService : ICategoryService
    {
        ICategoryRepository cr;

        public CategoryService()
        {
            cr = new CategoryRepository();
        }

        public void InsertCategory(CategoryViewModel cvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<CategoryViewModel, Category>(); cfg.IgnoreUnmapped(); });

            IMapper mapper = config.CreateMapper();

            Category c = mapper.Map<CategoryViewModel, Category>(cvm);

            cr.InsertCategory(c);            
        }

        public void UpdateCategory(CategoryViewModel cvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<CategoryViewModel, Category>(); cfg.IgnoreUnmapped(); });

            IMapper mapper = config.CreateMapper();

            Category u = mapper.Map<CategoryViewModel, Category>(cvm);

            cr.UpdateCategory(u);
        }
        
        public void DeleteCategory(int CategoryID)
        {
            cr.DeleteCategory(CategoryID);
        }

        public List<CategoryViewModel> GetCategories()
        {
            List<Category> u = cr.GetCategories();

            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Category, CategoryViewModel>(); cfg.IgnoreUnmapped(); });

            IMapper mapper = config.CreateMapper();

            List<CategoryViewModel> cvm = mapper.Map<List<Category>, List<CategoryViewModel>>(u);

            return cvm;
        }
        
        public CategoryViewModel GetCategoriesByCategoryID(int CategoryID)
        {
            Category c = cr.GetCategoriesByCategoryID(CategoryID).FirstOrDefault();

            CategoryViewModel cvm = null;
            if (c != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Category, CategoryViewModel>(); cfg.IgnoreUnmapped(); });

                IMapper mapper = config.CreateMapper();

                cvm = mapper.Map<Category, CategoryViewModel>(c);

                return cvm;
            }
            return cvm;
        }
    }
}
