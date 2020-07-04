using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackOverflowProject.DomainModels;

namespace StackOverflowProject.Repositories
{
    public interface ICategoryRepository
    {
        void InsertCategory(Category c);
        void UpdateCategory(Category c);
        void DeleteCategory(int CategoryID);

        List<Category> GetCategories();
        List<Category> GetCategoriesByCategoryID(int CategoryID);
    }

    public class CategoryRepository:ICategoryRepository
    {
        StackOverflowDBContext db = new StackOverflowDBContext();

        public void DeleteCategory(int CategoryID)
        {
            Category categoryToDelete = db.Categories.Where(u => u.CategoryID == CategoryID).FirstOrDefault();
            if (categoryToDelete != null)
            {
                db.Categories.Remove(categoryToDelete);
                db.SaveChanges();
            }
        }
        
        public List<Category> GetCategories()
        {
            List<Category> Categories = db.Categories.OrderBy(c => c.CategoryName).ToList();
            return Categories;
        }

        public List<Category> GetCategoriesByCategoryID(int CategoryID)
        {
            List<Category> Categories = db.Categories.Where(u => u.CategoryID == CategoryID).ToList();
            return Categories;
        }

        public void InsertCategory(Category c)
        {
            db.Categories.Add(c);
            db.SaveChanges();
        }
        
        public void UpdateCategory(Category c)
        {
            Category categoryToUpdate = db.Categories.Where(temp => temp.CategoryID == c.CategoryID).FirstOrDefault();
            if (categoryToUpdate != null)
            {
                categoryToUpdate.CategoryName= c.CategoryName;
                db.SaveChanges();
            }
        }
    }
}
