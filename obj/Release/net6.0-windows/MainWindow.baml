   M S B A M L   `   `   `  ����   snipper    Snipper.MainWindow   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  Screenshot Tool��$	��750��$	��950�� � Icon$ resources/icon.png�� � WindowStartupLocation$ CenterScreen=�.��� $��0,0,0,-6q� � ColumnDefinitions �� $
��134* ��� $
��449* ��� $
��317* � � RowDefinitions �� $
��Auto ��� $��* ��� $
��Auto �.+��� $��0�� �� Orientation$ 
Horizontal=�$��
140,25,0,5q�$��1��$��2��$
��Left=�$	��496��.+��� -   $��CaptureButton��$��📷 New Screenshot��$
��15,8q�$��5q�$��14(�
�������
������  � Cursor$
 Hand������  �� 
StartPoint$ 0.5,0&� �� EndPoint$ 0.5,1&�.��� 	 � Color$	 	#FF81C784��� $	 	#FF81C784��
 � Offset$
 1Q��� -   $��
CopyButton��$��📋 Copy Screenshot��$
��15,8q�$��5q�$��14(�
�������
������ $
 Hand������ $ 0.5,0&�$ 0.5,1&�.��� $	 	#FF64B5F6��� $	 	#FF64B5F6��$
 1Q��� -   $��
SaveButton��$��💾 Save Screenshot��$
��15,8q�$��5q�$��14(�
�������
������ $
 Hand������ $ 0.5,0&�$ 0.5,1&�.��� $	 	#FFBA68C8��� $	 	#FFBA68C8��$
 1Q�� $��1��$��20,20,20,10q�$��3�� �� $	��10* ��� $	��33* �.+��� $
��Auto=�$
��Auto��.���� $��0��$��0,0,20,0q�.+��� $��Choose Backgrounds��$��16(� �� TextAlignment$ Center=�$
��Bold"�$��0,0,0,15q��� -   $��GradientBlue��$��Blue Gradient��
 � Tag$ BlueGradient��$	��0,5q�$��10q�
������� J� RenderTransformOrigin$ 0.5,0.5&�$
 Hand������ $ 0.5,1&�$ 0.5,0&�.��� $	 	#FF64B5F6��$
 0Q�� $	 	#FF64B5F6��$
 1Q��� -   $��GradientPurple��$��Purple Gradient��$ PurpleGradient��$	��0,5q�$��10q�
�������$
 Hand������ $ 0.5,0&�$ 0.5,1&�.��� $	 	#FF9575CD��$
 0Q�� $	 	#FF9575CD��$
 1Q��� -   $��GradientOrange��$��Orange Gradient��$ OrangeGradient��$	��0,5q�$��10q�
�������$
 Hand������ $ 0.5,1&�$ 0.5,0&�.��� $	 	#FFF99E17��� $	 	#FFF99E17��$
 1Q��� -   $��GrayGradient��$��Gray Gradient��$ GrayGradient��$	��0,5q�$��10q�
����������#DDD$
 Hand������ $ 0.5,1&�$ 0.5,0&�.��� $
	 Gray��� $
	 Gray��$
 1Q��� -   $��BlackGradient��$��Black Gradient��$ BlackGradient��$	��0,5q�$��10q�
���   �
�������$
 Hand���� -	   $��Transparent��$��Transparent��$ Transparent��$	��0,5q�$��10q�
������ 
���   ����#DDD$
 Hand���� $��Padding:��$
��Bold"�$��15(�$��Center=�$��0,010,10,10q��� -
    ��Name$ PaddingSlider�� � Minimum$ 0Q� � Maximum$	 100Q� � Value$ 50Q�$	��150���� $��Center=�$��Center=�$��	10,0,0,10q�����  �� ElementName$ PaddingSlider�� �� Path$ Value� �� StringFormat$ {0:F0}px���� $��
V Padding:��$
��Bold"�$��15(�$��Center=�$��0,010,10,10q��� -   $ VPaddingSlider��$ 0Q�$	 100Q�$ 10Q�$	��150���� $��Center=�$��Center=�$��	10,0,0,10q����� $ VPaddingSlider��$ Value�$ {0:F0}px���� $��Inset:��$
��Bold"�$��15(�$��Center=�$��0,010,10,10q��� -   $ InsetSlider��$ 0Q�$	 100Q�$ 15Q�$	��150���� $��Center=�$��Center=�$��10,0,0,0q����� $ InsetSlider��$ Value�$ {0:F0}px���� $��Depth:��$
��Bold"�$��15(�$��Center=�$��0,010,10,10q��� -   $ DepthSlider��$ 0Q�$	 100Q�$ 10Q�$	��150���� $��Center=�$��Center=�$��	10,0,0,10q����� $ DepthSlider��$ Value�$ {0:F0}px���� $��Shadow Blur:��$
��Bold"�$��15(�$��94��$��	10,0,0,10q��� -   $ ShadowBlurSlider��$ 0Q�$ 20Q�$ 5Q�$	��120���� $��40��$��	10,0,0,10q����� $ ShadowBlurSlider��$ Value�$ {0:F0}px���� $��Shadow Opacity:��$
��Bold"�$��15(�$	��117��$��	10,0,0,10q��� -   $ ShadowOpacitySlider��$ 0Q�$ 1Q�$	 0.5Q�$	��120���� $��25��$��10 10 10 10q����� *Value+$ ShadowOpacitySlider��$ {0:F0}%���� $��Quick Padding:��$��	0,10,0,20q�$��15(�$
��Bold"�$��Center=�$��Center=��� -    ��Name$ PaddingPresetComboBox��$	��140�� �� 
IsEditable$
 True�� �� Text$ 20��.@��� $��
None (0px)��$ 0���� $��Small (10px)��$ 10���� $��Medium (20px)��$ 20���� $��Large (40px)��$ 40���� $��Extra Large (60px)��$ 60���� $��Custom��$ -1���� $��px��$��Center=�$��5,0,0,0q�$��Center=��� $��1�����#DDD$��1q� �� CornerRadius$ 8��$��5q�._�� .+�(�  (� Stretch$ Uniform=�$��Center=�$��Center=�$��0q�.��� -    �Name$ ScreenshotContainer��$��Center=�$��Center=�.+��� -   $��BackgroundBorder��$ 8������ $	 0,0&�$	 1,1&�.��� $	 #E3F2FD��$
 0Q�� $	 	#FFF99E17��$	
 0.9Q��� -   $��ScreenshotImage��$
��None=�$��25q� J� Opacity$ 1Q�$y�Visible=�$��Center=� J� OpacityMask !�  !� Visual �� 
���   �$ 8������ *ActualWidth+ �� RelativeSource ��  �� AncestorType	 Image���� *ActualHeight+ �� 	 Image�� -   $��PlaceHolderBorder��$��60,200,60,200q����	#FAF3F0CC$ 15��  �� Padding$  30q�._��� $��Center=�$��Center=�.+��� $��🖼️��$��48(�$��Center=�$	 0.3Q��� -   $��PlaceholderText��$��Center=�$&�� Your screenshot will appear here��$��18(����#636e72$��0,010q��� -   $��PlaceHolderTextSub��$��Center=�$V��PClick "New Screenshot" to capture your screen with the selected styling options.��$��14(����#636e72$	 0.7Q�$ Center=��� -   $��PlaceholderWatermark��$ 15��$��20 20 25 25q�._��� $��Bottom=�$��Right=�.+��� -   $��txtWatermark��$��18(�$ 1Q�$
��Thin"�
��������� $�Screenshot by WinSnipper��