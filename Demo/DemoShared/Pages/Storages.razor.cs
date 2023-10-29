// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using BootstrapBlazor.Components;
using BootstrapBlazor.WebAPI.Services;
using FreeSql.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DemoShared.Pages;

/// <summary>
/// Storage 存取设置
/// </summary>
public partial class Storages
{
    [Inject, NotNull] protected IStorage? Storage { get; set; }
    [Inject, NotNull] protected ICookie? Cookie { get; set; }
    [Inject, NotNull] protected ToastService? ToastService { get; set; }

    private Users NewUser { get; set; } = new Users();

    private UsersLogin Model { get; set; } = new UsersLogin();


    private string? message { get; set; } = "";

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            Model.Username = await Cookie.GetValue("username");
            if (!string.IsNullOrEmpty(Model.Username))
            {
                Model.Remember = true;
                if (!string.IsNullOrEmpty(await Cookie.GetValue("hash")))
                {
                    Model.RememberPassword = true;
                    Model.Password = await Cookie.GetValue("hash");
                }
                StateHasChanged();
            }
        }
    }

    private async Task OnValidSubmit(EditContext context)
    {
        await Login();
    }

    private async Task OnValidRegister(EditContext context)
    {
        await Task.Delay(200);

        Model.Username = NewUser.Username;
        Model.Password = NewUser.Password;
        await Login();

    }

    private async Task Login()
    {
        if (string.IsNullOrEmpty(Model.Username) || string.IsNullOrEmpty(Model.Password))
        {
            await ToastService.Error("登录失败! ", "用户名或密码不能为空.");
            return;
        }

        await Task.Delay(200);



        if (Model.Remember)
        {
            await Cookie.SetValue("username", Model.Username);
            if (Model.RememberPassword)
            {
                await Cookie.SetValue("hash", Model.Password);
            }
            else
            {
                await Cookie.RemoveValue("hash");
            }
        }
        else
        {
            await Cookie.RemoveValue("username");
            await Cookie.RemoveValue("hash");
        }


        StateHasChanged();

    }


    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem("GetValue","读取",  "-","Task<string>"),
        new AttributeItem("SetValue","设置",  "-","Task"),
        new AttributeItem("RemoveValue","清除",  "-","Task"),
    };
}


/// <summary>
/// 登录类
/// </summary>
public class UsersLogin
{

    [DisplayName("用户名")]
    [Required(ErrorMessage = "请输入用户名")]
    public string? Username { get; set; }

    [DisplayName("密码")]
    [Required(ErrorMessage = "请输入密码")]
    public string? Password { get; set; }
    public string? Hash { get; set; }

    [DisplayName("记住我")]
    public bool Remember { get; set; } = true;

    [DisplayName("保持登录")]
    public bool RememberPassword { get; set; }

    public bool IsLoging { get; set; } = false;
}

/// <summary>
/// 用户表
/// </summary>
public class Users
{

    [AutoGenerateColumn(Visible = false, Editable = false)]
    [Column(IsIdentity = true, IsPrimary = true)]
    [DisplayName("序号")]
    public Guid UserID { get; set; }

    //[Column(IsPrimary = true)]
    [DisplayName("用户名")]
    [Required]
    public string? Username { get; set; }

    [DisplayName("真实姓名")]
    [Required]
    public string? FullName { get; set; }


    [DisplayName("所属公司")]
    [Required]
    public string? Company { get; set; }

    [AutoGenerateColumn(Visible = false, Editable = false, ComponentType = typeof(BootstrapPassword))]
    [DisplayName("用户密码")]
    [Required]
    [DataType(DataType.Password)]
    [StringLength(100, ErrorMessage = "'{0}'最低{2}字符,最大{1}字符.", MinimumLength = 6)]
    public string? Password { get; set; }

    [AutoGenerateColumn(Ignore = true)]
    [Column(IsIgnore = true)]
    [DataType(DataType.Password)]
    [Display(Name = "确认密码")]
    [Compare("Password", ErrorMessage = "两次密码不一致")]
    public string? ConfirmPassword { get; set; }


    public override string ToString() => $"[{UserID}] {Username} ({FullName})";

    public static Users GenUser()
    {

        return new Users
        {
            Username = "root",
            FullName = "超级用户"
        };
    }
}
