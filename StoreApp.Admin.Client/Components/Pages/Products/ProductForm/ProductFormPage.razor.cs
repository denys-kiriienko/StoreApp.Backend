using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using StoreApp.Admin.Client.Services.Interfaces;
using StoreApp.Shared.Models;

namespace StoreApp.Admin.Client.Components.Pages.Products.ProductForm;

public partial class ProductFormPage
{
    [Inject] public required NavigationManager NavigationManager { get; set; }
    [Inject] public required IProductApiService ProductApiService { get; set; }

    [Parameter] public int? Id { get; set; }

    private bool IsEditMode => Id.HasValue;

    private ProductModel? ProductModel { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (IsEditMode)
        {
            var product = await ProductApiService.GetProductByIdAsync(Id.Value);

            if (product is null)
            {
                NavigationManager.NavigateTo("/products");
                return;
            }

            ProductModel = product;
        }
        else
        {
            ProductModel = new ProductModel() { Name = string.Empty, Price = 0 };
        }
    }

    private async Task HandleValidSubmit()
    {
        if (ProductModel is null)
            return;

        if (IsEditMode)
        {
            await ProductApiService.UpdateProductAsync(Id.Value, ProductModel);
        }
        else
        {
            await ProductApiService.AddProductAsync(ProductModel);
        }

        NavigationManager.NavigateTo("/products");
    }

    private async Task OnFileSelected(InputFileChangeEventArgs e)
    {
        var file = e.File;
        if (file is not null)
        {
            using var stream = file.OpenReadStream();
            using var ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            ProductModel!.ImageData = ms.ToArray();
        }
    }

    private void GoBack()
    {
        NavigationManager.NavigateTo("/products");
    }
}
