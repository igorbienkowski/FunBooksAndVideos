using FunBooksAndVideos.Application.Dtos;

namespace FunBooksAndVideos.Application.Interfaces;

public interface IOrderService
{
    void CreateOrder(OrderRequest request);
}