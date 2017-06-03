# TextureDownsizeFilter
This is mainly because of feedback from artists. Resizing filter of Unity is not good. So, They resize texture into photoshop. It is better if I add resize filtering option.

When you set maximum size of textures in the inspector of Unity, Unity do downsizing and resampling with Mitchell algorithm. You can not choice any other filtering approches into this process. But, fortunately, Unity support the way you can involve in importing process by using AssetPostprocessor API.
Implementation way is simple. C# support various image resampling algorithms from System.Draw. You can just use it. 
But, there is one big problem. Mono can use System.Draw in Windows but not in Mac. So, It works only for Windows at this moment.

# How to use
Copy Editor folder and past into your project. Since you copy, images which file name is end with "_bicubic.png" automatically resampled with Bicubic algorithm. 
You can simply expand to use various algothritm by uing System.Draw. Just reference the script TextureBicubicResampling.cs. Good luck!

