using BasketServices.Models.Dtos;
using BasketServices.Services.DiscountServices;

namespace BasketServices.Services
{
    public interface IBasketServices
    {
        Guid GetOrCreateBasket(string UserId);
        BasketDto GetBasket(string UserId);
        void AddItemToBasket(AddItemToBaketDto addItem);
        void RemoveItemFromBasket(Guid itemId);
        void SetQuantity(int quantity, Guid ItemId);
        void TransferBasketId(Guid anynumousId, Guid UserId);
        void ApplyDiscountToBasket(string BasketId, string DiscountId);

        ResultDto Checkout(CheckoutDto checkout,IDiscountServices discountServices);
    }
    }
