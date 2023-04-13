using Application.Core;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities
{
    public class List
    {
        public class Query : IRequest<MediatorHandlerResult<List<Activity>>> { }
        public class Handler : IRequestHandler<Query, MediatorHandlerResult<List<Activity>>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<MediatorHandlerResult<List<Activity>>> Handle(Query request, CancellationToken cancellationToken)
            {
                return MediatorHandlerResult<List<Activity>>.Success(await _context.Activities.ToListAsync());
            }
        }
    }
}