using Microsoft.Extensions.Diagnostics.HealthChecks;
using ProductServices.Attributes;
using ProductServices.Services.CategoryServices;

namespace ProductServices.Models.Entities
{

    [Auditable]
    //public class Category
    //{
    //    public Guid Id { get; set; }
    //    public string Name { get; set; }

    //    public Category category { get; set; }
    //    public Guid? ParentCategoryId { get; set; }
    //    public ICollection<Category> ChildCategories { get; set; }
    //    public ICollection<Product> Products { get; set; }
    //}

    public abstract class CategoryComponent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Guid? CategoryId { get; set; }
        public Category Category { get; set; }
        public abstract string Print();

        public abstract List<CategoryComponent> Add(CategoryComponent component);
        public abstract void Remove(CategoryComponent component);
        public abstract int GetChilgCount();

    }



    public class Category : CategoryComponent
    {

        
        public List<CategoryComponent> Components =  new List<CategoryComponent>();
        public ICollection<CategoryComponent> categoryComponent => Components;

        public Category(string name)
        {
            Name = name;
        }
        public Category()
        {
            
        }

        public override int GetChilgCount()
        {
            return Components.Count;
        }

        public override List<CategoryComponent> Add(CategoryComponent component)
        {
            Components.Add(component);
            return Components;
        }

        public override string Print()
        {
            string ul = $@"<ul> {Name}";
            foreach (var menuComponent in Components)
            {
                ul += menuComponent.Print();
            }
            ul += $@"</ul>";
            return ul;
        }

        public override void Remove(CategoryComponent component)
        {
            Components.Remove(component);
        }

    }



    public class CategoryItem : CategoryComponent
    {
        public string Link { get; set; }

        public CategoryItem(string name, string link)
        {
            Name = name;
            Link= link;
        }

        public CategoryItem()
        {
            
        }
        public override List<CategoryComponent> Add(CategoryComponent component)
        {
            throw new NotImplementedException();
        }

        public override string Print()
        {
            string li = $@"<li> <a href='{Link}'> {Name} </a> </li>";
            return li;
        }

        public override void Remove(CategoryComponent component)
        {
            throw new NotImplementedException();
        }

        public override int GetChilgCount()
        {
            return 0;
        }
    }


}
