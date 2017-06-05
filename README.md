# TextureDownsizeFilter project

ENG : This is mainly because of feedback from artists. Some people think Untiy's resizing filter of texture images is not good. So, They resize texture into photoshop. It is better if I add resize filtering option.

KOR : 이 프로젝트는 아티스트들의 피드백으로부터 착안하였습니다. 사람에 따라 유니티의 텍스쳐 이미지 리사이징 필터가 안좋게 느껴집니다. 그래서, 포토샵에서 리사이즈해서 가져오기도 합니다. 따라서, 필터링 옵션을 추가하면 좋겠다는 피드백입니다.

ENG : When you set maximum size of textures in the inspector of Unity, Unity do downsizing and resampling with Mitchell algorithm. You can not choice any other filtering approches into this process. But, fortunately, Unity support the way you can involve in importing process by using AssetPostprocessor API.

KOR : 유니티의 인스펙터를 통해서 텍스쳐의 maximum size를 지정하면, 유니티는 자동적으로 리샘플링하여 다운사이징합니다. 이 때 사용되는 알고리즘은 Mitchell입니다. 어떤 필터링을 수행할 것인 지를 선택하거나 할 수는 없습니다. 다행히도, 유니티는 AssetPostprocessor API를 통해서 이 과정에 개입할 수 있는 방법을 제공합니다.

ENG : Implementation way is simple. C# support various image resampling algorithms from System.Draw. You can just use it. But, there is one big problem. Mono can use System.Draw in Windows but not in Mac. So, It works only for Windows at this moment. I am researching way to solve.

KOR : 구현 방법은 간단합니다. C#은 System.Draw를 통해서 다양한 이미지 리샘플링 알고리즘을 제공합니다. 그냥 이를 가져다 쓰면 됩니다. 하지만 큰 문제가 하나 있습니다. Mono는 윈도우즈에서는 System.Draw를 쓸 수 있습니다. 맥에서는 안됩니다. 따라서, 이 스크립트는 아직 윈도우즈에서만 작동합니다. 현재 해결 방법을 찾고 있습니다.


# How to use

ENG : Copy Editor folder and past into your project. Since you copy, images which file name is end with "_bicubic.png" automatically resampled with Bicubic algorithm. You can simply expand to use various algothritm by uing System.Draw. Just reference the script TextureBicubicResampling.cs. Good luck! May the force be with you!

KOR : Editor 폴더를 너님의 프로젝트에 복붙해넣으면 됩니다. 복사한 이후부터는 파일 이름이 "_bicubic.png"로 끝나는 이미지는 Bicubic 알고리즘으로 리샘플링됩니다. 너님은 System.Draw를 이용하기만하면 다양한 알고리즘을 사용하도록 확장할 수 있습니다. TextureBicubicResampling.cs를 참고하셈. 행운을 빕니다. 포스가 함께하길!

![ref img](https://github.com/ozlael/TextureDownsizeFilter/blob/master/bicubicdiff.PNG)
Left : Unity default downsizing. Right : Bicubic. Bicubic looks more sharpen.


# To Do

ENG : I will do these. But I am not sure I have time(or my will) to handle these : 
- Optimization during converting from Bitmap into Texture
- Expand to use various algorithm
- Get feedbacks and check it is worth letting this feature into Unity's native. Please give me your feedback on my Facebook : https://www.facebook.com/ozlael.oz

KOR : 다음 내용들을 추가로 작업할 예정입니다. 다만, 이를 처리할 시간(혹은 의지)이 있을 지는 모르겠습니다 :
- 비트맵을 텍스쳐로 변환하는 과정의 최적화
- 다양한 알고리즘을 사용할 수 있도록 확장
- 피드백을 수집하고 유니티 네이티브 기능으로 삼을만한 가치가 있는 지 확인. 페이스북으로 피드백 주세요 : https://www.facebook.com/ozlael.oz
