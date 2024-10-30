using Microservice.Web.FronEnd.Models.NewFolder;

namespace Microservice.Web.FronEnd.Services.BasketServices
{
    public interface IBasketServices
    {
        BasketDto GetBasket(string UserId);
        ResultDto AddItemToBasket(AddItemToBasketDto addItem);
        ResultDto RemoveItemFromBasket(Guid ItemId);
        ResultDto SetQuantity(int quantity, Guid ItemId);
        ResultDto ApplyBasketOnDiscount(string BasketId, string DiscountId);
        ResultDto CheckoutBasket(CheckoutDto checkout);
        string GetOrCreateBasket(string UserId);
        string GetProductBasket(string UserId);
    }

   
}
