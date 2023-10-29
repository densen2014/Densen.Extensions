// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using Azure.AI.FormRecognizer.DocumentAnalysis;


namespace DemoShared.Pages;

public partial class AiFormPage
{
    private List<AnalyzedDocument>? models { get; set; }
    private Task OnResult(List<AnalyzedDocument> models)
    {
        this.models = models;
        StateHasChanged();
        return Task.CompletedTask;
    }
}
