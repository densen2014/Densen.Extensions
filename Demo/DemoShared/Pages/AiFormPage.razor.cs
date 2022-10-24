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
    public partial class AiFormPage
    {
        List<AnalyzedDocument> models { get; set; }
        private Task OnResult(List<AnalyzedDocument> models)
        {
            this.models = models;
            StateHasChanged();
            return Task.CompletedTask;
        }
    }
}
