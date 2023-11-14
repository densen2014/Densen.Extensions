// // ********************************** 
// // Densen Informatica 中讯科技 
// // 作者：Alex Chow
// // e-mail:zhouchuanglin@gmail.com 
// // **********************************

// using Microsoft.Extensions.FileSystemGlobbing;
// using System;
// using System.Collections.Generic;
// using System.IO;
// #if NET6_0_OR_GREATER 
// using SixLabors.ImageSharp;
// using SixLabors.ImageSharp.Processing;


// namespace AME;

// /// <summary>
// /// Image 扩展方法
// /// </summary>


// public static class ImageExtensions
// {
//     /// <summary>
//     /// 查看图片信息
//     /// </summary>
//     /// <param name="fileFullPath"></param>
//     /// <returns></returns>
//     public static string GetExif(this string fileFullPath)
//     {
//         ImageInfo imageInfo = Image.Identify(fileFullPath);
//         return $"Width: {imageInfo.Width} Height: {imageInfo.Height}";
//     }

//     /// <summary>
//     /// 调整大小
//     /// </summary>
//     /// <param name="fileFullPath"></param>
//     /// <param name="newWidth">为空则取一半</param>
//     /// <param name="newHeight">为空则取一半</param>
//     /// <returns></returns>
//     public static string ResizeToJpg(this string fileFullPath,int? newWidth=null,int? newHeight = null)
//     {
//         var x= fileFullPath + "_.jpg";
//         using (Image image = Image.Load(fileFullPath))
//         {
//             image.Mutate(x => x.Resize(image.Width / 2, image.Height / 2));
//             image.SaveAsJpeg(x, new SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder
//             {
//                 Quality = 85
//             });
//         }
//         return x;
//     }

//     //public static async string Watermark(this string fileFullPath, string watermark)
//     //{
//     //    var x = fileFullPath + "_Watermark_.jpg";
//     //    using var tifa = await Image.LoadAsync(fileFullPath); // 原图
//     //    using var logo = await Image.LoadAsync(watermark); // 水印
//     //    // Clone 做出 new image
//     //    using var newImage = tifa.Clone(imageProcessing =>
//     //    {
//     //        logo.Mutate(x => x.StgResizeWidth(200)); // 缩小 logo (这个不是必须的, 刚巧我的图片比较大而已)
//     //        imageProcessing.DrawImage(
//     //            logo,
//     //            opacity: 0.5f,
//     //            location: new Point(100, 100)
//     //        );
//     //        // imageProcessing 就是原图
//     //        // DrawImage 就是在图上画画
//     //        // logo 就是把水印画上去的意思
//     //        // opacity 给 logo 加上透明度
//     //        // location 是 x,y 坐标, 看你想把水印打到哪里
//     //    });
//     //    await newImage.SaveAsJpegAsync(@"C:\Users\keatk\Desktop\temp\review-avatar\new-image.jpg"); // 保存
//     //}
// }
// #endif
