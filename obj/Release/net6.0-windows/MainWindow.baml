   M S B A M L   `   `   `  ����   snipper    Snipper.MainWindow   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$7  1Snipper - Create Beautiful Screenshots in seconds��$	��710��$	��950�� � Icon$ resources/icon.png�� � WindowStartupLocation$ CenterScreen=� � 	Resources    PrimaryButtonBrush �� Color  PrimaryButtonHoverBrush  SecondaryButtonBrush  SecondaryButtonHoverBrush  SecondaryButtonBorderBrush  SmoothPrimaryButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType	 �� CornerRadius
 �� Triggers P� Property P� Value  SmoothSecondaryButtonStyle  StyledTextBox ��Name��%Y  &	        &	      &	 *     &	 =     &	 P     &	 c     7   7  &	 �    7  7  7  &	     �� $ 	#FF2A80FF���� $ 	#FF1E6CD1���� $ #F8F8FF���� $ #E6E6FA���� $ #DCDCDC���� " ��.���  � ��
 �������  � ��$ SemiBold"���  � ��
 ���� ��  �@�Cursor$
 Hand����  � ��8   ��  � ��$ 20,10q���  � �� �� " ��.R��� #��z��$	 5��#��z��#��z��._��� $��Center=�$��Center=�#��z��
 P�  �@J�IsMouseOver$
 True��. ���  � ��8  �� " ��.���  � ��
 �   ���  � ��$ Normal"���  �@�Cursor$
 Hand����  � ��8   ��  � ��8  ��  � ��$ 1q���  � ��$ 20,10q���  � �� �� " ��.R��� #��z��$	 5��#��z��#��z��._��� $��Center=�$��Center=�#��z��
 P�  �@J�IsMouseOver$
 True��. ���  � ��8  �� " ��.���  � ��$ 13(���  � ��$ Segoe UI)���  � ��$	 5,2q���  � ��
 �   ���  � ��
 �������  � ��
 �������  � ��$ 1q���  �@J�SnapsToDevicePixels$
 True����  � �� �� " ��.R��� #��z��#��z��#��z��$	 5��._��� $ PART_ContentHost��$��0q�
 P�  �@J�IsMouseOver$
 True��. ���  � �� �	#FF0056B3P�  �@J�IsKeyboardFocusWithin$
 True��. ���  � �� �	#FF004A99��  � ��$ 2q�.��� $��0,0,0,-6q� � ColumnDefinitions �� $	��30* ��� $	��70* � � RowDefinitions �� $
��Auto ��� $��* ��� $
��Auto �.+��� $*��$resources/logo_color_transparent.png��$��70��$��25 0 0 0q�$��Center=�$
��Left=��� $��0�� �� Orientation$ 
Horizontal=�$��100,10,25,0q�$��1��$��Right=�$
��auto��.+��� -   $��CaptureButton��#��[ $��📷 New Screenshot��$��16,10q�$��5q��� -   $��
CopyButton��#��[ $��📋 Copy Screenshot��$��16,10q�$��5q��� -   $��
SaveButton��#��[ $��💾 Save Screenshot��$��16,10q�$��5q�� $��1��$��20,20,20,10q�$��3�� �� $	��14* ��� $	��38* �.+��� $
��Auto=�$
��Auto��.���� $��0��$��0,5,10,0q�.+��� $��Choose Backgrounds��$��15(� �� TextAlignment$
 Left=�$��Segoe UI)�$��SemiBold"�$��	15,0,0,15q��  � Orientation$ 
Horizontal=�$��Center=�.+��� -   $��GradientBlue�� � ToolTip$ Blue Gradient��
�������
 � Tag$ BlueGradient��$	��5,5q�$��10 15q�
������� J� RenderTransformOrigin$ 0.5,0.5&� � Cursor$
 Hand��$��80��$
��Left=�����  �� 
StartPoint$ 0.5,0&� �� EndPoint$ 0.5,1&�.���  � Color$ 	#FFDCEBFF�� � Offset$ 0Q�� $ 	#FFAFD2F0��$ 1Q��� -   $��GradientPurple��$ Purple Gradient��
�������$
��Left=�$ PurpleGradient��$	��5,5q�$��10 15q�$��80��
�������$
 Hand������ $ 0.5,0&�$ 0.5,1&�.��� $ 	#FFEBE1F5��$ 0Q�� $ 	#FFC3AAE1��$ 1Q��� -   $��GradientOrange��$ Orange Gradient��
�������$ OrangeGradient��$	��5,5q�$��10 15q�$��80��
�������$
 Hand������ $ 0.5,0&�$ 0.5,1&�.��� $ 	#FFFFF0DC��$ 0Q�� $ 	#FFFFCDA0��$ 1Q��� -   $��GrayGradient��$ Gray Gradient��
�������$ GrayGradient��$	��5,5q�$��10 15q�$��80��
�������$
 Hand������ $ 0.5,0&�$ 0.5,1&�.��� $ 	#FFF0F0F0��$ 0Q�� $ 	#FFC8C8C8��$ 1Q��� -	   $��BlackGradient��$ Black Gradient��
�������$ BlackGradient��$	��5,5q�$��10 15q�$��80��
�������$
 Hand������ $ 0.5,0&�$ 0.5,1&�.��� $ 	#FFD2D2D2��$ 0Q�� $ 	#FF8C8C8C��$ 1Q��� -
   $��GradientTeal��$ Teal Gradient��
�������$ TealGradient��$	��5,5q�$��10 15q�$��80��
�������$
 Hand������ $ 0.5,0&�$ 0.5,1&�.��� $ 	#FFE1F5F0��$ 0Q�� $ 	#FFB9E1D7��$ 1Q��� -   $��GradientPink��$ Pink Gradient��
�������$ PinkGradient��$	��5,5q�$��10 15q�$��80��
�������$
 Hand������ $ 0.5,0&�$ 0.5,1&�.��� $ 	#FFFFF0F5��$ 0Q�� $ 	#FFFAC3D2��$ 1Q��� -   $��GradientCyan��$ Cyan Gradient��
�������$ CyanGradient��$	��5,5q�$��10 15q�$��80��
�������$
 Hand������ $ 0.5,0&�$ 0.5,1&�.��� $ 	#FFE6FAFA��$ 0Q�� $ 	#FFBEE6E6��$ 1Q��� -   $��GradientYellow��$ Yellow Gradient��
�������$ YellowGradient��$	��5,5q�$��10 15q�$��80��
���   �$
 Hand������ $ 0.5,0&�$ 0.5,1&�.��� $ 	#FFFFFFE6��$ 0Q�� $ 	#FFFFF0B4��$ 1Q��� -   $��GradientRed��$ Red Gradient��
�������$ RedGradient��$	��5,5q�$��10 15q�$��80��
�������$
 Hand������ $ 0.5,0&�$ 0.5,1&�.��� $ 	#FFFFEBEB��$ 0Q�� $ 	#FFF5B4B4��$ 1Q��� -   $��GradientIndigo��$ Indigo Gradient��
�������$ IndigoGradient��$	��5,5q�$��10 15q�$��80��
�������$
 Hand������ $ 0.5,0&�$ 0.5,1&�.��� $ 	#FFE6EBFA��$ 0Q�� $ 	#FFB9C3E1��$ 1Q��� -   $��GradientGreen��$ Green Gradient��
�������$ GreenGradient��$	��5,5q�$��10 15q�$��80��
�������$
 Hand������ $ 0.5,0&�$ 0.5,1&�.��� $ 	#FFEFFAEA��$ 0Q�� $ 	#FFB9E1B9��$ 1Q��� -   $��GradientCharcoal��$ Charcoal Gradient��
�������$ CharcoalGradient��$	��5,5q�$��10 15q�$��80��
�������$
 Hand������ $ 0.5,0&�$ 0.5,1&�.��� $ 	#FFF0F0F0��$ 0Q�� $ 	#FFA0A0A0��$ 1Q��� -   $��GradientLightBlue��$ Light Blue Gradient��
�������$ LightBlueGradient��$	��5,5q�$��10 15q�$��80��
�������$
 Hand������ $ 0.5,0&�$ 0.5,1&�.��� $ 	#FFebf5ff��$ 0Q�� $ 	#ffb9d7f0��$ 1Q��� -   $��GradientPeach��$ Peach Gradient��
�������$ PeachGradient��$	��5,5q�$��10 15q�$��80��
�������$
 Hand������ $ 0.5,0&�$ 0.5,1&�.��� $ #fff5f0��$ 0Q�� $ #ffd2be��$ 1Q��� -   $��GradientSkyBlue��$ Sky Blue Gradient��
�������$ SkyBlueGradient��$	��5,5q�$��10 15q�$��80��
�������$
 Hand������ $ 0.5,0&�$ 0.5,1&�.��� $ 	#FFE6F5FF��$ 0Q�� $ 	#FFB9D7F0��$ 1Q��� -   $��GradientLavender��$ Lavender Gradient��
�������$ LavenderGradient��$	��5,5q�$��10 15q�$��80��
�������$
 Hand������ $ 0.5,0&�$ 0.5,1&�.��� $ 	#FFF5F0FF��$ 0Q�� $ 	#FFD2BEE6��$ 1Q��� -   $��GradientEmerald��$ Emerald Gradient��
�������$ EmeraldGradient��$	��5,5q�$��10 15q�$��80��
�������$
 Hand������ $ 0.5,0&�$ 0.5,1&�.��� $ 	#FFEFFAF0��$ 0Q�� $ 	#FFB9E1C8��$ 1Q��� -   $��Transparent��$ Transparent��
�������$	��170��$ Transparent��$	��5,5q�$��10 15q�
���   �$
 Hand����L�  j� TileMode$
 Tile=� j� Viewport$ 	0,0,10,10� j� ViewportUnits$ Absolute=� L� Drawing I� .���  � Brush
 �����  � Geometry  �� ! �� Rect$! 	0,0,10,10��  �	#FFE0E0E0  � .���� $! 0,0,5,5��� $! 5,5,5,5��� $��Advanced Styling��$��15(�$
 Left=�$��Segoe UI)�$��SemiBold"�$��	15,15,0,5q�A� " A� Columns$" 2��$��5 5 5 0q�$��Stretch=�.+��� $��5q�.+��� $��Padding:��$��12(�$��Segoe UI)�$��SemiBold"�$��0,0,0,5q��� -   # ��Name$# PaddingSlider��$ � Minimum$$ 0Q�% � Maximum$	% 100Q�& � Value$& 40Q�$��Stretch=��� $��11(�$
��Left=�$��0,3,0,0q����� ' �� ElementName$' PaddingSlider��( �� Path$( Value�) �� StringFormat$) {0:F0}px���� $��5q�.+��� $��Inset:��$��12(�$��SemiBold"�$��0,0,0,5q��� -   $# InsetSlider��$$ 0Q�$	% 100Q�$& 0Q�$��Stretch=��� $��11(�$
��Left=�$��0,3,0,0q����� $' InsetSlider��$( Value�$) {0:F0}px���� $��5q�.+��� $��Depth:��$��12(�$��SemiBold"�$��0,0,0,5q��� -   $# DepthSlider��$$ 0Q�$	% 100Q�$& 10Q�$��Stretch=��� $��11(�$
��Left=�$��0,3,0,0q����� $' DepthSlider��$( Value�$) {0:F0}px���� $��5q�.+��� $��Shadow Blur:��$��12(�$��SemiBold"�$��0,0,0,5q��� -   $# ShadowBlurSlider��$$ 0Q�$% 20Q�$& 5Q�$��Stretch=��� $��11(�$
��Left=�$��0,3,0,0q����� $' ShadowBlurSlider��$( Value�$) {0:F0}px���� $��5q�.+��� $��Shadow Opacity:��$��12(�$��SemiBold"�$��0,0,0,5q��� -   $# ShadowOpacitySlider��$$ 0Q�$% 1Q�$	& 0.5Q�$��Stretch=��� $��11(�$
��Left=�$��0,3,0,0q����� *Value+$' ShadowOpacitySlider��$) {0:P0}���� $��5q�.+��� $��Corners:��$��12(�$��SemiBold"�$��0,0,0,5q��� -   $# RoundedCornersSlider��$$ 0Q�$% 50Q�$& 5Q�$��Stretch=��� $��11(�$
��Left=�$��0,3,0,0q����� *Value+$' RoundedCornersSlider��$) {0:P0}���� $	��0.5��
�������$��0,5,0,5q��� $ 
Horizontal=�$��Center=�$��	0,10,0,10q�$��Center=�.+��� -   * ��Name$* WatermarkToggle��+ d� 	IsChecked$
+ TrueY�$  Enable / Disable Watermark��$��Center=�$��0,0,10,0q��� -   , ��Name$, WatermarkInput��$	��180��$��25��$��0,0,0,0q�#��[ $��Center=�$
{�True������ *PropWatermarkText+�� -    $��ScreenshotContainerBorder��$��1�����#DDD$��1q�$	 8��$��5 0 5 0q�._�� .+�(� - (� Stretch$- Uniform=�$��Center=�$��Center=�$��0 0 0 0q�$}�False��.��� -!   . �Name$. ScreenshotContainer��$��Center=�$��Center=�$��10q�.+��� -"   $��BackgroundBorder��$	 10������ $	 0,0&�$	 1,1&�.��� $ 	#FFFFF0DC��$ 0Q�� $ 	#FFFFCDA0��$ 1Q��� -#   $��ScreenshotImage��$��Uniform=�/ J� SnapsToDevicePixels$
/ True��0 � UseLayoutRounding$
0 True��$��50q�1 J� Opacity$1 1Q�$y�Visible=�$��Center=�2 �� BitmapScalingMode$2 HighQuality=�3 J� OpacityMask3 !� 4 !� Visual4 �� -$   $��ScreenshotImageBorder��
���   �$	 20������ *ActualWidth+5 �� RelativeSource5 �� 6 �� AncestorType	6 Image���� *ActualHeight+5 �� 	6 Image�� -%   7 ��Name$7 WatermarkText��
�������$	1 0.9Q�$��12(�$��Segoe UI)�$��Normal"�$��Right=�$��Bottom=�$��8q�8 J� IsHitTestVisible$8 False������ *PropWatermarkText+9 J� Effect9 3  -System.Windows.Media.Effects.DropShadowEffect  :   
BlurRadius$: 5Q�;   ShadowDepth$; 0Q�<   Color$< Black��=   Opacity$	= 0.9Q�� .+��� -&   > ��Name$> ScreenshotPreview��$��Uniform=��� -'   $��PlaceHolderBorder��$��60,230,60,200q����	#FAF3F0CC$	 15��? �� Padding$? 30q�._��� $��Center=�$��Center=�.+��� $��🖼️��$��48(�$��Center=�$	1 0.3Q��� -(   $��PlaceholderText��$��Center=�$&�� Your screenshot will appear here��$��18(����#636e72$��0,010q��� -)   $��PlaceHolderTextSub��$��Center=�$V��PClick "New Screenshot" to capture your screen with the selected styling options.��$��14(����#636e72$	1 0.7Q�$ Center=��� -*   @ ��Name$@ TempSnackbar�����#323232$	 6��$? 10q�$��Center=�$��Bottom=�$1 0Q�$��0,0,0,30q�$ 0.5,1&�z�T� A T� Y$A 20Q�._��� $ 
Horizontal=�.+��� -+   $7 TempSnackbarIcon��$	��✔��
�������$��16(�$��0,0,5,0q��� -,   $7 TempSnackbarText��
�������$��14(�