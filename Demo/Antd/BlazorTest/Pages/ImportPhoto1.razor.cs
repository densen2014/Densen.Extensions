using AmeBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace AntdBlazor.Pages;
public partial class ImportPhoto1
{
    private string? _srcPhoto;
    private bool _visible = false;
    private void ShowModal()
    {
        _visible = true;
    }

    private void ShowPhoto()
    {
        this._srcPhoto = "photo-1.jpg";
    } 

    protected override void OnInitialized()
    {
        this.ShowPhoto();
     }
}