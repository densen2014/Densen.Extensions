// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using BootstrapBlazor.Components;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace DemoShared.Pages;

/// <summary>
/// MindMaps
/// </summary>
public sealed partial class MindMaps
{

//    [NotNull]
//    MindMap? MindMap { get; set; }

//    new string? Message { get; set; } = "";

//    private MindMapOption Options { get; set; } = new();

//    [NotNull]
//    [DisplayName("布局")]
//    private EnumMindMapLayout Layout { get; set; } = EnumMindMapLayout.mindMap;

//    [NotNull]
//    [DisplayName("主题")]
//    private EnumMindMapTheme Theme { get; set; } = EnumMindMapTheme.defaultTheme;

//    /// <summary>
//    /// 初始化数据
//    /// </summary>
//    public MindMapNode Data { get; set; } = new MindMapNode
//    {
//        Data = new NodeData
//        {
//            Text = "根节点",
//            Generalization = new Generalization
//            {
//                Text = "概要的内容"
//            },

//        },
//        Children = new List<MindMapNode>()
//{
//            new MindMapNode
//            {
//                Data = new NodeData
//                {
//                    Text = "二级节点1",
//                },
//                    Children = new List<MindMapNode>()
//{
//                        new MindMapNode
//                        {
//                            Data = new NodeData
//                            {
//                                Text = "分支主题1",
//                            },
//                        },
//                        new MindMapNode
//                        {
//                            Data = new NodeData
//                            {
//                                Text = "分支主题2",
//                            },
//                        },
//                        new MindMapNode
//                        {
//                            Data = new NodeData
//                            {
//                                Text = "分支主题3",
//                            },
//                        }
//                    }
//            },
//            new MindMapNode
//            {
//                Data = new NodeData
//                {
//                    Text = "二级节点2",
//                },
//            },
//            new MindMapNode
//            {
//                Data = new NodeData
//                {
//                    Text = "二级节点3",
//                },
//            }
//        }
//    };

//    private Task OnReceive(string? message)
//    {
//        Message = message;
//        return Task.CompletedTask;
//    }

//    private Task OnError(string message)
//    {
//        Message = message;
//        return Task.CompletedTask;
//    }

//    private async Task Export()
//    {
//        await MindMap.Export();
//        await ShowBottomMessage("下载Png");
//    }

//    private async Task ExportJson()
//    {
//        await MindMap.Export("json", WithConfig: false);
//        await ShowBottomMessage("下载Json");
//    }

//    private async Task ExportPng()
//    {
//        await MindMap.Export(IsDownload: false, WithConfig: false);
//        await ShowBottomMessage("已导出Png");
//    }

//    private async Task GetFullData()
//    {
//        await MindMap.GetData();
//    }

//    private async Task GetData()
//    {
//        await MindMap.GetData(false);
//    }

//    private async Task SetData()
//    {
//        if (Message != null) await MindMap.SetData(Message);
//    }

//    private async Task Reset()
//    {
//        await MindMap.Reset();
//    }

//    private async Task Sample()
//    {
//        Message = SampleData;
//        await SetData();
//    }

//    private async Task Sample2()
//    {
//        Message = SampleData2;
//        await SetData();
//    }

//    private async Task OnSelectedLayoutChanged(IEnumerable<SelectedItem> values, EnumMindMapLayout val)
//    {
//        await MindMap.SetLayout(val);
//    }

//    private async Task OnSelectedThemeChanged(IEnumerable<SelectedItem> values, EnumMindMapTheme val)
//    {
//        await MindMap.SetTheme(val);
//    }

//    /// <summary>
//    /// 获得属性方法
//    /// </summary>
//    /// <returns></returns>
//    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
//    {
//        new()
//        {
//            Name = "Data",
//            Description = "初始数据",
//            Type = "MindMapNode",
//            ValueList = " — ",
//            DefaultValue = " — "
//        },
//        new()
//        {
//            Name = "ShowUI",
//            Description = "显示内置UI",
//            Type = "bool",
//            ValueList = " — ",
//            DefaultValue = "true"
//        },
//        new()
//        {
//            Name = "StyleCss",
//            Description = "自定义CSS",
//            Type = "string",
//            ValueList = " — ",
//            DefaultValue = "empty"
//        },
//        new()
//        {
//            Name = "OnReceive",
//            Description = "收到数据回调方法",
//            Type = "Func<string?, Task>",
//            ValueList = " — ",
//            DefaultValue = " — "
//        },
//        new()
//        {
//            Name = "OnError",
//            Description = "错误回调方法",
//            Type = "Func<string, Task>",
//            ValueList = " — ",
//            DefaultValue = " — "
//        },
//        new()
//        {
//            Name = "Export",
//            Description = "下载为文件",
//            Type = "Task",
//            ValueList = " — ",
//            DefaultValue = " — "
//        },
//        new()
//        {
//            Name = "GetData",
//            Description = "获取数据",
//            Type = "Task",
//            ValueList = " — ",
//            DefaultValue = " — "
//        },
//        new()
//        {
//            Name = "SetData",
//            Description = "导入数据",
//            Type = "Task",
//            ValueList = " — ",
//            DefaultValue = " — "
//        },
//        new()
//        {
//            Name = "Reset",
//            Description = "复位",
//            Type = "Task",
//            ValueList = " — ",
//            DefaultValue = " — "
//        },
//        new()
//        {
//            Name = "SetTheme",
//            Description = "切换主题",
//            Type = "Task",
//            ValueList = " — ",
//            DefaultValue = " — "
//        },
//        new()
//        {
//            Name = "SetLayout",
//            Description = "切换布局",
//            Type = "Task",
//            ValueList = " — ",
//            DefaultValue = " — "
//        },
//        new()
//        {
//            Name = "Options",
//            Description = "配置选项",
//            Type = "MindMapOption",
//            ValueList = " — ",
//            DefaultValue = " — "
//        }
//    };

//    /// <summary>
//    /// NodeData
//    /// </summary>
//    /// <returns></returns>
//    private IEnumerable<AttributeItem> GetNodeDataAttributes() => new AttributeItem[]
//    {
//        new()
//        {
//            Name = "Text",
//            Description = "节点文本",
//            Type = "string",
//            ValueList = " — ",
//            DefaultValue = "empty"
//        },
//        new()
//        {
//            Name = "Image",
//            Description = "图片",
//            Type = "string",
//            ValueList = " — ",
//            DefaultValue = "empty"
//        },
//        new()
//        {
//            Name = "ImageTitle",
//            Description = "图片文本",
//            Type = "string",
//            ValueList = " — ",
//            DefaultValue = "empty"
//        },
//        new()
//        {
//            Name = "ImageSize",
//            Description = "图片尺寸",
//            Type = "ImageSize",
//            ValueList = " — ",
//            DefaultValue = "empty"
//        },
//        new()
//        {
//            Name = "Icon",
//            Description = "图标",
//            Type = "List<string>",
//            ValueList = " — ",
//            DefaultValue = "empty"
//        },
//        new()
//        {
//            Name = "Tag",
//            Description = "标签",
//            Type = "List<string>",
//            ValueList = " — ",
//            DefaultValue = "empty"
//        },
//        new()
//        {
//            Name = "Hyperlink",
//            Description = "链接",
//            Type = "string",
//            ValueList = " — ",
//            DefaultValue = "empty"
//        },
//        new()
//        {
//            Name = "HyperlinkTitle",
//            Description = "链接文本",
//            Type = "string",
//            ValueList = " — ",
//            DefaultValue = "empty"
//        },
//        new()
//        {
//            Name = "Note",
//            Description = "备注内容",
//            Type = "string",
//            ValueList = " — ",
//            DefaultValue = "empty"
//        },
//        new()
//        {
//            Name = "Generalization",
//            Description = "概要",
//            Type = "Generalization",
//            ValueList = " — ",
//            DefaultValue = "empty"
//        },
//        new()
//        {
//            Name = "Expand",
//            Description = "节点是否展开",
//            Type = "bool",
//            ValueList = " — ",
//            DefaultValue = "false"
//        },
//    };

//    private string SampleData = """
//       {"root":{"data":{"text":"根节点"},"children":[{"data":{"text":"二级节点1","expand":true},"children":[{"data":{"text":"分支主题"},"children":[{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"},"children":[{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"},"children":[{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"},"children":[{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"},"children":[{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"},"children":[{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"}}]},{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"}}]},{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"}}]},{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"}}]},{"data":{"text":"分支主题"}}]},{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"}}]}]},{"data":{"text":"二级节点2","expand":false},"children":[{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"},"children":[{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"}}]},{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"}}]},{"data":{"text":"二级节点3","expand":true},"children":[{"data":{"text":"分支主题"},"children":[{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"},"children":[{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"}}]},{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"}}]}]},{"data":{"text":"二级节点4","expand":false},"children":[{"data":{"text":"分支主题"},"children":[{"data":{"text":"分支主题"},"children":[{"data":{"text":"分支主题"},"children":[{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"}}]},{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"}}]},{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"},"children":[{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"},"children":[{"data":{"text":"分支主题"},"children":[{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"}}]},{"data":{"text":"分支主题"},"children":[{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"},"children":[{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"}}]},{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"}}]},{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"}}]}]},{"data":{"text":"分支主题"},"children":[{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"},"children":[{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"},"children":[{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"}}]},{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"}}]},{"data":{"text":"分支主题"},"children":[{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"}}]},{"data":{"text":"分支主题"},"children":[{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"}},{"data":{"text":"分支主题"}}]}]}]}]}]}}
//       """;
//    private string SampleData2 = """
//       {
//           "root": {
//               "data": {
//                   "text": "一周安排"
//               },
//               "children": [
//                   {
//                       "data": {
//                           "text": "生活"
//                       },
//                       "children": [
//                           {
//                               "data": {
//                                   "text": "锻炼"
//                               },
//                               "children": [
//                                   {
//                                       "data": {
//                                           "text": "晨跑"
//                                       },
//                                       "children": [
//                                           {
//                                               "data": {
//                                                   "text": "7:00-8:00"
//                                               },
//                                               "children": []
//                                           }
//                                       ]
//                                   },
//                                   {
//                                       "data": {
//                                           "text": "夜跑"
//                                       },
//                                       "children": [
//                                           {
//                                               "data": {
//                                                   "text": "20:00-21:00"
//                                               },
//                                               "children": []
//                                           }
//                                       ]
//                                   }
//                               ]
//                           },
//                           {
//                               "data": {
//                                   "text": "饮食"
//                               },
//                               "children": [
//                                   {
//                                       "data": {
//                                           "text": "早餐"
//                                       },
//                                       "children": [
//                                           {
//                                               "data": {
//                                                   "text": "8:30"
//                                               },
//                                               "children": []
//                                           }
//                                       ]
//                                   },
//                                   {
//                                       "data": {
//                                           "text": "午餐"
//                                       },
//                                       "children": [
//                                           {
//                                               "data": {
//                                                   "text": "11:30"
//                                               },
//                                               "children": []
//                                           }
//                                       ]
//                                   },
//                                   {
//                                       "data": {
//                                           "text": "晚餐"
//                                       },
//                                       "children": [
//                                           {
//                                               "data": {
//                                                   "text": "19:00"
//                                               },
//                                               "children": []
//                                           }
//                                       ]
//                                   }
//                               ]
//                           },
//                           {
//                               "data": {
//                                   "text": "休息"
//                               },
//                               "children": [
//                                   {
//                                       "data": {
//                                           "text": "午休"
//                                       },
//                                       "children": [
//                                           {
//                                               "data": {
//                                                   "text": "12:30-13:00"
//                                               },
//                                               "children": []
//                                           }
//                                       ]
//                                   },
//                                   {
//                                       "data": {
//                                           "text": "晚休"
//                                       },
//                                       "children": [
//                                           {
//                                               "data": {
//                                                   "text": "23:00-6:30"
//                                               },
//                                               "children": []
//                                           }
//                                       ]
//                                   }
//                               ]
//                           }
//                       ]
//                   },
//                   {
//                       "data": {
//                           "text": "工作日\n周一至周五"
//                       },
//                       "children": [
//                           {
//                               "data": {
//                                   "text": "日常工作"
//                               },
//                               "children": [
//                                   {
//                                       "data": {
//                                           "text": "9:00-18:00"
//                                       },
//                                       "children": []
//                                   }
//                               ]
//                           },
//                           {
//                               "data": {
//                                   "text": "工作总结"
//                               },
//                               "children": [
//                                   {
//                                       "data": {
//                                           "text": "21:00-22:00"
//                                       },
//                                       "children": []
//                                   }
//                               ]
//                           }
//                       ]
//                   },
//                   {
//                       "data": {
//                           "text": "学习"
//                       },
//                       "children": [
//                           {
//                               "data": {
//                                   "text": "工作日"
//                               },
//                               "children": [
//                                   {
//                                       "data": {
//                                           "text": "早间新闻"
//                                       },
//                                       "children": [
//                                           {
//                                               "data": {
//                                                   "text": "8:00-8:30"
//                                               },
//                                               "children": []
//                                           }
//                                       ]
//                                   },
//                                   {
//                                       "data": {
//                                           "text": "阅读"
//                                       },
//                                       "children": [
//                                           {
//                                               "data": {
//                                                   "text": "21:00-23:00"
//                                               },
//                                               "children": []
//                                           }
//                                       ]
//                                   }
//                               ]
//                           },
//                           {
//                               "data": {
//                                   "text": "休息日"
//                               },
//                               "children": [
//                                   {
//                                       "data": {
//                                           "text": "财务管理"
//                                       },
//                                       "children": [
//                                           {
//                                               "data": {
//                                                   "text": "9:00-10:30"
//                                               },
//                                               "children": []
//                                           }
//                                       ]
//                                   },
//                                   {
//                                       "data": {
//                                           "text": "职场技能"
//                                       },
//                                       "children": [
//                                           {
//                                               "data": {
//                                                   "text": "14:00-15:30"
//                                               },
//                                               "children": []
//                                           }
//                                       ]
//                                   },
//                                   {
//                                       "data": {
//                                           "text": "其他书籍"
//                                       },
//                                       "children": [
//                                           {
//                                               "data": {
//                                                   "text": "16:00-18:00"
//                                               },
//                                               "children": []
//                                           }
//                                       ]
//                                   }
//                               ]
//                           }
//                       ]
//                   },
//                   {
//                       "data": {
//                           "text": "休闲娱乐"
//                       },
//                       "children": [
//                           {
//                               "data": {
//                                   "text": "看电影"
//                               },
//                               "children": [
//                                   {
//                                       "data": {
//                                           "text": "1~2部"
//                                       },
//                                       "children": []
//                                   }
//                               ]
//                           },
//                           {
//                               "data": {
//                                   "text": "逛街"
//                               },
//                               "children": [
//                                   {
//                                       "data": {
//                                           "text": "1~2次"
//                                       },
//                                       "children": []
//                                   }
//                               ]
//                           }
//                       ]
//                   }
//               ]
//           }
//       }
//       """;

}
