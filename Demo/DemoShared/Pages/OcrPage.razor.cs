using Azure.AI.FormRecognizer.DocumentAnalysis;
using BootstrapBlazor.Ocr.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Microsoft.JSInterop;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;


namespace DemoShared.Pages
{
    public partial class OcrPage
    {
        List<string> res { get; set; }
        private Task OnResult(List<string> res)
        {
            this.res = res;
            StateHasChanged();
            return Task.CompletedTask;
        }
    }
}
