using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LoginApp.Models;
using LoginApp.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using LoginApp.Attributes;

namespace LoginApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private LoginAppContext _context;

        public HomeController(ILogger<HomeController> logger, LoginAppContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// ログイン画面
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// ログイン処理
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public async Task<IActionResult> Login(LoginInfo login)
        {
            if (ModelState.IsValid)
            {
                var loginUser = GetLoginUser(login);

                // ログイン処理
                Claim[] claims = {
                    new Claim(ClaimTypes.NameIdentifier, loginUser.Id.ToString()), // ユニークID
                    new Claim(ClaimTypes.Name, loginUser.Id.ToString()),
                  };

                // 一意の ID 情報
                var claimsIdentity = new ClaimsIdentity(
                  claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // ログイン
                await HttpContext.SignInAsync(
                  CookieAuthenticationDefaults.AuthenticationScheme,
                  new ClaimsPrincipal(claimsIdentity),
                  new AuthenticationProperties
                  {
                    // Cookie をブラウザー セッション間で永続化するか？（ブラウザを閉じてもログアウトしないかどうか）
                    IsPersistent = false
                  });

                return UserPage(loginUser);
            }

            return Index();
        }

        /// <summary>
        /// ログインユーザーの取得処理
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        private ViewUser GetLoginUser(LoginInfo login)
        {
            var user = _context.Users.Single(x => x.Login_Id == login.Login_Id && x.Password == login.Password);

            var viewUser = new ViewUser
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Age = user.Age,
            };

            return viewUser;
        }

        /// <summary>
        /// ログアウト確認画面
        /// </summary>
        /// <returns></returns>
        public IActionResult Logout()
        {
            return View();
        }

        /// <summary>
        /// ログアウト処理
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> LogoutSuccess()
        {
            // ログアウト処理
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // ログアウトに成功したらログインページへ戻る
            return LocalRedirect(Url.Content("~/"));
        }

        // ログインしていない状態では開けないページを作成するには、[Authorize]属性をつける
        [Authorize]
        // URLを直打ちされた場合に表示できないように、新しく作成した[RefererConfirmation]Attributeを付与
        [RefererConfirmation]
        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize]
        [RefererConfirmation]
        /// <summary>
        /// ログインユーザー情報画面
        /// </summary>
        /// <returns></returns>
        public IActionResult UserPage(ViewUser user)
        {
            return View(nameof(UserPage), user);
        }

        // ==============================
        // エラー画面
        // ==============================

        #region リファラーエラー画面表示
        /// <summary>
        /// リファラーエラー画面表示 URLが直接入力されたときに通る
        /// </summary>
        /// <returns></returns>
        public IActionResult RefererError()
        {
            return View();
        }
        #endregion

        #region 認証エラー画面表示
        /// <summary>
        /// 認証エラー画面表示 認証が失敗またはクッキーが切れたときに通る
        /// </summary>
        /// <returns></returns>
        public IActionResult AuthorizationError()
        {
            return View();
        }
        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
