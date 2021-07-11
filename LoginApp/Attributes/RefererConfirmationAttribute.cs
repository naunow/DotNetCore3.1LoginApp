using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LoginApp.Attributes
{
    /// <summary>
    /// URLが直接入力されたらログイン画面へ返すAttribute
    /// </summary>
    public class RefererConfirmationAttribute : ActionFilterAttribute
    {
        // アクションメソッドの実行前に呼ばれる
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string referer = context.HttpContext.Request.Headers["Referer"].ToString();
            Debug.WriteLine(referer);

            if (string.IsNullOrEmpty(referer))
            {
                // URLを直接入力されたときに表示させるURL
                var redirectTo = "~/Home/RefererError";
                // ログイン画面に戻す
                context.Result = new RedirectResult(redirectTo);
                return;
            }
        }
    }
}
