using System;


public class ProductNotFoundException : Exception
{
    public ProductNotFoundException(string message) : base("Product not found")
    {
        
    }
}
