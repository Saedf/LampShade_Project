namespace ShopManagement.Application.Contract.ProductCategory;

public class ViewModelProductCategory
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string CreationDate { get; set; }
    public string Picture { get; set; }
    public long ProductsCount { get; set; }
}