using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Application.Activities;

public class AList
{
    public class Query : IRequest<List<Activity>>
    {
    }

    public class Handler : IRequestHandler<Query, List<Activity>>
    {
        private readonly DataContext _dataContext;
        private readonly ILogger _logger;

        public Handler(DataContext dataContext, ILogger logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }

        public async Task<List<Activity>> Handle(Query request, CancellationToken cancellationToken)
        {
            try
            {
                for (int i = 0; i < 10; i++)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    await Task.Delay(1000, cancellationToken);
                    _logger.LogInformation($"Task {i} has completed");
                }
            }
            catch (Exception e)
            {
                _logger.LogInformation("Task has cancelled");
            }
            return await _dataContext.Activities.ToListAsync();
        }
    }
}