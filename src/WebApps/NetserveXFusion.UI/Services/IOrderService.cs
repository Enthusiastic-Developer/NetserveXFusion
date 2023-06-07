using NetserveXFusion.UI.Models;

namespace NetserveXFusion.UI.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName);
    }
}
