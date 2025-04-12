public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsDeleted { get; set; } = false;
    public virtual ICollection<Subcategory> Subcategories { get; set; } = new List<Subcategory>();

  
}