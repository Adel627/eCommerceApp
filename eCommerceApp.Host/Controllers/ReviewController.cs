using eCommerceApp.Application.Consts;
using eCommerceApp.Application.DTOs.Review;
using eCommerceApp.Application.Services.Interfaces;
using eCommerceApp.Host.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.Host.Controllers
{
    [Authorize(Roles = Roles.User)]
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController(IReviewService reviewService) : ControllerBase
    {
        private readonly IReviewService _reviewService = reviewService;

       
        [HttpPost("rate")]
        public async Task<IActionResult> Rate(RateRequest request)
        {
          var result =  await _reviewService.RateProduct(request , User.GetUserId()!);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpGet("product/rates/{productId}")]
        public async Task<IActionResult> GetAllProductRates(Guid productId)
        {
            var result = await _reviewService.GetAllProductReviewAsync(productId);
            return result.Success ? Ok(result.Value) : NotFound(result);

        }
        [HttpGet("all-user-rates")]
        public async Task<IActionResult> GetAllRates()
        {
           var result = await _reviewService.GetAllRatesAsync(User.GetUserId()!);
            return result.Any() ? Ok(result) : NotFound(result);

        }


        [HttpDelete("delete-rate/{RateId}")]
        public async Task<IActionResult> DeleteRate(Guid RateId)
        {
            var result = await _reviewService.DeleteRate(RateId);
            return result.Success ? Ok(result) : NotFound(result);
        }
        

        [HttpPost("add-comment")]
        public async Task<IActionResult> AddComment(CommentRequest request)
        {
            var result = await _reviewService.AddComment(request, User.GetUserId()!);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpGet("all-user-comments")]
        public async Task<IActionResult> GetAllComments()
        {
            var result = await _reviewService.GetAllCommentsAsync(User.GetUserId()!);
            return result.Any() ? Ok(result) : NotFound(result);

        }

        [HttpPut("update-comment")]
        public async Task<IActionResult> UpdateComment(UpdateCommentRequest request)
        {
            var result = await _reviewService.UpdateComment(request);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpDelete("delete-comment/{commentId}")]
        public async Task<IActionResult> DeleteComment(Guid commentId)
        {
            var result = await _reviewService.DeleteComment(commentId);
            return result.Success ? Ok(result) : NotFound(result);
        }
    }
}
