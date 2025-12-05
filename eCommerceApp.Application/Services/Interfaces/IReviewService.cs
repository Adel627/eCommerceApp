using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Review;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerceApp.Application.Services.Interfaces
{
    public interface IReviewService
    {
        Task<ServiceResponse> RateProduct(RateRequest request , string UserId);
        Task<IEnumerable<RateResponse>> GetAllRatesAsync(string UserId);
        Task<ServiceResponse> DeleteRate(Guid Id);
        Task<ServiceResponse> AddComment(CommentRequest request , string UserId);
        Task<IEnumerable<CommentResponse>> GetAllCommentsAsync(string UserId);
        Task<ServiceResponse> UpdateComment(UpdateCommentRequest request );
        Task<ServiceResponse> DeleteComment(Guid Id);
    }
}
