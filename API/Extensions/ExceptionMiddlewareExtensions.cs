using Entities.ErrorModel;
using Entities.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Services.Contracts;

namespace API.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this WebApplication app, ILoggerService logger) //Extension method (genişletme metodu) yazmak için sınıfın ve metodun statik olması gerekir.
        {
            //Bu ifade sayesinde bu metot, Program.cs içinde sanki app nesnesinin kendi metoduymuş gibi (app.ConfigureExceptionHandler(...)) çağrılabilecek

            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature is not null)
                    {
                        // Hatanın tipine göre seviye ve status code belirliyoruz
                        var (statusCode, logLevel) = contextFeature.Error switch //Burada Switch Case yapısı kullanıldı
                        {
                            NotFoundException => (StatusCodes.Status404NotFound, "Warn"), //Case
                            BadRequestException => (StatusCodes.Status400BadRequest, "Warn"), //Case
                            _ => (StatusCodes.Status500InternalServerError, "Error") //Case
                        };

                        context.Response.StatusCode = statusCode;

                        // LOGLAMA: Başına [EXCEPTION] etiketi koyuyoruz ki aksiyonlardan ayrılsın
                        string logMsg = $"Path: {context.Request.Path} | Message: {contextFeature.Error.Message}";  //Fix: .Message eklendi.

                        if (logLevel == "Error")
                            logger.LogError(logMsg);
                        else
                            logger.LogWarn(logMsg);

                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message
                        }.ToString());
                    }
                });
            });

            // --- Loglamalar bu şekilde oluşturulmalıdır!!! ---
            // AKSİYON LOGU: Başına [ACTION] koyuyoruz
            //_logger.LogInfo($"UserID: {userId} - Action: {action}");
            //_logger.LogInfo($"Token Generated - User: {newUser.NickName}");
            //_logger.LogInfo($"User Deleted - ID: {id}");



            //UseExceptionHandler: ASP.NET Core'un içinde hazır gelen, "bir hata olduğunda buraya gir" diyen komuttur.
            //context.Response.StatusCode: Hata olduğunda kullanıcıya varsayılan olarak 500 (Internal Server Error) döneceğimizi belirtiyoruz.
            //context.Response.ContentType: Hata mesajını düz yazı veya HTML olarak değil, modern standart olan JSON formatında göndereceğimizi söylüyoruz.

            //IExceptionHandlerFeature: Bu arayüz, o an meydana gelen hatanın tüm detaylarını (hata mesajı, hangi satırda olduğu vb.) içinde barındırır.
            //contextFeature.Error: İşte asıl "patlayan" hata nesnesi buradadır.
            //logger.LogError: Senin LoggerManager sınıfını kullanarak, bu hatayı o anki tarihle beraber logs klasöründeki dosyaya mühürlüyoruz.
        }
    }
}
