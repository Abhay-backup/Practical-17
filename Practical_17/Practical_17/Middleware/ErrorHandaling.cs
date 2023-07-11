namespace Practical_17.Middleware
{
	// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
	public class ErrorHandaling
	{
		private readonly RequestDelegate _next;

		public ErrorHandaling(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext httpContext)
		{

			await _next(httpContext);

			if (httpContext.Response.StatusCode == 404 && !httpContext.Response.HasStarted && !httpContext.Request.Path.StartsWithSegments("/Error"))
			{
				httpContext.Response.Redirect("/Error/NotFound");
			}
		}
	}

	// Extension method used to add the middleware to the HTTP request pipeline.
	public static class ErrorHandalingExtensions
	{
		public static IApplicationBuilder UseErrorHandaling(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<ErrorHandaling>();
		}
	}
}
