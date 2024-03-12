// using System.Net;
// using E_commerce_API.Errors;
// using System.Text.Json;

// namespace E_commerce_API.Middleware
// {
//     public class ExceptionMiddleware
//     {
//         private readonly IHostEnvironment _env;
//         private readonly RequestDelegate _next;
//         private readonly ILogger<ExceptionMiddleware> _logger;

//         public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger,
//             IHostEnvironment env) 
//         {
//             _env = env;
//             _next = next;
//             _logger = logger;
//         }

//         public async Task InvokeAsync(HttpClient context)
//         {
//             try
//             {
//                 await _next(context);
//             }
//             catch (Exception ex)
//             {

//             }
//         }

//     }
// }
