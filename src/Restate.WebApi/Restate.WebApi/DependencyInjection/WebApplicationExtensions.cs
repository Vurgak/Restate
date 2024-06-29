namespace Restate.WebApi.DependencyInjection;

public static class WebApplicationExtensions
{
    public static void UseWebApi(this WebApplication application)
    {
        application.UseHttpsRedirection();
        application.MapControllers();

        if (application.Environment.IsDevelopment())
        {
            application.UseSwagger();
            application.UseSwaggerUI();
        }
    }
}
