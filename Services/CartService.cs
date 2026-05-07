

using Buoi24.ViewModel;

namespace Buoi24.Services;
public class CartService
{
    //Giã sử giỏ hàng có 1 sản phẩm
    public List<ProductCartViewModel> lstProd { get; set; } = new List<ProductCartViewModel>()
    {
        // new ProductCartViewModel
        // {
        //     Id = 2,
        //     image = "https://dummyimage.com/300x300/000/fff&text=Samsung+S24",
        //     name = "Samsung Galaxy S24",
        //     price = 1499,
        //     quantity = 1
        // }
    };

    public async Task AddItem(ProductCartViewModel itemClick)
    {
        //Kiểm tra giỏ hàng có item đó chưa nếu có thì tăng số lượng nếu chưa có thì thêm vào lstProd
        ProductCartViewModel? itemCart = lstProd.SingleOrDefault(item => item.Id == itemClick.Id);
        if (itemCart != null)
        {
            itemCart.quantity += 1;
        }
        else
        {
            itemClick.quantity = 1;
            lstProd.Add(itemClick);
        }

        //Cập nhật lại giao diện 
        await StateHasChange();
    }

    public async Task DeleteItem(ProductCartViewModel itemClick)
    {
        //Kiểm tra giỏ hàng có item đó chưa nếu có thì giảm số lượng nếu chưa có thì thêm vào lstProd
        ProductCartViewModel? itemCart = lstProd.SingleOrDefault(item => item.Id == itemClick.Id);
        if (itemCart != null)
        {
            lstProd.Remove(itemCart);
        }

        //Cập nhật lại giao diện 
        await StateHasChange();
    }

    public async Task UpdateQuantity(ProductCartViewModel itemClick, int quantity)
    {
        //Kiểm tra giỏ hàng có item đó chưa nếu có thì cập nhật số lượng nếu chưa có thì thêm vào lstProd
        ProductCartViewModel? itemCart = lstProd.SingleOrDefault(item => item.Id == itemClick.Id);
        if (itemCart != null)
        {
            itemCart.quantity += quantity;

            if (itemCart.quantity <= 0)
            {
                lstProd.Remove(itemCart);
            }
        }

        //Cập nhật lại giao diện 
        await StateHasChange();
    }

    public decimal ToTalPrice()
    {
        decimal totalPrice = lstProd.Sum(item => item.price * item.quantity);
        return totalPrice;
    }

    public int TotalQuantity()
    {
        int totalQuantity = lstProd.Sum(item => item.quantity);
        return totalQuantity;
    }

    public event Action OnChange;

    public async Task StateHasChange()
    {
        this.OnChange?.Invoke();
    }
}