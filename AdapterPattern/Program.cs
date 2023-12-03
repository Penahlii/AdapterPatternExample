using System;

public class ExternalInventorySystem
{
    public void FetchProductDetails(int productId)
    {
        Console.WriteLine($"Fetching details for product with ID {productId} from external system");
    }

    public void UpdateProductStock(int productId, int newStock)
    {
        Console.WriteLine($"Updating stock for product with ID {productId} to {newStock} in external system");
    }
}


public interface IProductManager
{
    ProductInfo GetProductInfo(int productId);
    void UpdateStock(int productId, int newStock);
}


public class ExternalInventoryAdapter : IProductManager
{
    private ExternalInventorySystem externalInventorySystem;

    public ExternalInventoryAdapter(ExternalInventorySystem externalInventorySystem)
    {
        this.externalInventorySystem = externalInventorySystem;
    }

    public ProductInfo GetProductInfo(int productId)
    {
        externalInventorySystem.FetchProductDetails(productId);
        return new ProductInfo { Id = productId, Name = "External Product", Price = 29.99f };
    }

    public void UpdateStock(int productId, int newStock)
    {
        externalInventorySystem.UpdateProductStock(productId, newStock);
        Console.WriteLine($"Updated stock for product with ID {productId} to {newStock} using external system");
    }
}


public class ProductInfo
{
    public int Id { get; set; }
    public string Name { get; set; }
    public float Price { get; set; }
}


public class ECommerceSystem
{
    public void ManageProduct(IProductManager productManager, int productId)
    {
        var productInfo = productManager.GetProductInfo(productId);
        Console.WriteLine($"Managing product: {productInfo.Name}, Price: {productInfo.Price}");
    }
}

class Program
{
    static void Main()
    {
        ExternalInventorySystem externalInventorySystem = new ExternalInventorySystem();
        ExternalInventoryAdapter externalAdapter = new ExternalInventoryAdapter(externalInventorySystem);

        ECommerceSystem ecommerceSystem = new ECommerceSystem();
        ecommerceSystem.ManageProduct(externalAdapter, 456);
    }
}