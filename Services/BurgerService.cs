using Buoi24.ViewModel;

namespace Buoi24.Services;

public class BurgerService
{
    public readonly List<Topping> listTopping = new List<Topping>()
    {
        new Topping { Id = "salad", Price = 500, Quantity = 1 },
        new Topping { Id = "beef", Price = 1000m, Quantity = 1 },
        new Topping { Id = "cheese", Price = 800m, Quantity = 1 }
    };

    public BurgerService()
    {
    }

    public event Action OnChange;

    public async Task StateHasChange()
    {
        this.OnChange?.Invoke();
    }

    public async Task AddTopping(string id)
    {
        //Kiểm tra giỏ hàng có item đó chưa nếu có thì cập nhật số lượng nếu chưa có thì thêm vào lstProd
        Topping? itemCart = listTopping.SingleOrDefault(item => item.Id == id);
        if (itemCart != null)
        {
            itemCart.Quantity++;
        }
        else
        {
            listTopping.Add(new Topping { Id = id, Price = 0, Quantity = 1 });
        }

        await StateHasChange();
    }

    public async Task UpdateTopping(string id, int quantity)
    {
          //Kiểm tra giỏ hàng có item đó chưa nếu có thì cập nhật số lượng nếu chưa có thì thêm vào lstProd
          Topping? itemCart = listTopping.SingleOrDefault(item => item.Id == id);
          if (itemCart != null)
          {

            if (itemCart.Quantity + quantity < 1)
            {
                return;
            }
              itemCart.Quantity += quantity;

            if (itemCart.Price <= 0)
              { 
                itemCart.Quantity = 1;
                listTopping.Remove(itemCart);
              }
          }

      await StateHasChange();
    }

    public async Task RemoveTopping(string id)
    {
        Topping? itemCart = listTopping.SingleOrDefault(item => item.Id == id);
        if (itemCart != null)
        {   
            itemCart.Quantity = 1;
            listTopping.Remove(itemCart);
        }

        await StateHasChange();
    }
}