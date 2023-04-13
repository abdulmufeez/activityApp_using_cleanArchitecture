using Application.Core;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Delete
    {
        public class Command : IRequest<MediatorHandlerResult<Unit>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, MediatorHandlerResult<Unit>>
        {
        private readonly DataContext _context;
            public Handler(DataContext context)
            {
            _context = context;
            }

            public async Task<MediatorHandlerResult<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.FindAsync(request.Id);

                if (activity is null) return null;

                _context.Remove(activity);

                return await _context.SaveChangesAsync() > 0 
                    ? MediatorHandlerResult<Unit>.Success(Unit.Value)
                    : MediatorHandlerResult<Unit>.Failure("Failed to delete activity"); 
            }
        }
    }
}