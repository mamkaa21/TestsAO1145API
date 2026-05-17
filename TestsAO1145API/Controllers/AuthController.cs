using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace TestsAO1145API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly Testao1145Context context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthController(Testao1145Context context)
        {
            this.context = context;

        }

        public class AuthOptions
        {
            public const string ISSUER = "MyAuthServer"; // издатель токена
            public const string AUDIENCE = "MyAuthClient"; // потребитель токена
            const string KEY = "mysupersecret_secretsecretsecretkey!123";   // ключ для шифрации
            public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
        }

        [HttpPost("CheckAccountIsExist")]
        public async Task<ActionResult> CheckAccountIsExist(StModel Student) //добавить проверку еще на роль - если рольID = 1, это админ, если рольID = 2 -> это юзер
        {
            var newUser = new Student
            {
                Id = Student.Id,
                Login = Student.Login,
                Password = Student.Password,
                FirstName = Student.FirstName,
                LastName = Student.LastName,
                Age = Student.Age,
                IdClass = Student.IdClass
            };
            if (string.IsNullOrEmpty(newUser.Login) || string.IsNullOrEmpty(newUser.Password))
                return BadRequest("Логин или пароль не иожет быть пустым");

            var user = await context.Students.FirstOrDefaultAsync(s => s.Login == newUser.Login);
            if (user == null)
                return NotFound("Неверный логин");
            else
            {
                if (newUser.Password != user.Password)
                {
                    return NotFound("Неверный пароль");
                }
                else
                {


                    int id = user.Id;

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, id.ToString()),

                    };
                    var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    //кладём полезную нагрузку
                    claims: claims,
                    //устанавливаем время жизни токена 2 минуты
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(20)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

                    string token = new JwtSecurityTokenHandler().WriteToken(jwt);
                    return Ok(token);
                    //    return Ok(new ResponceTokenAndEmployee
                    //    {
                    //        Token = token,

                    //        Student1 = user
                    //    });
                    //return Ok((StModel)user);
                }
            }

        }


                //public class ResponceTokenAndEmployee
                //{
                //    public string Token { get; set; }

                //    public Student Student1 { get; set; }
                //}


                [HttpPost("AddNewUser")]
                public async Task<ActionResult> AddNewUser(StModel Student)
                {
                    var newUser = new Student { Id = Student.Id, Login = Student.Login, Password = Student.Password,
                        FirstName = Student.FirstName, LastName = Student.LastName, Age = Student.Age, IdClass = Student.IdClass };
                    if (string.IsNullOrEmpty(newUser.Login))
                        return BadRequest("Вы не ввели логин");
                    var check = await context.Students.FirstOrDefaultAsync(s => s.Login == newUser.Login);
                    if (check == null)
                    {

                        context.Students.Add(newUser);
                        await context.SaveChangesAsync();
                        return Ok("Успешно!");
                    }
                    else
                        return BadRequest("Такой аккаунт уже существует");
                }
            
        
    }
    
}
