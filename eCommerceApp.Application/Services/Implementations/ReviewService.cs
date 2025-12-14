using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Review;
using eCommerceApp.Application.Services.Interfaces;
using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Interfaces;
using MapsterMapper;

namespace eCommerceApp.Application.Services.Implementations
{
    public class ReviewService(IProductRepository productRepository , IRateRepository rateRepository 
       ,ICommentRepository commentRepository , IMapper mapper) : IReviewService
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IRateRepository _rateRepository = rateRepository;
        private readonly ICommentRepository _commentRepository = commentRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<ServiceResponse> RateProduct(RateRequest request, string UserId)
        {
            var userRate = await _rateRepository.GetByUserIdandProductId(UserId, request.ProductId);

            var product = await _productRepository.GetCurrentByIdAsync(request.ProductId);
            if (product == null)
                return new ServiceResponse(false, "there are no products with the given Id");

            if (userRate != null)
            {
                userRate.Rate = request.Rate;
                userRate.UpdatedDate = DateTime.UtcNow;

                await _rateRepository.UpdateAsync(userRate);

                var rates = await _rateRepository.GetByProductId(request.ProductId);
                product.AverageRating = rates.Average(r => r.Rate);

                await _productRepository.UpdateAsync(product);
                return new ServiceResponse(true, "Rate Saved");

            }
            var rate = _mapper.Map<Rates>(request);
            rate.UserId = UserId;

            await _rateRepository.AddAsync(rate);

            var Rates = await _rateRepository.GetByProductId(request.ProductId);
            product.AverageRating = Rates.Average(r => r.Rate);

            await _productRepository.UpdateAsync(product);
            return new ServiceResponse(true, "Rate Saved");
        }
        public async Task<ServiceResponse> DeleteRate(Guid Id)
        {
            var Rate = await _rateRepository.GetByIdAsync(Id);
            if (Rate == null)
                return new ServiceResponse(false, "There are no Rates with this Id");

            await _rateRepository.DeleteAsync(Id);

            var product = await _productRepository.GetByIdAsync(Rate.ProductId);
            var Rates = await _rateRepository.GetByProductId(product!.Id);
            product.AverageRating = Rates.Average(r => r.Rate);

            await _productRepository.UpdateAsync(product);
            return new ServiceResponse(true, "Rate Deleted!!");

        }
        public async Task<IEnumerable<RateResponse>> GetAllRatesAsync(string UserId)
        {
            var rates = await _rateRepository.GetByUserId(UserId);

            return !rates.Any() ? []
                : _mapper.Map<IEnumerable<RateResponse>>(rates);
        }

        public async Task <ServiceResponse<ProductReview>> GetAllProductReviewAsync(Guid ProductId)
        {
            var product = await _productRepository.GetCurrentByIdAsync(ProductId);

            if(product == null)
                return new ServiceResponse<ProductReview>(false , "There are no products with the given Id");
      
            var rates = await _rateRepository.GetByProductId(ProductId);
            var comments = await _commentRepository.GetByProductId(ProductId);
            var productReview = new ProductReview
            {
                RateResponse = _mapper.Map<IEnumerable<RateResponse>>(rates),
                CommentResponse = _mapper.Map<IEnumerable<CommentResponse>>(comments)
            };
            return 
                new ServiceResponse<ProductReview>(true,Value: productReview);
        }
        public async Task<ServiceResponse> AddComment(CommentRequest request, string UserId)
        {

            var product = await _productRepository.GetCurrentByIdAsync(request.ProductId);
            if (product == null)
                return new ServiceResponse(false, "there are no products with the given Id");

            var comment = _mapper.Map<Comment>(request);
            comment.UserId = UserId;

            await _commentRepository.AddAsync(comment);
            return new ServiceResponse(true, "Comment Saved");

        }
        public async Task<IEnumerable<CommentResponse>> GetAllCommentsAsync(string UserId)
        {
            var comments = await _commentRepository.GetByUserId(UserId);

            return !comments.Any() ? []
                : _mapper.Map<IEnumerable<CommentResponse>>(comments);
        }
        public async Task<ServiceResponse> UpdateComment(UpdateCommentRequest request)
        {
            var comment = await _commentRepository.GetByIdAsync(request.Id);
            if (comment == null)
                return new ServiceResponse(false, "There are no comments with this Id");
          
            comment.Content = request.Content;
            comment.UpdatedDate = DateTime.UtcNow;

            await _commentRepository.UpdateAsync(comment);
            return new ServiceResponse(true, "Comment Updated!!");

        }
        public async Task<ServiceResponse> DeleteComment(Guid Id)
        {
            var comment = await _commentRepository.GetByIdAsync(Id);
            if (comment == null)
                return new ServiceResponse(false, "There are no comments with this Id");

            await _commentRepository.DeleteAsync(Id);
            return new ServiceResponse(true, "Comment Deleted!!");

        }

        
    }
}
