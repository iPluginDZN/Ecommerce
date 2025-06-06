using Ecommerce.Application.Products.Commands;
using MediatR;

namespace Ecommerce.API.Tasks;

public class UpdatePopularProductsTask(
    IServiceProvider serviceProvider,
    ILogger<UpdatePopularProductsTask> logger
) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(TimeSpan.FromMinutes(30));

        do
        {
            using var scope = serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetService<IMediator>();
            if (mediator == null)
            {
                logger.LogError("Mediator not found");
                continue;
            }

            await mediator.Send(new UpdatePopularProduct.Command(), stoppingToken);
            logger.LogInformation("Updated popular products");
        } while (
            !stoppingToken.IsCancellationRequested
            && await timer.WaitForNextTickAsync(stoppingToken)
        );
    }
}
