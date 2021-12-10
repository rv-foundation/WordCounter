using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WordCounter.Application.Common.Interfaces;
using WordCounter.Application.Common.Security;

namespace WordCounter.Application.Common.Behaviours
{
    public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ICurrentUserService _currentUserService;

        public AuthorizationBehaviour(ICurrentUserService currentUserService) {
            _currentUserService = currentUserService;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next) {
            var authorizeAttributes = request.GetType().GetCustomAttributes(typeof(AuthorizeAttribute),true).Length;

            if (authorizeAttributes > 0) {
                // Must be authenticated user
                if (_currentUserService.UserId == null) {
                    throw new UnauthorizedAccessException();
                }

            }

            // User is authorized / authorization not required
            return await next();
        }
    }
}
