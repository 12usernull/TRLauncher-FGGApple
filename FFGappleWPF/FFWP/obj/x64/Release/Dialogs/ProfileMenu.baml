   M S B A M L   `   `   `  ���� L  HFFGapple Launcher, Version=4.5.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:FFWP.DialogsFFWP.Dialogs      FFWP.Dialogs.ProfileMenu   D @ModernWpf, Version=0.9.6.0, Culture=neutral, PublicKeyToken=nullM IModernWpf.Controls, Version=0.9.6.0, Culture=neutral, PublicKeyToken=null,ui!http://schemas.modernwpf.com/2019   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     ,localclr-namespace:FFWP.Dialogs     *adornershttp://gu.se/Adorners     &   ModernWpf.Controls.ContentDialog    Title$  ProfileMenu��
�������
���   �.��� $	��275��$	��497�� � ColumnDefinitions �� $
��365* ��� $
��132* �.+�� $��0,0,3,0q�$��2�� �� $
��220* ��� $��161.022 ��� $
��113* � � RowDefinitions �� �� $��52 �.+��� $��0,0,-1,0q�$��2��$��3��.+���  �� Left$	 276��$
��Wrap=�
 �� Top$ 14��$��18(�$
��Bold"�$��Center=�$	��Top=���  � Language$ tr-tr�$�Fabric Minecraft: ���� $	 276��$
��Wrap=�$ 50��$��18(�$
��Bold"�$��Center=�$	��Top=��� $ tr-tr�$�Forge Minecraft:��%  ModernWpf.Controls.ToggleSwitch  -     Name$ 	FabricTOG��$��44��$	 441��$ 12��$	��Top=�
���   �$��Center=�  -   $ ForgeTOG��$��44��$	 441��$ 46��$	��Top=�$��Center=��� -    ��Name$ vercombo��$	 278��$	 159��$	��209��$��Center=��� -    ��Name$ addbtt��$��Save Profile��$ 9��$	 232��$	��477��$
��Bold"�$	��Top=�$��Center=����� 	 �� EndPoint$	 0.5,1&�
 �� 
StartPoint$
 0.5,0&�.���  � Color$ Black��� $ 	#FF5D17DE�� � Offset$ 0Q��� $	 278��$ 78��$��3��$	��209��$��Center=�$��Center=��� -    ��Name$ 	developer��$
��Wrap=�$��14(�$
��Bold"�$	��210��$
��Left=�$	 280��$ 88.19��$	��Top=��� $�	Developer���� $ de-de�$�:���� -   $
 date��$
��Wrap=�$��14(�$
��Bold"�$	��210��$
��Left=�$	 280��$	 111��$	��Top=��� $
�Date���� $ de-de�$�:���� $
��Wrap=�$��14(�$
��Bold"�$	��210��$	 280��$	 133��$��Center=�$	��Top=��� $�	Publisher���� $ de-de�$�:���� -   $ ramtxt��$ 10��$
��Wrap=�$��18(�$
��Bold"�$	 198��$��62��$��Center=�$��Center=��� $ tr-tr�$
�RAM:���� -    ��Name$ 	ramslider�� �� IsSnapToTickEnabled .  �� TickFrequency$ 1Q� �� AutoToolTipPlacement$ TopLeft=� �� AutoToolTipPrecision$ 0�� � Minimum$ 1Q� �� Orientation$ 
Horizontal=� �� TickPlacement$ BottomRight=� �� IsMoveToPointEnabled . $��32��$ 67��$	 195��$	��423���� -	    ��Name$ profilename��$��Hidden=�$��Visible=�$
��Left=�$��	10,10,0,0q� �� TextWrapping$
 Wrap=�$��Profil Ismi:��$	��Top=�$	��109��{�.  �� 
IsReadOnly . $y�Hidden=��� -
   $ username��$
��Left=�$��	10,20,0,0q�$
 Wrap=�$��Kullanici Adi:��$	��Top=�$	��228��$��2���� -   $ 	serveript��$
��Left=�$��	10,56,0,0q�$
 Wrap=�$��	Server IP��$	��Top=�$	��159���� -   $ serverportt��$
��Left=�$��
174,56,0,0q�$
 Wrap=�$
��Port��$	��Top=�$��44��$��2���� -   $ widthtxt��$
��Left=�$��	10,92,0,0q�$
 Wrap=�$��Genislik��$	��Top=�$	��114���� -   $ 	heigthtxt��$
��Left=�$��
124,92,0,0q�$
 Wrap=�$��
Yükseklik��$	��Top=�$	��114��$��2���� -   $ 	customdir��$
��Left=�$��
10,130,0,0q�$
 Wrap=�$��Özel Dizin��$	��Top=�$	��159���� -   $��..��$��174,130,0,0q�$��32��$��64��
�������$��14(�$
��Bold"�$��2��$	��Top=����� $	 0.5,1&�$
 0.5,0&�.��� $ Black��� $ 	#FF5D17DE��$ 0Q��� -   $
 mail��$
��Left=�$��	10,20,0,0q�$
 Wrap=�$��E-Mail��$	��Top=�$	��114���� $
��Left=�$��	20,57,0,0q�$��1��$
��Wrap=�$��FFNet��$	��Top=�A� -    A�Name$ profilnameerror��$
��Left=�$��16��
���   �$	��Top=�$��17��$��	-9,38,0,0q�$y�Hidden=� J� RenderTransformOrigin$ 0.543,1.026&����� $	 0.5,1&�$
 0.5,0&�.��� $ Black��� $ 	#FF5D17DE��$ 0Q�A� -   $ 	crederror��$
��Left=�$��16��
���   �$	��Top=�$��17��$��	-9,55,0,0q�$y�Hidden=����� $	 0.5,1&�$
 0.5,0&�.��� $ Black��� $ 	#FF5D17DE��$ 0Q�A� -   $ 
mcvererror��$
��Left=�$��16��
���   �$	��Top=�$��17��$��106,128,0,0q�$��2��$y�Hidden=����� $	 0.5,1&�$
 0.5,0&�.��� $ Black��� $ 	#FF5D17DE��$ 0Q�G� -    G�Name$ passtxt��$	��114��$��126,20,125,223q��� -   $ 
passholder��$
��Left=�$��
131,53,0,0q�$
��Wrap=�$	��Top=�$��14(�$y�Hidden=��� $ de-de�$�Sifre���� -   $ devdata��$
��Left=�$��
359,88,0,0q�$
��Wrap=�$	��Top=�$��14(�$��2���� $ de-de�$�?!���� -   $ datedata��$
��Left=�$��359,111,0,0q�$
��Wrap=�$	��Top=�$��14(�$��2���� $ de-de�$�?!���� -   $ pubdata��$
��Left=�$��359,133,0,0q�$
��Wrap=�$	��Top=�$��14(�$��2���� $ de-de�$�?!��